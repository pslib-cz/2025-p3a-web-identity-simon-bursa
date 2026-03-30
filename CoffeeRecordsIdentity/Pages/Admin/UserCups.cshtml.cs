using CoffeeRecordsIdentity.Data;
using CoffeeRecordsIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeRecordsIdentity.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class UserCupsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : PageModel
    {
        public List<CoffeeCup> CoffeeCups { get; set; } = [];

        public void OnGet()
        {
            var user = userManager.GetUserAsync(User);

            if (user == null)
            {
                return;
            }

            CoffeeCups = context.Cups.Where(x => x.UserId == user.Id.ToString()).ToList();
        }
    }
}
