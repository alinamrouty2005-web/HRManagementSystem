using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.DTOs
{
    public class EmployeeProjectCreateDto
    {
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Range(1, int.MaxValue)]
        public int ProjectId { get; set; }

        [StringLength(100)]
        public string? Role { get; set; }

        [Required]
        public DateTime AssignedDate { get; set; }
    }
}
