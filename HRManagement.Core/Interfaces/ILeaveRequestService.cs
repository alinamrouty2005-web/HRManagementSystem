using HRManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<PagedResultDto<LeaveRequestDto>> GetAllAsync(string? search,int pageNumber,int pageSize,int? employeeId,int? leaveTypeId,bool? isApproved,string? sortBy);
        Task<LeaveRequestDto?> GetByIdAsync(int id);

        Task<LeaveRequestDto> CreateAsync(LeaveRequestCreateDto dto);

        Task<bool> UpdateAsync(int id, LeaveRequestUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
