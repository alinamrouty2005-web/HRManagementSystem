using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.DTOs
{
    public class EmployeeProjectDto
    {
        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }

        public string? Role { get; set; }

        public DateTime AssignedDate { get; set; }
    }
}
