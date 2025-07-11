using FinanceTrackerWeb.Data;
using FinanceTrackerWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerWeb.Pages.Transactions
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Transaction Transaction { get; set; } = default!;

        public SelectList Categories { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Загружаем транзакцию и категории
            Transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            Categories = new SelectList(await _context.Categories.ToListAsync(),
                                      nameof(Category.Id),
                                      nameof(Category.Name));

            if (Transaction == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = new SelectList(await _context.Categories.ToListAsync(), nameof(Category.Id), nameof(Category.Name));
                return Page();
            }

            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == Transaction.Id);
            if (transaction == null)
                return NotFound();

            transaction.Amount = Transaction.Amount;
            transaction.Date = Transaction.Date;
            transaction.Type = Transaction.Type;
            transaction.CategoryId = Transaction.CategoryId;
            transaction.Description = Transaction.Description;

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }


        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(t => t.Id == id);
        }
    }
}