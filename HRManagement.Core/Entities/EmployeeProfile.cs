using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.Entities
{
    public class EmployeeProfile
    {

        public int EmployeeProfileId { get; set; }

        [StringLength(250)]

        public string? Address { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(100)]
        public string? Nationality { get; set; }

        [StringLength(50)]
        public string? EmergencyContact { get; set; }

        public int EmployeeId { get; set; }

        // Navigation Property
        public Employee Employee { get; set; } = null!;
    }
}
