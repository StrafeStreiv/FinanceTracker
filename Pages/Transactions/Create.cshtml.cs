using FinanceTrackerWeb.Data;
using FinanceTrackerWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerWeb.Pages.Transactions
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
        public Transaction Transaction { get; set; } = new();

        public SelectList Categories { get; set; }

        public void OnGet()
        {
            Categories = new SelectList(_context.Categories.AsNoTracking().ToList(), "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = new SelectList(_context.Categories.AsNoTracking().ToList(), "Id", "Name");
                return Page();
            }

            // ”казываем владельца (UserId) и исправл€ем DateTime
            Transaction.UserId = _userManager.GetUserId(User);
            Transaction.Date = DateTime.SpecifyKind(Transaction.Date, DateTimeKind.Utc);

            _context.Transactions.Add(Transaction);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
