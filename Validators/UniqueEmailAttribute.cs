using CraftShop.Context;
using System.ComponentModel.DataAnnotations;
namespace CraftShop.Validators;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Please enter your email.");
        }

        CraftShopContext? _db = (CraftShopContext?)validationContext.GetService(typeof(CraftShopContext));

        if (_db is not null)
        {
             if (_db.Users.Any(user => user.Email == value.ToString()))
            {
                return new ValidationResult("Email in use. Please login.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
        return new ValidationResult("Server error. DB connection not established.");
    }
}
