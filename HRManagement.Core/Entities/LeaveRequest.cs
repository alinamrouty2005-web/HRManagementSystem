using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.Entities
{
    public class LeaveRequest
    {
        public int LeaveRequestId { get; set; }

        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Range(1, int.MaxValue)]
        public int LeaveTypeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(500)]
        public string? Reason { get; set; }

        public bool IsApproved { get; set; }

        // Navigation Properties
        public Employee Employee { get; set; } = null!;

        public LeaveType LeaveType { get; set; } = null!;
    }
}
