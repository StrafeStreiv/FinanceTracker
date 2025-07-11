namespace FinanceTrackerWeb.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = "Expense"; // "Income" или "Expense"
        public string UserId { get; set; } = string.Empty;
        public decimal? MonthlyLimit { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        
    }
}
