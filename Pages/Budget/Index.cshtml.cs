using FinanceTrackerWeb.Data;
using FinanceTrackerWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerWeb.Pages.Budget
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Category> Categories { get; set; } = new();
        public Dictionary<int, decimal> CategorySpent { get; set; } = new();

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            Categories = await _context.Categories
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Name)
                .ToListAsync();

            var now = DateTime.UtcNow;
            var monthStart = DateTime.SpecifyKind(new DateTime(now.Year, now.Month, 1), DateTimeKind.Utc);
            var monthEnd = DateTime.SpecifyKind(monthStart.AddMonths(1), DateTimeKind.Utc);


            CategorySpent = await _context.Transactions
                .Where(t => t.UserId == userId
                    && t.Type == TransactionType.Expense
                    && t.Date >= monthStart && t.Date < monthEnd)
                .GroupBy(t => t.CategoryId)
                .Select(g => new { CategoryId = g.Key, TotalSpent = g.Sum(t => t.Amount) })
                .ToDictionaryAsync(g => g.CategoryId, g => g.TotalSpent);
        }
    }
}
