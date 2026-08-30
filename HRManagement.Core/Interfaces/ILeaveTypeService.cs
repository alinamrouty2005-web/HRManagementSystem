using HRManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.Interfaces
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeDto>> GetAllAsync();

        Task<LeaveTypeDto?> GetByIdAsync(int id);

        Task<LeaveTypeDto> CreateAsync(LeaveTypeCreateDto dto);

        Task<bool> UpdateAsync(int id, LeaveTypeUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
