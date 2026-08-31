using HRManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.Interfaces
{
    public interface IPayrollService
    {
        Task<PagedResultDto<PayrollDto>> GetAllAsync(string? search,int pageNumber,int pageSize,int? employeeId,bool? isPaid,string? sortBy);
        Task<PayrollDto?> GetByIdAsync(int id);

        Task<PayrollDto> CreateAsync(PayrollCreateDto dto);

        Task<bool> UpdateAsync(int id, PayrollUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
