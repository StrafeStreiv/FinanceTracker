using FinanceTrackerWeb.Data;
using FinanceTrackerWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerWeb.Pages.Categories
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

        public IList<Category> Categories { get; set; } = new List<Category>();

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            Categories = await _context.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();
            
        }
    }
}
