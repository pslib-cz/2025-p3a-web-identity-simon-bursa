using CoffeeRecordsIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeRecordsIdentity.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class UsersModel(UserManager<ApplicationUser> userManager) : PageModel
    {
        public List<ApplicationUser> Users { get; set; } = [];

        public void OnGet() {
            Users = userManager.Users.ToList();
        } 
    }
}
