using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pos.Data;
using Pos.Models;

namespace Pos.Controllers.BackOffice;

[Authorize(Policy = "AdminOnly")]
public class StoreMgmtController : Controller
{
    private readonly AppDbContext _db;
    public StoreMgmtController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
        => View(await _db.Stores.ToListAsync());

    [HttpGet]
    public IActionResult Create() => View(new Store());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Store model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Stores.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var item = await _db.Stores.FindAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Store model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Stores.Update(model);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
