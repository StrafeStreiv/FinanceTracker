using FinanceTrackerWeb.Data;
using FinanceTrackerWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerWeb.Pages.Categories
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public EditModel(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = _userManager.GetUserId(User);
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (category == null)
            {
                return NotFound();
            }
            Category = category;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == Category.Id && c.UserId == _userManager.GetUserId(User));
            if (category == null)
                return NotFound();

            category.Name = Category.Name;
            category.MonthlyLimit = Category.MonthlyLimit;
            category.Type = Category.Type;

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }


        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}