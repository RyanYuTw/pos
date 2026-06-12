using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pos.Data;
using Pos.Models;

namespace Pos.Controllers.BackOffice;

[Authorize(Policy = "AdminOnly")]
public class ProductMgmtController : Controller
{
    private readonly AppDbContext _db;
    public ProductMgmtController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index(string? kind, string? q)
    {
        var query = _db.Products.Where(p => p.Status != "2");
        if (!string.IsNullOrEmpty(kind))
            query = query.Where(p => p.Kind == kind);
        if (!string.IsNullOrEmpty(q))
            query = query.Where(p => p.Name.Contains(q) || p.No.Contains(q));

        ViewBag.Kinds = await _db.ProductKinds.ToListAsync();
        return View(await query.ToListAsync());
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Kinds = await _db.ProductKinds.ToListAsync();
        ViewBag.Sizes = await _db.ProductSizes.ToListAsync();
        return View(new Product());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product model)
    {
        if (await _db.Products.AnyAsync(p => p.No == model.No))
            ModelState.AddModelError("No", "商品編號已存在");
        if (!ModelState.IsValid)
        {
            ViewBag.Kinds = await _db.ProductKinds.ToListAsync();
            ViewBag.Sizes = await _db.ProductSizes.ToListAsync();
            return View(model);
        }

        model.Updater = User.Identity!.Name!;
        model.UpdateTime = DateTime.Now;
        model.Status = "0";
        _db.Products.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var item = await _db.Products.FindAsync(id);
        if (item == null) return NotFound();
        ViewBag.Kinds = await _db.ProductKinds.ToListAsync();
        ViewBag.Sizes = await _db.ProductSizes.ToListAsync();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Product model)
    {
        var item = await _db.Products.FindAsync(model.No);
        if (item == null) return NotFound();

        item.Name = model.Name;
        item.Kind = model.Kind;
        item.UnitValue = model.UnitValue;
        item.Unit = model.Unit;
        item.Size = model.Size;
        item.Price = model.Price;
        item.DiscountPrice = model.DiscountPrice;
        item.Description = model.Description;
        item.Shelfing = model.Shelfing;
        item.Promotion = model.Promotion;
        item.Mix = model.Mix;
        item.Stock = model.Stock;
        item.Updater = User.Identity!.Name!;
        item.UpdateTime = DateTime.Now;
        item.Status = "1";
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        var item = await _db.Products.FindAsync(id);
        if (item != null)
        {
            item.Status = "2";
            item.Updater = User.Identity!.Name!;
            item.UpdateTime = DateTime.Now;
            await _db.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}
