using HRManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.Interfaces
{
    public interface IEmployeePositionService
    {
        Task<List<EmployeePositionDto>> GetAllAsync();

        Task<EmployeePositionDto?> GetByIdAsync(int employeeId, int positionId);

        Task<EmployeePositionDto> CreateAsync(EmployeePositionCreateDto dto);

        Task<bool> UpdateAsync(int employeeId, int positionId,EmployeePositionUpdateDto dto);

        Task<bool> DeleteAsync(int employeeId,int positionId);
    }
}
