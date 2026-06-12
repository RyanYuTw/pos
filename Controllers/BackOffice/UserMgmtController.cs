using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pos.Data;
using Pos.Models;

namespace Pos.Controllers.BackOffice;

[Authorize(Policy = "AdminOnly")]
public class UserMgmtController : Controller
{
    private readonly AppDbContext _db;
    public UserMgmtController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index(string? q)
    {
        var query = _db.Users.Where(u => u.Status != "2");
        if (!string.IsNullOrEmpty(q))
            query = query.Where(u => u.Name.Contains(q) || u.UserId.Contains(q));
        return View(await query.ToListAsync());
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadUserGroups();
        return View(new User());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(User model)
    {
        if (await _db.Users.AnyAsync(u => u.UserId == model.UserId))
            ModelState.AddModelError("UserId", "帳號已存在");
        if (!ModelState.IsValid)
        {
            await LoadUserGroups();
            return View(model);
        }

        model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
        model.Updater = User.Identity!.Name!;
        model.UpdateTime = DateTime.Now;
        model.Status = "0";
        _db.Users.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var item = await _db.Users.FindAsync(id);
        if (item == null) return NotFound();
        await LoadUserGroups();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(User model)
    {
        if (!ModelState.IsValid)
        {
            await LoadUserGroups();
            return View(model);
        }
        var item = await _db.Users.FindAsync(model.UserId);
        if (item == null) return NotFound();

        item.Name = model.Name;
        item.IdNo = model.IdNo;
        item.UserGroup = model.UserGroup;
        item.EmployeeNo = model.EmployeeNo;
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
        var item = await _db.Users.FindAsync(id);
        if (item != null)
        {
            item.Status = "2";
            item.Updater = User.Identity!.Name!;
            item.UpdateTime = DateTime.Now;
            await _db.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }

    private async Task LoadUserGroups()
    {
        var groups = await _db.UserGroups.OrderBy(g => g.Id).ToListAsync();
        ViewBag.UserGroups = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(groups, "Name", "Name");
    }
}
