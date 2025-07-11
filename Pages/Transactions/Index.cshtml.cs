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
    public class TransactionsIndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public TransactionsIndexModel(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();
        public SelectList CategoriesSelectList { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            // Получение категорий
            var categories = await _context.Categories
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Name)
                .ToListAsync();

            CategoriesSelectList = new SelectList(categories, "Id", "Name");
            

            // Считаем расходы за текущий месяц
            var now = DateTime.UtcNow;
            var monthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var monthEnd = monthStart.AddMonths(1);

            var expenses = await _context.Transactions
                .Where(t => t.UserId == userId &&
                            t.Type == TransactionType.Expense &&
                            t.Date >= monthStart && t.Date < monthEnd)
                .ToListAsync();

            

            // Загружаем транзакции с учётом фильтра по категории
            var query = _context.Transactions
                .Include(t => t.Category)
                .Where(t => t.UserId == userId);

            if (CategoryId.HasValue)
            {
                query = query.Where(t => t.CategoryId == CategoryId.Value);
            }

            Transactions = await query
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }
    }
}
