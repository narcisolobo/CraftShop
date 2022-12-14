#pragma warning disable CS8618
using CraftShop.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CraftShop.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [UniqueUsername]
    [Required(ErrorMessage = "Please enter your username.")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters long.")]
    [MaxLength(15, ErrorMessage = "Username must be less than 15 characters long.")]
    public string Username { get; set; }

    [UniqueEmail]
    [Required(ErrorMessage = "Please enter your email.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email.")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please enter your password.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    public string Password { get; set; }

    [NotMapped]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please re-type your password.")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }

    public List<Craft> Crafts { get; set; } = new List<Craft>();
    public List<Order> Orders { get; set; } = new List<Order>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public override string ToString()
    {
        return $"ID: {UserId}, Username: {Username}";
    }
}
