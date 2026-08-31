using HRManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.Interfaces
{
    public interface IEmployeeProfileService
    {
        Task<PagedResultDto<EmployeeProfileDto>> GetAllAsync(
            string? search,
            int pageNumber,
            int pageSize,
            int? employeeId,
            string? sortBy);
        Task<EmployeeProfileDto?> GetByIdAsync(int id);

        Task<EmployeeProfileDto?> GetByEmployeeIdAsync(int employeeId);

        Task<EmployeeProfileDto> CreateAsync(EmployeeProfileCreateDto dto);

        Task<bool> UpdateAsync(int id, EmployeeProfileUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
