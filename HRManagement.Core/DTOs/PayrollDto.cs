using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.DTOs
{
    public class PayrollDto
    {
        public int PayrollId { get; set; }

        public int EmployeeId { get; set; }

        public decimal GrossSalary { get; set; }

        public decimal TotalDeductions { get; set; }

        public decimal NetSalary { get; set; }

        public DateTime PayrollDate { get; set; }

        public bool IsPaid { get; set; }
    }
}
