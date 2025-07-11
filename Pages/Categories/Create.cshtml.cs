using FinanceTrackerWeb.Data;
using FinanceTrackerWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceTrackerWeb.Pages.Categories
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public CreateModel(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Category Category { get; set; } = new();

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            // Сохраняем UserId владельца категории
            Category.UserId = _userManager.GetUserId(User);

            _context.Categories.Add(Category);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
