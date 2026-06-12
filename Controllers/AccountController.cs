using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Pos.Data;
using Pos.ViewModels;

namespace Pos.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _db;

    public AccountController(AppDbContext db) => _db = db;

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.UserId == model.UserId && u.Status != "2");

        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
        {
            ModelState.AddModelError("", "帳號或密碼錯誤");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserId),
            new(ClaimTypes.GivenName, user.Name),
            new("UserGroup", user.UserGroup ?? ""),
            new("EmployeeNo", user.EmployeeNo)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        user.UserLogin = true;
        await _db.SaveChangesAsync();

        return LocalRedirect(returnUrl ?? "/");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        var userId = User.Identity?.Name;
        if (userId != null)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user != null)
            {
                user.UserLogin = false;
                await _db.SaveChangesAsync();
            }
        }
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    [Authorize]
    [HttpGet]
    public IActionResult ChangePassword() => View();

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var userId = User.Identity!.Name!;
        var user = await _db.Users.FindAsync(userId);
        if (user == null || !BCrypt.Net.BCrypt.Verify(model.OldPassword, user.Password))
        {
            ModelState.AddModelError("", "舊密碼不正確");
            return View(model);
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
        user.Updater = userId;
        user.UpdateTime = DateTime.Now;
        user.Status = "1";
        await _db.SaveChangesAsync();

        TempData["Success"] = "密碼已修改成功";
        return RedirectToAction("Index", "Home");
    }
}
