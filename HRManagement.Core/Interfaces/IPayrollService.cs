using HRManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.Interfaces
{
    public interface IPayrollService
    {
        Task<List<PayrollDto>> GetAllAsync();

        Task<PayrollDto?> GetByIdAsync(int id);

        Task<PayrollDto> CreateAsync(PayrollCreateDto dto);

        Task<bool> UpdateAsync(int id, PayrollUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
