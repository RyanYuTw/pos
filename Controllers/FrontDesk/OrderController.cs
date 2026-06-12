using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pos.Data;
using Pos.Models;
using Pos.ViewModels;

namespace Pos.Controllers.FrontDesk;

[Authorize]
public class OrderController : Controller
{
    private readonly AppDbContext _db;
    public OrderController(AppDbContext db) => _db = db;

    /// <summary>點餐 - 選擇桌位</summary>
    public async Task<IActionResult> Index()
    {
        var tables = await _db.TableSettings.ToListAsync();
        // 取得佔用中的桌位 → 訂單號對應表（同桌號取最新一筆）
        var activeOrders = await _db.Orders
            .Where(o => o.Status == "0")
            .Select(o => new { o.TableNo, o.No, o.OrderDate })
            .ToListAsync();

        var tableOrderMap = activeOrders
            .GroupBy(o => o.TableNo)
            .ToDictionary(g => g.Key, g => g.OrderByDescending(o => o.OrderDate).First().No);

        ViewBag.ActiveTables = tableOrderMap.Keys.ToList();
        ViewBag.TableOrderMap = tableOrderMap;
        return View(tables);
    }

    /// <summary>點餐 - 商品選擇</summary>
    [HttpGet]
    public async Task<IActionResult> Create(string tableNo)
    {
        var products = await _db.Products
            .Where(p => p.Shelfing && p.Status != "2")
            .ToListAsync();
        var kinds = await _db.ProductKinds.ToListAsync();
        ViewBag.TableNo = tableNo;
        ViewBag.Kinds = kinds;
        return View(products);
    }

    /// <summary>點餐 - 送出訂單</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderCreateViewModel model)
    {
        if (!ModelState.IsValid || model.Items == null || !model.Items.Any())
        {
            TempData["Error"] = "請至少選擇一項商品";
            return RedirectToAction("Create", new { tableNo = model.TableNo });
        }

        var userId = User.Identity!.Name!;
        var stationNo = HttpContext.Session.GetString("StationNo") ?? "001";
        var handOverNo = HttpContext.Session.GetString("HandOverNo") ?? GenerateHandOverNo();

        var subTotal = model.Items.Sum(i => (i.DiscountPrice ?? 0) * i.Amount);
        var serviceCharge = (float)(subTotal * 0.1);

        var order = new Orders
        {
            No = GenerateOrderNo(),
            CustomerId = "GUEST",
            StationNo = stationNo,
            HandOverNo = handOverNo,
            OrderDate = DateTime.Now,
            TableNo = model.TableNo,
            SubTotal = (float)subTotal,
            Allowance = 0,
            ServiceCharge = serviceCharge,
            GrandTotal = (float)(subTotal + serviceCharge),
            Updater = userId,
            UpdateTime = DateTime.Now,
            Status = "0"
        };

        order.Details = model.Items.Select((item, idx) => new OrdersDetail
        {
            No = order.No,
            SerialNo = idx + 1,
            ProductNo = item.ProductNo,
            Amount = item.Amount,
            Discount = item.Discount,
            DiscountPrice = item.DiscountPrice,
            Updater = userId,
            UpdateTime = DateTime.Now,
            Status = "0"
        }).ToList();

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        return RedirectToAction("Detail", new { id = order.No });
    }

    /// <summary>消費明細</summary>
    public async Task<IActionResult> Detail(string id)
    {
        var order = await _db.Orders
            .Include(o => o.Details)
            .FirstOrDefaultAsync(o => o.No == id);
        if (order == null) return NotFound();

        var productNos = order.Details.Select(d => d.ProductNo).Distinct().ToList();
        ViewBag.ProductNames = await _db.Products
            .Where(p => productNos.Contains(p.No))
            .ToDictionaryAsync(p => p.No, p => p.Name);

        return View(order);
    }

    /// <summary>換桌</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeTable(string orderNo, string newTableNo)
    {
        var inUse = await _db.Orders.AnyAsync(o => o.TableNo == newTableNo && o.Status == "0");
        if (inUse)
        {
            TempData["Error"] = $"桌號 {newTableNo} 已有消費者使用";
            return RedirectToAction("Detail", new { id = orderNo });
        }

        var order = await _db.Orders.FindAsync(orderNo);
        if (order != null)
        {
            order.TableNo = newTableNo;
            order.Updater = User.Identity!.Name!;
            order.UpdateTime = DateTime.Now;
            await _db.SaveChangesAsync();
        }
        return RedirectToAction("Detail", new { id = orderNo });
    }

    /// <summary>退餐（刪除明細項次）</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveItem(string orderNo, int serialNo)
    {
        var detail = await _db.OrdersDetails
            .FirstOrDefaultAsync(d => d.No == orderNo && d.SerialNo == serialNo);
        if (detail != null)
        {
            detail.Status = "2";
            detail.Updater = User.Identity!.Name!;
            detail.UpdateTime = DateTime.Now;

            var order = await _db.Orders.FindAsync(orderNo);
            if (order != null)
            {
                var activeItems = await _db.OrdersDetails
                    .Where(d => d.No == orderNo && d.Status != "2")
                    .ToListAsync();
                order.SubTotal = activeItems.Sum(d => d.DiscountPrice ?? 0);
                order.GrandTotal = order.SubTotal + order.ServiceCharge - order.Allowance;
                order.Updater = User.Identity!.Name!;
                order.UpdateTime = DateTime.Now;
            }
            await _db.SaveChangesAsync();
        }
        return RedirectToAction("Detail", new { id = orderNo });
    }

    /// <summary>出餐確認</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Serve(string orderNo, int serialNo)
    {
        // 出餐時間以 UpdateTime 記錄，Status 改為 "S" (Served)
        var detail = await _db.OrdersDetails
            .FirstOrDefaultAsync(d => d.No == orderNo && d.SerialNo == serialNo);
        if (detail != null)
        {
            detail.Status = "S";
            detail.Updater = User.Identity!.Name!;
            detail.UpdateTime = DateTime.Now;
            await _db.SaveChangesAsync();
        }
        return RedirectToAction("Detail", new { id = orderNo });
    }

    private static string GenerateOrderNo()
        => DateTime.Now.ToString("yyyyMMddHHmmss");

    private static string GenerateHandOverNo()
        => DateTime.Now.ToString("yyyyMMddHHmm");
}
