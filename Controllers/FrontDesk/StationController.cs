using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pos.Data;
using Pos.Models;

namespace Pos.Controllers.FrontDesk;

[Authorize]
public class StationController : Controller
{
    private readonly AppDbContext _db;
    public StationController(AppDbContext db) => _db = db;

    /// <summary>開帳</summary>
    [HttpGet]
    public async Task<IActionResult> Open()
    {
        var hosts = await _db.StationHosts.ToListAsync();
        return View(hosts);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Open(string stationNo, float openingCash = 0)
    {
        var userId = User.Identity!.Name!;
        var host = await _db.StationHosts.FindAsync(stationNo);
        if (host == null)
        {
            ModelState.AddModelError("", "找不到指定收銀機");
            return View(await _db.StationHosts.ToListAsync());
        }

        var handOverNo = GenerateHandOverNo();
        var openLog = new StationLog
        {
            StationNo = stationNo,
            StartTime = DateTime.Now,
            UserId = userId,
            HostName = host.HostName,
            HandOverNo = handOverNo,
            OpeningCash = openingCash,
            Updater = userId,
            UpdateTime = DateTime.Now,
            Status = "0"
        };
        _db.StationLogs.Add(openLog);
        await _db.SaveChangesAsync();

        HttpContext.Session.SetString("StationNo", stationNo);
        HttpContext.Session.SetString("HandOverNo", handOverNo);
        return RedirectToAction("Index", "Home");
    }

    /// <summary>取得當前班別的 StationLog（優先用 session，其次從 DB 撈最近一筆未結班）</summary>
    private async Task<StationLog?> GetCurrentLog()
    {
        var handOverNo = HttpContext.Session.GetString("HandOverNo");
        if (handOverNo != null)
            return await _db.StationLogs.FirstOrDefaultAsync(l => l.HandOverNo == handOverNo);

        var userId = User.Identity!.Name!;
        return await _db.StationLogs
            .Where(l => l.UserId == userId && l.Status == "0")
            .OrderByDescending(l => l.StartTime)
            .FirstOrDefaultAsync();
    }

    /// <summary>交班 - 顯示摘要</summary>
    [HttpGet]
    public async Task<IActionResult> HandOver()
    {
        var log = await GetCurrentLog();
        if (log == null) return RedirectToAction("Open");

        HttpContext.Session.SetString("HandOverNo", log.HandOverNo);
        HttpContext.Session.SetString("StationNo", log.StationNo);

        var orders = await _db.Orders
            .Where(o => o.HandOverNo == log.HandOverNo && o.Status == "1")
            .ToListAsync();

        var expenses = await _db.StationCashes
            .Where(c => c.HandOverNo == log.HandOverNo)
            .OrderBy(c => c.UpdateTime)
            .ToListAsync();

        var cash = orders.Sum(o => o.Cash ?? 0);
        var expenseTotal = expenses.Where(c => c.CashType == "1").Sum(c => (float)c.Amount);
        var incomeTotal = expenses.Where(c => c.CashType == "0").Sum(c => (float)c.Amount);

        ViewBag.HandOverNo = log.HandOverNo;
        ViewBag.OpeningCash = log.OpeningCash;
        ViewBag.GrandTotal = orders.Sum(o => o.GrandTotal);
        ViewBag.ServiceCharge = orders.Sum(o => o.ServiceCharge);
        ViewBag.Allowance = orders.Sum(o => o.Allowance);
        ViewBag.Cash = cash;
        ViewBag.CreditCard = orders.Sum(o => o.CreditCard ?? 0);
        ViewBag.Point = orders.Sum(o => o.Point ?? 0);
        ViewBag.Coupons = orders.Sum(o => o.Coupons ?? 0);
        ViewBag.Expenses = expenses;
        ViewBag.ExpenseTotal = expenseTotal;
        ViewBag.IncomeTotal = incomeTotal;
        // 應繳現金 = 收現 - 業外支出 + 業外收入（備用金本身不計入，留給下一班）
        ViewBag.NetCash = cash - expenseTotal + incomeTotal;

        return View();
    }

    /// <summary>備用金支出／收入新增</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCash(string cashType, decimal amount, string? description)
    {
        var log = await GetCurrentLog();
        if (log == null) return RedirectToAction("Open");

        _db.StationCashes.Add(new StationCash
        {
            StationNo = log.StationNo,
            HandOverNo = log.HandOverNo,
            CashType = cashType,
            Amount = amount,
            Description = description,
            Updater = User.Identity!.Name!,
            UpdateTime = DateTime.Now
        });
        await _db.SaveChangesAsync();
        return RedirectToAction("HandOver");
    }

    /// <summary>備用金支出刪除</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCash(int id)
    {
        var item = await _db.StationCashes.FindAsync(id);
        if (item != null) _db.StationCashes.Remove(item);
        await _db.SaveChangesAsync();
        return RedirectToAction("HandOver");
    }

    /// <summary>交班 - 確認送出</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> HandOver(string receiver)
    {
        var userId = User.Identity!.Name!;
        var log = await GetCurrentLog();
        if (log == null) return RedirectToAction("Open");

        var orders = await _db.Orders
            .Where(o => o.HandOverNo == log.HandOverNo && o.Status == "1")
            .ToListAsync();

        var expenses = await _db.StationCashes
            .Where(c => c.HandOverNo == log.HandOverNo && c.CashType == "1")
            .ToListAsync();
        var incomes = await _db.StationCashes
            .Where(c => c.HandOverNo == log.HandOverNo && c.CashType == "0")
            .ToListAsync();

        var cash = orders.Sum(o => o.Cash ?? 0);
        var expenseTotal = expenses.Sum(c => (float)c.Amount);
        var incomeTotal = incomes.Sum(c => (float)c.Amount);

        var handOver = new HandOver
        {
            No = log.HandOverNo,
            HandoverDate = DateTime.Now,
            OpeningCash = log.OpeningCash,
            GrandTotal = orders.Sum(o => o.GrandTotal),
            ServiceCharge = orders.Sum(o => o.ServiceCharge),
            Allowance = orders.Sum(o => o.Allowance),
            Cash = cash - expenseTotal + incomeTotal,
            CreditCard = orders.Sum(o => o.CreditCard ?? 0),
            Point = orders.Sum(o => o.Point ?? 0),
            Coupons = orders.Sum(o => o.Coupons ?? 0),
            HandOverUser = userId,
            Receiver = string.IsNullOrWhiteSpace(receiver) ? userId : receiver,
            Updater = userId,
            UpdateTime = DateTime.Now,
            Status = "0"
        };
        _db.HandOvers.Add(handOver);

        log.EndTime = DateTime.Now;
        log.Status = "1";

        await _db.SaveChangesAsync();

        HttpContext.Session.Remove("StationNo");
        HttpContext.Session.Remove("HandOverNo");
        return RedirectToAction("Open");
    }

    private static string GenerateHandOverNo()
        => DateTime.Now.ToString("yyyyMMddHHmm");
}
