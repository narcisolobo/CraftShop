using CraftShop.Context;
using System.ComponentModel.DataAnnotations;
namespace CraftShop.Validators;

public class UniqueUsernameAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Please enter your username.");
        }

        CraftShopContext? _db = (CraftShopContext?)validationContext.GetService(typeof(CraftShopContext));

        if (_db is not null)
        {
             if (_db.Users.Any(user => user.Username == value.ToString()))
            {
                return new ValidationResult("Username in use. Please choose another.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
        return new ValidationResult("Server error. DB connection not established.");
    }
}
