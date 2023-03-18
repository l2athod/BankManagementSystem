using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
