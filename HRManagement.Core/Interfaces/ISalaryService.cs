using HRManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.Interfaces
{
    public interface ISalaryService
    {
        Task<List<SalaryDto>> GetAllAsync();

        Task<SalaryDto?> GetByIdAsync(int id);

        Task<SalaryDto> CreateAsync(SalaryCreateDto dto);

        Task<bool> UpdateAsync(int id, SalaryUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
