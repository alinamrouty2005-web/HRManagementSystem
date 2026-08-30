using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.Entities
{
    public class UserRole
    {
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Range(1, int.MaxValue)]
        public int RoleId { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;

        public Role Role { get; set; } = null!;
    }
}
