using System;
using System.ComponentModel.DataAnnotations;

namespace ST10062630_PROG6212_POE.Models
{
    // ST10062630_PROG6212_POE > Models > Claim.cs
    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }

        [Required]
        public string Lecturer { get; set; }

        [Required]
        public int HoursWorked { get; set; }

        [Required]
        public decimal HourlyRate { get; set; }

        public decimal Amount { get; set; }

        public string Notes { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        public string Month { get; set; }

        public string DocumentPath { get; set; }

        public DateTime SubmittedDate { get; set; } = DateTime.Now;

        // Corrected property name
        public string RoleName { get; set; } // Updated to match the database column name

        public string Role { get; set; } // This stores the role name (e.g., "Lecturer")
    }

}
