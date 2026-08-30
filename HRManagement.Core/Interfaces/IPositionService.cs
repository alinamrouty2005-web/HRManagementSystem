using System;
using System.Collections.Generic;
using System.Text;
using HRManagement.Core.DTOs;


namespace HRManagement.Core.Interfaces
{
    public interface IPositionService
    {
        Task<List<PositionDto>> GetAllAsync();

        Task<PositionDto?> GetByIdAsync(int id);

        Task<PositionDto> CreateAsync(PositionCreateDto dto);

        Task<bool> UpdateAsync(int id, PositionUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
