using System;
using System.Collections.Generic;
using System.Text;
using HRManagement.Core.DTOs;


namespace HRManagement.Core.Interfaces
{
    public interface IPositionService
    {
        Task<PagedResultDto<PositionDto>> GetAllAsync(string? search,int pageNumber,int pageSize,string? sortBy,decimal? minSalary,decimal? maxSalary);

        Task<PositionDto?> GetByIdAsync(int id);

        Task<PositionDto> CreateAsync(PositionCreateDto dto);

        Task<bool> UpdateAsync(int id, PositionUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
