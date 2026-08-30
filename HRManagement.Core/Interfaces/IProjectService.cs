using HRManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetAllAsync();

        Task<ProjectDto?> GetByIdAsync(int id);

        Task<ProjectDto> CreateAsync(ProjectCreateDto dto);

        Task<bool> UpdateAsync(int id, ProjectUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
