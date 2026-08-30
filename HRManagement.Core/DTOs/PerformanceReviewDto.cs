using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.DTOs
{
    public class PerformanceReviewDto
    {
        public int PerformanceReviewId { get; set; }

        public int EmployeeId { get; set; }

        public int ReviewerId { get; set; }

        public decimal Score { get; set; }

        public string? Comments { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}
