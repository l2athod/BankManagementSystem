using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Models
{
    public class TransactionModel
    {
        [Key]
        public long TransactionId { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }

        public decimal TransferAmount { get; set; }
        public DateTime DateOfTransaction { get; set; } = DateTime.Now;
        [Required]
        public long CustomerId { get; set; }
        [Required]
        [StringLength(11)]
        [Display(Name = "Your AccountNumber")]
        public string FromAccountNumber { get; set; }
        [Required]
        [StringLength(11)]
        [Display(Name = "Beneficiary AccountNumber")]
        //[ValidateModel(ErrorMessage = "FromAccountNumber and ToAccountNumber cannot be the same.")]
        public string ToAccountNumber { get; set; } = null!;
        public bool IsActive { get; set; }
    }

    public class NotSameAccountAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (TransactionModel)validationContext.ObjectInstance;

            if (model.FromAccountNumber == model.ToAccountNumber)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
