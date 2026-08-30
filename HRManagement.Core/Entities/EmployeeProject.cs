using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.Entities
{
    public class EmployeeProject
    {
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Range(1, int.MaxValue)]
        public int ProjectId { get; set; }

        [StringLength(100)]
        public string? Role { get; set; }

        [Required]
        public DateTime AssignedDate { get; set; }

        // Navigation Properties
        public Employee Employee { get; set; } = null!;

        public Project Project { get; set; } = null!;
    }
}
