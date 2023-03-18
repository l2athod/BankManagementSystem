using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Models
{
    public class TransactionModel
    {
        [Key]
        public long TransactionId { get; set; }

        [RegularExpression(@"^(credit|debit)$", ErrorMessage = "TransactionType must be either 'credit' or 'debit'.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "TransactionType must be either 'credit' or 'debit'.")]
        public string TransactionType { get; set; }
        public string Description { get; set; }

        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "TransferAmount must be valid amount.")]
        public decimal TransferAmount { get; set; }
        public DateTime DateOfTransaction { get; set; } = DateTime.Now;
        [Required]
        public long CustomerId { get; set; }

        [Required(ErrorMessage = "ToAccountNumber Required")]
        public string FromAccountNumber { get; set; } = null!;

        [Required(ErrorMessage = "ToAccountNumber Required")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "ToAccountNumber must have 11 digits")]
        public string ToAccountNumber { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
