using HRManagement.Core.DTOs;
using HRManagement.Core.Entities;
using HRManagement.Core.Interfaces;
using HRManagement.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using HRManagement.Core.Exceptions;

namespace HRManagement.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectRepository _projectRepository;

        public ProjectService(ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<PagedResultDto<ProjectDto>> GetAllAsync(
     string? search,
     int pageNumber,
     int pageSize,
     string? sortBy)
        {
            var projects =
                await _projectRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                projects = projects
                    .Where(p =>
                        p.Name.Contains(search) ||
                        (p.Description != null &&
                         p.Description.Contains(search)))
                    .ToList();
            }

            if (sortBy == "name")
            {
                projects = projects
                    .OrderBy(p => p.Name)
                    .ToList();
            }
            else if (sortBy == "startDate")
            {
                projects = projects
                    .OrderBy(p => p.StartDate)
                    .ToList();
            }
            else
            {
                projects = projects
                    .OrderBy(p => p.ProjectId)
                    .ToList();
            }

            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 10;

            var totalCount = projects.Count();

            var result = projects
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProjectDto
                {
                    ProjectId = p.ProjectId,
                    Name = p.Name,
                    Description = p.Description,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate
                })
                .ToList();

            return new PagedResultDto<ProjectDto>
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = result
            };
        }

        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project is null)
                return null;

            return new ProjectDto
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate
            };
        }

        public async Task<ProjectDto> CreateAsync(ProjectCreateDto dto)
        {
            if (dto.EndDate.HasValue && dto.EndDate.Value < dto.StartDate)
            {
                throw new ValidationException("End date cannot be before start date.");
            }

            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };

            await _projectRepository.AddAsync(project);

            return new ProjectDto
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate
            };
        }

        public async Task<bool> UpdateAsync(int id,ProjectUpdateDto dto)
        {
            if (dto.EndDate.HasValue && dto.EndDate.Value < dto.StartDate)
            {
                throw new ValidationException("End date cannot be before start date.");
            }

            var project = await _projectRepository.GetByIdAsync(id);

            if (project is null)
                return false;

            project.Name = dto.Name;
            project.Description = dto.Description;
            project.StartDate = dto.StartDate;
            project.EndDate = dto.EndDate;

            await _projectRepository.UpdateAsync(project);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project =await _projectRepository.GetByIdAsync(id);

            if (project is null)
                return false;

            await _projectRepository.DeleteAsync(project);

            return true;
        }
    }
}
