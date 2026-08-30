using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRManagement.Core.DTOs
{
    public class PositionCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Description { get; set; }

        [Range(0, 1000000)]
        public decimal BaseSalary { get; set; }
    }
}
