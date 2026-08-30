using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace HRManagement.Core.DTOs
{
    public class PositionDto
    {
        public int PositionId { get; set; }
       
        public string Title { get; set; } = string.Empty;
       
        public string? Description { get; set; }
       
        public decimal BaseSalary { get; set; }
    }
}
