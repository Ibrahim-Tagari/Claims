using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10062630_PROG6212_POE.Models
{
    public class Approval
    {
        [Key]
        public int ApprovalId { get; set; }

        [Required]
        public int ClaimId { get; set; }

        [Required]
        public string ApprovedBy { get; set; }

        [Required]
        public string ApprovalStatus { get; set; }

        public DateTime ApprovalDate { get; set; } = DateTime.Now;

        [ForeignKey("ClaimId")]
        public Claim Claim { get; set; }
    }
}
