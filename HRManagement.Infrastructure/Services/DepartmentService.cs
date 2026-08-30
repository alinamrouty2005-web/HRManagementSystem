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
    public class DepartmentService : IDepartmentService
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentService(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();

            return departments.Select(d => new DepartmentDto
                {
                    DepartmentId = d.DepartmentId,
                    Name = d.Name,
                    Description = d.Description
                }).ToList();
        }

        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            var department =await _departmentRepository.GetByIdAsync(id);

            if (department is null)
                return null;

            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                Description = department.Description
            };
        }

        public async Task<DepartmentDto> CreateAsync(DepartmentCreateDto dto)
        {
            var department = new Department
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _departmentRepository.AddAsync(department);

            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                Description = department.Description
            };
        }

        public async Task<bool> UpdateAsync(int id,DepartmentUpdateDto dto)
        {
            var department =await _departmentRepository.GetByIdAsync(id);

            if (department is null)
                return false;

            department.Name = dto.Name;
            department.Description = dto.Description;

            await _departmentRepository.UpdateAsync(department);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department =await _departmentRepository.GetByIdAsync(id);

            if (department is null)
                return false;

            await _departmentRepository.DeleteAsync(department);

            return true;
        }
    }
}
