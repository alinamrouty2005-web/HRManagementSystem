using HRManagement.Core.DTOs;
using HRManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace HRManagement.Core.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllAsync();

        Task<DepartmentDto?> GetByIdAsync(int id);

        Task<DepartmentDto> CreateAsync(DepartmentCreateDto dto);

        Task<bool> UpdateAsync(int id, DepartmentUpdateDto dto);

        Task<bool> DeleteAsync(int id);

    }
}
    

