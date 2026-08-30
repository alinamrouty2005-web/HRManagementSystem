using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Navigation Property
        public ICollection<Employee> Employees { get; set; }
            = new List<Employee>();
    }
}
