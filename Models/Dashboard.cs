#pragma warning disable CS8618

namespace CraftShop.Models;

public class Dashboard
{
    public User User { get; set; }
    public int QuantitySold { get; set; }
    public int QuantityBought { get; set; }
    public double TotalEarned { get; set; }
}
