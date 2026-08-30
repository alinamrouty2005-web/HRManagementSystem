using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.DTOs
{
    public class PayrollUpdateDto
    {
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Range(0, 1000000)]
        public decimal GrossSalary { get; set; }

        [Range(0, 1000000)]
        public decimal TotalDeductions { get; set; }

        [Range(0, 1000000)]
        public decimal NetSalary { get; set; }

        [Required]
        public DateTime PayrollDate { get; set; }

        public bool IsPaid { get; set; }
    }
}
