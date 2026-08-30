using HRManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.Interfaces
{
    public interface IAttendanceService
    {
        Task<List<AttendanceDto>> GetAllAsync();

        Task<AttendanceDto?> GetByIdAsync(int id);

        Task<AttendanceDto> CreateAsync(AttendanceCreateDto dto);

        Task<bool> UpdateAsync(int id,AttendanceUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
