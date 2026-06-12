using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pos.Data;
using Pos.Models;

namespace Pos.Controllers.FrontDesk;

[Authorize]
public class ReservationController : Controller
{
    private readonly AppDbContext _db;
    public ReservationController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index(DateTime? date)
    {
        date ??= DateTime.Today;
        var list = await _db.Reservations
            .Where(r => r.BookDate.Date == date.Value.Date && r.Status != "2")
            .OrderBy(r => r.BookTime)
            .ToListAsync();
        ViewBag.Date = date.Value;
        return View(list);
    }

    [HttpGet]
    public IActionResult Create() => View(new Reservation
    {
        No = GenerateNo(),
        BookDate = DateTime.Today,
        BookTime = DateTime.Today.AddHours(18)
    });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Reservation model)
    {
        if (!ModelState.IsValid) return View(model);

        model.Updater = User.Identity!.Name!;
        model.UpdateTime = DateTime.Now;
        model.Status = "0";
        _db.Reservations.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var item = await _db.Reservations.FindAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Reservation model)
    {
        if (!ModelState.IsValid) return View(model);

        var item = await _db.Reservations.FindAsync(model.No);
        if (item == null) return NotFound();

        item.BookDate = model.BookDate;
        item.BookTime = model.BookTime;
        item.Contact = model.Contact;
        item.Phone = model.Phone;
        item.TableNo = model.TableNo;
        item.Attendance = model.Attendance;
        item.Member = model.Member;
        item.Memo = model.Memo;
        item.Updater = User.Identity!.Name!;
        item.UpdateTime = DateTime.Now;
        item.Status = "1";
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckIn(string id)
    {
        var item = await _db.Reservations.FindAsync(id);
        if (item != null)
        {
            item.Status = "1";
            item.Updater = User.Identity!.Name!;
            item.UpdateTime = DateTime.Now;
            await _db.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        var item = await _db.Reservations.FindAsync(id);
        if (item != null)
        {
            item.Status = "2";
            item.Updater = User.Identity!.Name!;
            item.UpdateTime = DateTime.Now;
            await _db.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }

    private static string GenerateNo()
        => DateTime.Now.ToString("yyyyMMddHHmm");
}
