using CraftShop.Models;
using CraftShop.Context;
using Microsoft.AspNetCore.Mvc;

namespace CraftShop.Controllers;

public class OrdersController : Controller
{
    private CraftShopContext _db;
    public OrdersController(CraftShopContext craftShopContext)
    {
        _db = craftShopContext;
    }

    // [Protected]
    [HttpGet("orders")]
    public ViewResult OrderHistory()
    {
        return View("OrderHistory");
    }
}