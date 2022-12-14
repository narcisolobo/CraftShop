using CraftShop.Models;
using CraftShop.Context;
using Microsoft.AspNetCore.Mvc;

namespace CraftShop.Controllers;
public class CraftsController : Controller
{
    private CraftShopContext _db;
    public CraftsController(CraftShopContext craftShopContext)
    {
        _db = craftShopContext;
    }

    // [Protected]
    [HttpGet("crafts/new")]
    public ViewResult NewCraft()
    {
        return View("NewCraft");
    }

    // [Protected]
    [HttpGet("crafts")]
    public ViewResult AllCrafts()
    {
        return View("AllCrafts");
    }

    // [Protected]
    [HttpGet("crafts/{craftId}")]
    public ViewResult ShowCraft(int craftId)
    {
        return View("ShowCraft");
    }
}