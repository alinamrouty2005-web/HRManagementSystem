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

        public async Task<List<EmployeeProjectDto>> GetAllAsync()
        {
            var employeeProjects =await _repository.GetAllAsync();

            return employeeProjects.Select(ep => new EmployeeProjectDto
                {
                    EmployeeId = ep.EmployeeId,
                    ProjectId = ep.ProjectId,
                    Role = ep.Role,
                    AssignedDate = ep.AssignedDate
                }).ToList();
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
