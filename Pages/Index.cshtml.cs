using FinanceTrackerWeb.Data;
using FinanceTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public int CategoriesCount { get; set; }
        public int TransactionsCount { get; set; }

        public List<Transaction> RecentTransactions { get; set; } = new();
        public List<string> BudgetWarnings { get; set; } = new();


        public async Task OnGetAsync()
        {
            // Подсчёт общего количества категорий и транзакций
            CategoriesCount = await _context.Categories.CountAsync();
            TransactionsCount = await _context.Transactions.CountAsync();

            // Пять последних операций
            RecentTransactions = await _context.Transactions
                .Include(t => t.Category)
                .OrderByDescending(t => t.Date)
                .Take(5)
                .ToListAsync();
            var now = DateTime.UtcNow;
            var monthStart = DateTime.SpecifyKind(new DateTime(now.Year, now.Month, 1), DateTimeKind.Utc);
            var monthEnd = DateTime.SpecifyKind(monthStart.AddMonths(1), DateTimeKind.Utc);

            // Получаем категории с лимитами
            var categoriesWithLimits = await _context.Categories
                .Where(c => c.MonthlyLimit.HasValue)
                .ToListAsync();

            // Считаем расходы
            var expenses = await _context.Transactions
                .Where(t => t.Type == TransactionType.Expense && t.Date >= monthStart && t.Date < monthEnd)
                .GroupBy(t => t.CategoryId)
                .Select(g => new { CategoryId = g.Key, TotalSpent = g.Sum(t => t.Amount) })
                .ToListAsync();

            foreach (var category in categoriesWithLimits)
            {
                var spent = expenses.FirstOrDefault(e => e.CategoryId == category.Id)?.TotalSpent ?? 0;
                if (spent > category.MonthlyLimit)
                {
                    var over = spent - category.MonthlyLimit.Value;
                    BudgetWarnings.Add($"Превышен лимит по категории «{category.Name}» на {over:C}.");
                }
            }

        }
    }
}
