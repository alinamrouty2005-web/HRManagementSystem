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
    public class EmployeeProjectService : IEmployeeProjectService
    {
        private readonly EmployeeProjectRepository _repository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly ProjectRepository _projectRepository;

        public EmployeeProjectService(EmployeeProjectRepository repository,EmployeeRepository employeeRepository,ProjectRepository projectRepository)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
        }

        public async Task<PagedResultDto<EmployeeProjectDto>> GetAllAsync(
     string? search,
     int pageNumber,
     int pageSize,
     int? employeeId,
     int? projectId,
     string? sortBy)
        {
            var employeeProjects =
                await _repository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                employeeProjects = employeeProjects
                    .Where(ep =>
                        ep.Role != null &&
                        ep.Role.Contains(search))
                    .ToList();
            }

            if (employeeId.HasValue)
            {
                employeeProjects = employeeProjects
                    .Where(ep => ep.EmployeeId == employeeId.Value)
                    .ToList();
            }

            if (projectId.HasValue)
            {
                employeeProjects = employeeProjects
                    .Where(ep => ep.ProjectId == projectId.Value)
                    .ToList();
            }

            if (sortBy == "assignedDate")
            {
                employeeProjects = employeeProjects
                    .OrderBy(ep => ep.AssignedDate)
                    .ToList();
            }
            else
            {
                employeeProjects = employeeProjects
                    .OrderBy(ep => ep.EmployeeId)
                    .ToList();
            }

            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 10;

            var totalCount = employeeProjects.Count();

            var result = employeeProjects
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ep => new EmployeeProjectDto
                {
                    EmployeeId = ep.EmployeeId,
                    ProjectId = ep.ProjectId,
                    Role = ep.Role,
                    AssignedDate = ep.AssignedDate
                })
                .ToList();

            return new PagedResultDto<EmployeeProjectDto>
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = result
            };
        }

        public async Task<EmployeeProjectDto?> GetByIdAsync(int employeeId,int projectId)
        {
            var employeeProject =await _repository.GetByIdAsync(employeeId,projectId);

            if (employeeProject is null)
                return null;

            return new EmployeeProjectDto
            {
                EmployeeId = employeeProject.EmployeeId,
                ProjectId = employeeProject.ProjectId,
                Role = employeeProject.Role,
                AssignedDate = employeeProject.AssignedDate
            };
        }

        public async Task<EmployeeProjectDto> CreateAsync(EmployeeProjectCreateDto dto)
        {
            var employee =await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                throw new ValidationException("Employee does not exist.");

            var project = await _projectRepository.GetByIdAsync(dto.ProjectId);

            if (project is null)
                throw new ValidationException("Project does not exist.");

            if (dto.AssignedDate > DateTime.Now)
                throw new ValidationException("Assigned date cannot be in the future.");

            var existing =await _repository.GetByIdAsync(dto.EmployeeId,dto.ProjectId);

            if (existing is not null)
                throw new ValidationException("This employee is already assigned to this project.");

            var employeeProject = new EmployeeProject
            {
                EmployeeId = dto.EmployeeId,
                ProjectId = dto.ProjectId,
                Role = dto.Role,
                AssignedDate = dto.AssignedDate
            };

            await _repository.AddAsync(employeeProject);

            return new EmployeeProjectDto
            {
                EmployeeId = employeeProject.EmployeeId,
                ProjectId = employeeProject.ProjectId,
                Role = employeeProject.Role,
                AssignedDate = employeeProject.AssignedDate
            };
        }

        public async Task<bool> UpdateAsync(int employeeId,int projectId,EmployeeProjectUpdateDto dto)
        {
            var employeeProject =await _repository.GetByIdAsync(employeeId,projectId);

            if (employeeProject is null)
                return false;

            var employee =await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                throw new ValidationException("Employee does not exist.");

            var project =await _projectRepository.GetByIdAsync(dto.ProjectId);

            if (project is null)
                throw new ValidationException("Project does not exist.");

            if (dto.AssignedDate > DateTime.Now)
                throw new ValidationException("Assigned date cannot be in the future.");

            employeeProject.EmployeeId = dto.EmployeeId;
            employeeProject.ProjectId = dto.ProjectId;
            employeeProject.Role = dto.Role;
            employeeProject.AssignedDate = dto.AssignedDate;

            await _repository.UpdateAsync(employeeProject);

            return true;
        }

        public async Task<bool> DeleteAsync(int employeeId,int projectId)
        {
            var employeeProject =await _repository.GetByIdAsync(employeeId,projectId);

            if (employeeProject is null)
                return false;

            await _repository.DeleteAsync(employeeProject);

            return true;
        }
    }
}
