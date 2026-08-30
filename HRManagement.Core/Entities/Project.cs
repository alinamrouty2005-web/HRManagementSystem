using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        // Navigation Property
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
            = new List<EmployeeProject>();
    }
}
