using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        // Navigation Properties
        public Employee Employee { get; set; } = null!;

        public ICollection<UserRole> UserRoles { get; set; }
            = new List<UserRole>();
    }
}
