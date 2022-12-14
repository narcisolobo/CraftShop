#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CraftShop.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Required]
    public int CraftId { get; set; }
    public Craft? Craft { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public override string ToString()
    {
        return $"OrderId: {OrderId}, CraftId: {CraftId}, UserId: {UserId}, Quantity: {Quantity}";
    }
}
