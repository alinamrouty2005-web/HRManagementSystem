using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.Entities
{
    public class EmployeePosition
    {
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Range(1, int.MaxValue)]
        public int PositionId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        // Navigation Properties
        public Employee Employee { get; set; } = null!;

        public Position Position { get; set; } = null!;
    }
}
