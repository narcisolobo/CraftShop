#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CraftShop.Models;

public class Craft
{
    [Key]
    public int CraftId { get; set; }

    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Required]
    [DataType(DataType.Url)]
    public string ImageUrl { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    [DataType(DataType.Currency)]
    public double? Price { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int? Quantity { get; set; }

    [Required]
    public string Description { get; set; }

    public List<Order> Orders { get; set; } = new List<Order>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public override string ToString()
    {
        return $"CraftId: {CraftId}, Title: {Title}, CreatorId: {UserId}";
    }
}
