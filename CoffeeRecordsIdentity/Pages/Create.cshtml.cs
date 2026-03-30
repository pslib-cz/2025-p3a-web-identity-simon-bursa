using CoffeeRecordsIdentity.Data;
using CoffeeRecordsIdentity.InputModels;
using CoffeeRecordsIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeRecordsIdentity.Pages
{
    [Authorize]
    public class CreateModel(ILogger<CreateModel> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager) : PageModel
    {
        private readonly ILogger<CreateModel> _logger = logger;
        private readonly ApplicationDbContext _context = context;

        private readonly UserManager<ApplicationUser> _userManager = userManager;


        [BindProperty]
        public CoffeeCupIM Input { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("OnPostAsync called with MachineNo: {MachineNo} and UserName: {UserName}", Input.MachineNo, Input.UserName);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            

            var user = _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Page();
            }
            
            CoffeeCup cc = new CoffeeCup { MachineNo = Input.MachineNo, UserName = Input.UserName, Created = DateTime.Now, User = user, UserId = user.Id.ToString() };
            _context.Cups.Add(cc);
            await _context.SaveChangesAsync();
            _logger.LogInformation("CoffeeCup created with Id: {CoffeeCupId}", cc.CoffeeCupId);

            return RedirectToPage("./Index");
        }
    }
}
