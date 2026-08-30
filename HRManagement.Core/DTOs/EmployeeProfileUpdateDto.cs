using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.DTOs
{
    public class EmployeeProfileUpdateDto
    {
        [StringLength(250)]
        public string? Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(100)]
        public string? Nationality { get; set; }

        [StringLength(50)]
        public string? EmergencyContact { get; set; }

        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }
    }
}
