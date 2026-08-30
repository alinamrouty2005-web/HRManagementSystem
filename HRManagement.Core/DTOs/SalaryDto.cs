using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.DTOs
{
    public class SalaryDto
    {
        public int SalaryId { get; set; }

        public int EmployeeId { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal Allowances { get; set; }

        public decimal Deductions { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
