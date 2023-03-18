using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        [StringLength(11)]
        public string AccountNumber { get; set; } = null!;
        [Required]
        public double AccountBalance { get; set; }
        public string DateOfCreation { get; set; } = null!;
        public string Accounttype { get; set; }
        [Required]
        [StringLength(11)]
        public string IFSCCode { get; set; }
        public int BranchId { get; set; }
        public string AccountStatus { get; set; }
        public bool IsActive { get; set; }

    }
}
