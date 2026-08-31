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

        public async Task<PagedResultDto<DepartmentDto>> GetAllAsync(string? search,int pageNumber,int pageSize,string? sortBy)
        {
            var departments =await _departmentRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                departments = departments.Where(d => d.Name.Contains(search)).ToList();
            }

            if (sortBy == "name")
            {
                departments = departments.OrderBy(d => d.Name).ToList();
            }
            else
            {
                departments = departments.OrderBy(d => d.DepartmentId).ToList();
            }

            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 10;

            var totalCount = departments.Count();

            var result = departments.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Select(d => new DepartmentDto
                {
                    DepartmentId = d.DepartmentId,
                    Name = d.Name,
                    Description = d.Description
                }).ToList();

            return new PagedResultDto<DepartmentDto>
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = result
            };
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
