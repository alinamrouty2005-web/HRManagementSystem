using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.Entities
{
    public class PerformanceReview
    {
        public int PerformanceReviewId { get; set; }

        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Range(1, int.MaxValue)]
        public int ReviewerId { get; set; }

        [Range(0, 100)]
        public decimal Score { get; set; }

        [StringLength(500)]
        public string? Comments { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }

        public Employee Employee { get; set; } = null!;

        public Employee Reviewer { get; set; } = null!;
    }
}
