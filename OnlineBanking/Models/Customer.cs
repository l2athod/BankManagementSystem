using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBanking.Models
{
    public class Customer
    {
        [Key]
        public long CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        // Format: yyyy-MM-dd hh:mm:ss
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PinCode { get; set; } = null!;
        public string Gender { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", ErrorMessage = "Invalid phone number")]
        public string? PhoneNo { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;
        [Display(Name = "Account")]
        public long AccountId { get; set; }
        //[ForeignKey("AccountId")]
        //public virtual Account Account { get; set; }

        [Display(Name = "Role")]
        public int RoleId { get; set; }
        //[ForeignKey("role")]
        //public virtual Role role { get; set; }
    }
}
