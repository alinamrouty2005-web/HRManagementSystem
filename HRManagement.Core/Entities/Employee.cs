using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [Required]
        public DateTime HireDate { get; set; }
        
        
        public User User { get; set; } = null!;
        
        [Range(1, int.MaxValue)]
        public int DepartmentId { get; set; }
       
        // Navigation Property
        public Department Department { get; set; } = null!;
       
        // Relationships
        public EmployeeProfile EmployeeProfile { get; set; } = null!;
       
        public ICollection<Attendance> Attendances { get; set; }
            = new List<Attendance>();

        public ICollection<LeaveRequest> LeaveRequests { get; set; }
            = new List<LeaveRequest>();

        public ICollection<PerformanceReview> PerformanceReviews { get; set; }
            = new List<PerformanceReview>();

        public ICollection<EmployeePosition> EmployeePositions { get; set; }
            = new List<EmployeePosition>();

        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
            = new List<EmployeeProject>();
        public ICollection<PerformanceReview> ReviewsGiven { get; set; }
            = new List<PerformanceReview>();

        public ICollection<Salary> Salaries { get; set; }
            = new List<Salary>();

        public ICollection<Payroll> Payrolls { get; set; }
            = new List<Payroll>();
    }
}