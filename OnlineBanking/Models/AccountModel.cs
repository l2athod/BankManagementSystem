using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Models
{
    public class AccountModel
    {
        public string? CustomerName { get; set; }
        public long AccountId { get; set; }

        [Required]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "Account number must be 11 digits")]
        public string AccountNumber { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "Account balance must be a numeric value")]
        public decimal AccountBalance { get; set; }
        [Required]

        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        [Required]


        public string AccountType { get; set; }
        [Required]
        public string Branch { get; set; }

        public string IFSCCode { get; set; }
        [Required]
        public bool Status { get; set; }

    }
}

