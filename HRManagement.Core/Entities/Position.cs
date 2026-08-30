using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.Entities
{
    public class Position
    {
        public int PositionId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal BaseSalary { get; set; }

        // Navigation Property
        public ICollection<EmployeePosition> EmployeePositions { get; set; }
            = new List<EmployeePosition>();
    }
}
