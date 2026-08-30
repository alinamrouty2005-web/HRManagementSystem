using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.Entities
{
    public class Attendance
    {
        public int AttendanceId { get; set; }

        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public DateTime? CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public bool IsPresent { get; set; }

        // Navigation Property
        public Employee Employee { get; set; } = null!;
    }
}
