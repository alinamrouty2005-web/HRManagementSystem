using HRManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.Interfaces
{
    public interface IAttendanceService
    {
        Task<PagedResultDto<AttendanceDto>> GetAllAsync(string? search,int pageNumber,int pageSize,int? employeeId,bool? isPresent,string? sortBy);

        Task<AttendanceDto?> GetByIdAsync(int id);

        Task<AttendanceDto> CreateAsync(AttendanceCreateDto dto);

        Task<bool> UpdateAsync(int id,AttendanceUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
