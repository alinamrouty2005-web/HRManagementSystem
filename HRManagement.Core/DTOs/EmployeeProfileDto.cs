using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.DTOs
{
    public class EmployeeProfileDto
    {
        public int EmployeeProfileId { get; set; }

        public string? Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Nationality { get; set; }

        public string? EmergencyContact { get; set; }

        public int EmployeeId { get; set; }
    }
}
