using OnlineBanking.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBanking.Models
{
    public class Login
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Display(Name = "Role")]
        public string RoleType { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public bool IsActive { get; set; }
    }
}