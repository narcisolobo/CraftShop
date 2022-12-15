#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CraftShop.Models;

public class OrdersView
{
    public List<Order> Sales { get; set; } = new List<Order>();
    public List<Order> Orders { get; set; } = new List<Order>();
}
