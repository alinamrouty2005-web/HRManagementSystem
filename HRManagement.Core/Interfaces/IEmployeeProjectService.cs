using HRManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.Interfaces
{
    public interface IEmployeeProjectService
    {
        Task<PagedResultDto<EmployeeProjectDto>> GetAllAsync(
            string? search,
            int pageNumber,
            int pageSize,
            int? employeeId,
            int? projectId,
            string? sortBy);
        Task<EmployeeProjectDto?> GetByIdAsync(int employeeId,int projectId);

        Task<EmployeeProjectDto> CreateAsync(EmployeeProjectCreateDto dto);

        Task<bool> UpdateAsync(int employeeId,int projectId,EmployeeProjectUpdateDto dto);

        Task<bool> DeleteAsync(int employeeId,int projectId);
    }
}
