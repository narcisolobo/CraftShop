using CraftShop.Models;
using CraftShop.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    [HttpPost("crafts")]
    public IActionResult CreateCraft(Craft craft)
    {
        if (ModelState.IsValid)
        {
            _db.Add(craft);
            _db.SaveChanges();
            return RedirectToAction("ShowCraft", new {craftId = craft.CraftId});
        }
        return NewCraft();
    }

    // [Protected]
    [HttpGet("crafts")]
    public ViewResult AllCrafts()
    {
        List<Craft> allCrafts = _db.Crafts
            .Include(c => c.User)
            .ToList();
        return View("AllCrafts", allCrafts);
    }

    // [Protected]
    [HttpGet("crafts/{craftId}")]
    public ViewResult ShowCraft(int craftId)
    {
        Craft? craft = _db.Crafts
            .Include(c => c.User)
            .FirstOrDefault(c => c.CraftId == craftId);
        if (craft is null)
        {
            return AllCrafts();
        }
        ViewBag.quantity = craft.Quantity;
        return View("ShowCraft", craft);
    }
    
    [Protected]
    [HttpGet("orders")]
    public ViewResult OrderHistory()
    {
        OrdersView ordersView = new OrdersView()
        {
            Sales = _db.Orders
                .Include(o => o.User)
                .Include(o => o.Craft)
                .Where(o => o.Craft.UserId == HttpContext.Session.GetInt32("userId"))
                .ToList(),
            
            Orders = _db.Orders
                .Include(o => o.User)
                .Include(o => o.Craft)
                .Where(o => o.UserId == HttpContext.Session.GetInt32("userId"))
                .ToList(),
        };
        return View("OrderHistory", ordersView);
    }

    [Protected]
    [HttpPost("")]
    public IActionResult CreateOrder(Order order)
    {
        if (ModelState.IsValid)
        {
            Craft? craft = _db.Crafts.FirstOrDefault(c => c.CraftId == order.CraftId);
            if (craft is not null)
            {
                craft.Quantity -= order.Quantity;
                _db.Add(order);
                _db.SaveChanges();
                return RedirectToAction("ShowCraft", new { craftId = craft.CraftId });
            }
            return AllCrafts();
        }
        return ShowCraft(order.CraftId);
    }
}
