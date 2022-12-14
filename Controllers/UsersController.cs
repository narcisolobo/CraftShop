using CraftShop.Models;
using CraftShop.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CraftShop.Controllers;

public class UsersController : Controller
{
    private CraftShopContext _context;

    public UsersController(CraftShopContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public ViewResult LoginRegister()
    {
        return View();
    }

    [HttpPost("users/register")]
    public IActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, user.Password);
            _context.Add(user);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("userId", user.UserId);
            HttpContext.Session.SetString("username", user.Username);
            return RedirectToAction("Dashboard");
        }
        return View("LoginRegister");
    }

    [HttpPost("users/login")]
    public IActionResult Login(LoginUser loginUser)
    {
        User? user = _context.Users.FirstOrDefault(u => u.Email == loginUser.LogEmail);
        if (user is null)
        {
            ModelState.AddModelError("LogEmail", "Email not found.");
            return View("LoginRegister");
        }

        PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
        var result = hasher.VerifyHashedPassword(loginUser, user.Password, loginUser.LogPassword);
        if (result == 0)
        {
            ModelState.AddModelError("LogPassword", "Incorrect Password.");
            return View("LoginRegister");
        }

        HttpContext.Session.SetInt32("userId", user.UserId);
        HttpContext.Session.SetString("username", user.Username);
        return RedirectToAction("Dashboard");
    }

    [Protected]
    [HttpGet("users/dashbaord")]
    public ViewResult Dashboard()
    {
        int userId = 0;
        int? sessionUserId = HttpContext.Session.GetInt32("userId");
        if (sessionUserId is not null)
        {
            userId = (int)sessionUserId;
        }
        User? user = _context.Users.FirstOrDefault(user => user.UserId == userId);
        return View("Dashboard", user);
    }

    [Protected]
    [HttpPost("users/logout")]
    public RedirectToActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("LoginRegister");
    }
}

public class ProtectedAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? _userId = context.HttpContext.Session.GetInt32("userId");
        if (_userId is null)
        {
            context.Result = new RedirectToActionResult("LoginRegister", "Users", null);
        }
    }
}
