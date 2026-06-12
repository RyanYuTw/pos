using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pos.Data;
using Pos.Models;
using Pos.ViewModels;

namespace Pos.Controllers.FrontDesk;

[Authorize]
public class CheckoutController : Controller
{
    private readonly AppDbContext _db;
    public CheckoutController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Index(string orderNo)
    {
        var order = await _db.Orders
            .Include(o => o.Details)
            .FirstOrDefaultAsync(o => o.No == orderNo && o.Status == "0");
        if (order == null) return NotFound();

        await LoadProductNames(order.Details);
        ViewBag.Payments = await _db.Payments.ToListAsync();
        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Process(CheckoutViewModel model)
    {
        var order = await _db.Orders.FindAsync(model.OrderNo);
        if (order == null) return NotFound();

        order.Cash = model.Cash;
        order.CreditCard = model.CreditCard;
        order.Point = model.Point;
        order.Coupons = model.Coupons;
        order.Allowance = model.Allowance;
        order.GrandTotal = order.SubTotal + order.ServiceCharge - order.Allowance;
        order.Updater = User.Identity!.Name!;
        order.UpdateTime = DateTime.Now;
        order.Status = "1";  // 已結帳

        await _db.SaveChangesAsync();
        return RedirectToAction("Receipt", new { id = model.OrderNo });
    }

    /// <summary>收據</summary>
    public async Task<IActionResult> Receipt(string id)
    {
        var order = await _db.Orders
            .Include(o => o.Details)
            .FirstOrDefaultAsync(o => o.No == id);
        if (order == null) return NotFound();

        await LoadProductNames(order.Details);
        return View(order);
    }

    /// <summary>退貨 - 選擇項目</summary>
    [HttpGet]
    public async Task<IActionResult> Return(string orderNo)
    {
        var order = await _db.Orders
            .Include(o => o.Details)
            .FirstOrDefaultAsync(o => o.No == orderNo && o.Status == "1");
        if (order == null) return NotFound();

        await LoadProductNames(order.Details);
        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Return(string orderNo, List<int> returnSerialNos)
    {
        var userId = User.Identity!.Name!;
        var order = await _db.Orders
            .Include(o => o.Details)
            .FirstOrDefaultAsync(o => o.No == orderNo);
        if (order == null) return NotFound();

        foreach (var sn in returnSerialNos)
        {
            var detail = order.Details.FirstOrDefault(d => d.SerialNo == sn);
            if (detail != null)
            {
                detail.Status = "R";
                detail.Updater = userId;
                detail.UpdateTime = DateTime.Now;
            }
        }

        order.Status = "R";
        order.Updater = userId;
        order.UpdateTime = DateTime.Now;
        await _db.SaveChangesAsync();

        TempData["Success"] = "退貨作業完成";
        return RedirectToAction("Index", "Order");
    }

    private async Task LoadProductNames(IEnumerable<OrdersDetail> details)
    {
        var productNos = details.Select(d => d.ProductNo).Distinct().ToList();
        ViewBag.ProductNames = await _db.Products
            .Where(p => productNos.Contains(p.No))
            .ToDictionaryAsync(p => p.No, p => p.Name);
    }
}
