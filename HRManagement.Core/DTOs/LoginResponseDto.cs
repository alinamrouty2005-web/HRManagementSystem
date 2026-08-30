using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.DTOs
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
    }
}
