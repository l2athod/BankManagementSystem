using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Models
{
    public class UserDetail
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string Gender { get; set; }
        public long PhoneNo { get; set; }
        public string Email { get; set; }
    }
}
