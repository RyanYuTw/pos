using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pos.Data;
using Pos.ViewModels;

namespace Pos.Controllers.BackOffice;

[Authorize]
public class ReportController : Controller
{
    private readonly AppDbContext _db;
    public ReportController(AppDbContext db) => _db = db;

    /// <summary>交班表查詢</summary>
    public async Task<IActionResult> HandOver(DateTime? startDate, DateTime? endDate)
    {
        startDate ??= DateTime.Today;
        endDate ??= DateTime.Today;
        var list = await _db.HandOvers
            .Where(h => h.HandoverDate.Date >= startDate.Value.Date
                     && h.HandoverDate.Date <= endDate.Value.Date
                     && h.Status != "2")
            .OrderByDescending(h => h.HandoverDate)
            .ToListAsync();
        ViewBag.StartDate = startDate;
        ViewBag.EndDate = endDate;
        return View(list);
    }

    /// <summary>銷售商品統計表</summary>
    public async Task<IActionResult> SalesProduct(DateTime? startDate, DateTime? endDate, string? productNo)
    {
        startDate ??= DateTime.Today;
        endDate ??= DateTime.Today;

        var query = _db.OrdersDetails
            .Join(_db.Orders,
                d => d.No,
                o => o.No,
                (d, o) => new { Detail = d, Order = o })
            .Where(x => x.Order.OrderDate.Date >= startDate.Value.Date
                      && x.Order.OrderDate.Date <= endDate.Value.Date
                      && x.Order.Status == "1"
                      && x.Detail.Status != "2");

        if (!string.IsNullOrEmpty(productNo))
            query = query.Where(x => x.Detail.ProductNo == productNo);

        var result = await query
            .GroupBy(x => x.Detail.ProductNo)
            .Select(g => new SalesProductViewModel
            {
                ProductNo = g.Key,
                TotalAmount = g.Sum(x => x.Detail.Amount),
                TotalPrice = g.Sum(x => x.Detail.DiscountPrice ?? 0)
            })
            .ToListAsync();

        // 補充商品名稱
        var productNos = result.Select(r => r.ProductNo).ToList();
        var products = await _db.Products
            .Where(p => productNos.Contains(p.No))
            .Select(p => new { p.No, p.Name })
            .ToDictionaryAsync(p => p.No, p => p.Name);
        foreach (var r in result)
            r.ProductName = products.TryGetValue(r.ProductNo, out var name) ? name : r.ProductNo;

        ViewBag.StartDate = startDate;
        ViewBag.EndDate = endDate;
        return View(result);
    }

    /// <summary>日銷售額統計表</summary>
    public async Task<IActionResult> DailySales(int? year, int? month)
    {
        year ??= DateTime.Today.Year;
        month ??= DateTime.Today.Month;

        var result = await _db.Orders
            .Where(o => o.OrderDate.Year == year
                     && o.OrderDate.Month == month
                     && o.Status == "1")
            .GroupBy(o => o.OrderDate.Day)
            .Select(g => new DailySalesViewModel
            {
                Day = g.Key,
                TotalSales = g.Sum(o => o.GrandTotal),
                OrderCount = g.Count()
            })
            .OrderBy(r => r.Day)
            .ToListAsync();

        ViewBag.Year = year;
        ViewBag.Month = month;
        return View(result);
    }

    /// <summary>退貨明細表</summary>
    public async Task<IActionResult> ReturnItems(DateTime? startDate, DateTime? endDate)
    {
        startDate ??= DateTime.Today;
        endDate ??= DateTime.Today;

        var list = await _db.Orders
            .Include(o => o.Details)
            .Where(o => o.Status == "R"
                     && o.OrderDate.Date >= startDate.Value.Date
                     && o.OrderDate.Date <= endDate.Value.Date)
            .ToListAsync();

        ViewBag.StartDate = startDate;
        ViewBag.EndDate = endDate;
        return View(list);
    }
}
