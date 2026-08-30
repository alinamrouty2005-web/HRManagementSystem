using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.Entities
{
    public class LeaveType
    {
        [Required]
        [StringLength(100)]
        public int LeaveTypeId { get; set; }

        public string Name { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Description { get; set; }

        // Navigation Property
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
            = new List<LeaveRequest>();
    }
}
