using Microsoft.AspNetCore.Identity;

namespace CoffeeRecordsIdentity.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public List<CoffeeCup> CoffeeCups { get; set; } = new List<CoffeeCup>();

}
