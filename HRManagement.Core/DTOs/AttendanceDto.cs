using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.DTOs
{
    public class AttendanceDto
    {
        public int AttendanceId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime Date { get; set; }

        public DateTime? CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public bool IsPresent { get; set; }
    }
}
