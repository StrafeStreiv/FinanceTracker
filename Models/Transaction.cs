using System.ComponentModel.DataAnnotations;

namespace FinanceTrackerWeb.Models
{
    public enum TransactionType
    {
        Income,  // Доход
        Expense  // Расход
    }

    public class Transaction
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Сумма обязательна")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Сумма должна быть больше 0")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Дата обязательна")]
        public DateTime Date { get; set; } = DateTime.Now;

        [StringLength(200, ErrorMessage = "Описание не должно превышать 200 символов")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Категория обязательна")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public string? UserId { get; set; }
        public TransactionType Type { get; set; }
        public User? User { get; set; }
    }
}
