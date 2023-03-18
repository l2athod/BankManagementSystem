using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBanking.Models
{
    public class AccountTransaction
    {
        [Key]
        public TransactionModel transactionModel { get; set; } = new TransactionModel();
        [NotMapped]
        public List<SelectListItem> accounts { get; set; } = new List<SelectListItem>();
        [NotMapped]
        public Dictionary<string, string> AccountBalance { get; set; } = new Dictionary<string, string>();
    }
}
