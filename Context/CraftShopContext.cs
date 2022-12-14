#pragma warning disable CS8618
using CraftShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CraftShop.Context;

public class CraftShopContext : DbContext
{
    public CraftShopContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Craft> Crafts { get; set; }
    public DbSet<Order> Orders { get; set; }
}
