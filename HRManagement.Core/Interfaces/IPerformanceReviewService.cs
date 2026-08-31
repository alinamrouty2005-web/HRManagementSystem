using HRManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.Interfaces
{
    public interface IPerformanceReviewService
    {
        Task<PagedResultDto<PerformanceReviewDto>> GetAllAsync(
            string? search,
            int pageNumber,
            int pageSize,
            int? employeeId,
            int? reviewerId,
            decimal? minScore,
            decimal? maxScore,
            string? sortBy);
        Task<PerformanceReviewDto?> GetByIdAsync(int id);

        Task<PerformanceReviewDto> CreateAsync(PerformanceReviewCreateDto dto);

        Task<bool> UpdateAsync(int id,PerformanceReviewUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
