using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.Entities
{
    public class Salary
    {
        public int SalaryId { get; set; }

        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Range(0, 1000000)]
        public decimal BasicSalary { get; set; }

        [Range(0, 1000000)]
        public decimal Allowances { get; set; }

        [Range(0, 1000000)]
        public decimal Deductions { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        // Navigation Property
        public Employee Employee { get; set; } = null!;
    }
}
