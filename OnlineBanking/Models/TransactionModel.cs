using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBanking.Models
{
    public class TransactionModel
    {
        [Key]
        public long TransactionId { get; set; }
        public string TransactionType { get; set; } = "debit";
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
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
    }
}
