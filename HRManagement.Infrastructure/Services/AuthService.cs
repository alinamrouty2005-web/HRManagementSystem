using HRManagement.Core.DTOs;
using HRManagement.Core.Entities;
using HRManagement.Core.Interfaces;
using HRManagement.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HRManagement.Core.Exceptions;


namespace HRManagement.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(UserRepository userRepository,IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser =await _userRepository.GetByUsernameAsync(dto.Username);

            if (existingUser is not null)
                throw new ValidationException("Username already exists.");

            var user = new User
            {
                Username = dto.Username,
                PasswordHash =BCrypt.Net.BCrypt.HashPassword(dto.Password),
                EmployeeId = dto.EmployeeId,
                IsActive = true
            };

            await _userRepository.AddAsync(user);

            const int defaultRoleId = 3;

            await _userRepository.AddUserRoleAsync(user.UserId, defaultRoleId);

            return CreateToken(user, defaultRoleId);
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var user =await _userRepository.GetByUsernameAsync(dto.Username);

            if (user is null)
                throw new ValidationException("Invalid username or password.");

            if (!user.IsActive)
                throw new ValidationException("User is inactive.");

            var passwordValid =BCrypt.Net.BCrypt.Verify(dto.Password,user.PasswordHash);

            if (!passwordValid)
                throw new ValidationException("Invalid username or password.");

            var roleId = user.UserRoles.Select(ur => ur.RoleId).FirstOrDefault();

            return CreateToken(user, roleId);
        }

        private LoginResponseDto CreateToken(User user,int roleId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),

                new Claim(ClaimTypes.Name,user.Username),

                new Claim(ClaimTypes.Role,GetRoleName(roleId))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:DurationInMinutes"]!)),
                signingCredentials: credentials);

            return new LoginResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        private string GetRoleName(int roleId)
        {
            return roleId switch
            {
                1 => "Admin",
                2 => "HR",
                3 => "Employee",
                _ => "Employee"
            };
        }
    }
}
