using HRManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.Interfaces
{
    public interface IPerformanceReviewService
    {
        Task<List<PerformanceReviewDto>> GetAllAsync();

        Task<PerformanceReviewDto?> GetByIdAsync(int id);

        Task<PerformanceReviewDto> CreateAsync(PerformanceReviewCreateDto dto);

        Task<bool> UpdateAsync(int id,PerformanceReviewUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
