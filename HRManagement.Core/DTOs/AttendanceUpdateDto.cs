using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace HRManagement.Core.DTOs
{
    public class AttendanceUpdateDto
    {
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public DateTime? CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public bool IsPresent { get; set; }
    }
}
