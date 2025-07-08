using Microsoft.AspNetCore.Identity;

namespace FinanceTrackerWeb.Models
{
    public class User : IdentityUser
    {
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
