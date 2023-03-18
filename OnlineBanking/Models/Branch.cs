using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Models
{
    public class Branch
    {
        [Key]
        public int BrnachId { get; set; }
        [Required]
        public string BranchName { get; set; } = null!;
        public string BranchCity { get; set; } = null!;
        public string BranchState { get; set; } = null!;
        public bool IsActive { get; set; }

    }
}
