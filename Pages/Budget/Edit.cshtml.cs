using FinanceTrackerWeb.Data;
using FinanceTrackerWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerWeb.Pages.Budget
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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User);
            Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            if (Category == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == Category.Id && c.UserId == _userManager.GetUserId(User));
            if (category == null)
                return NotFound();

            category.MonthlyLimit = Category.MonthlyLimit;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
