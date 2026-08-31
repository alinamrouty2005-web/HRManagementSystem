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
    public class SalaryService : ISalaryService
    {
        private readonly SalaryRepository _salaryRepository;
        private readonly EmployeeRepository _employeeRepository;

        public SalaryService( SalaryRepository salaryRepository, EmployeeRepository employeeRepository)
        {
            _salaryRepository = salaryRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<PagedResultDto<SalaryDto>> GetAllAsync(string? search,int pageNumber,int pageSize,int? employeeId,decimal? minSalary,decimal? maxSalary,string? sortBy)
        {
            var salaries =await _salaryRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search) &&int.TryParse(search, out int searchEmployeeId))
            {
                salaries = salaries.Where(s => s.EmployeeId == searchEmployeeId).ToList();
            }

            if (employeeId.HasValue)
            {
                salaries = salaries.Where(s => s.EmployeeId == employeeId.Value).ToList();
            }

            if (minSalary.HasValue)
            {
                salaries = salaries.Where(s => s.BasicSalary >= minSalary.Value).ToList();
            }

            if (maxSalary.HasValue)
            {
                salaries = salaries.Where(s => s.BasicSalary <= maxSalary.Value).ToList();
            }

            if (sortBy == "salary")
            {
                salaries = salaries.OrderBy(s => s.BasicSalary).ToList();
            }
            else if (sortBy == "effectiveDate")
            {
                salaries = salaries.OrderBy(s => s.EffectiveDate).ToList();
            }
            else
            {
                salaries = salaries.OrderBy(s => s.SalaryId).ToList();
            }

            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 10;

            var totalCount = salaries.Count();

            var result = salaries
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SalaryDto
                {
                    SalaryId = s.SalaryId,
                    EmployeeId = s.EmployeeId,
                    BasicSalary = s.BasicSalary,
                    Allowances = s.Allowances,
                    Deductions = s.Deductions,
                    EffectiveDate = s.EffectiveDate
                }).ToList();

            return new PagedResultDto<SalaryDto>
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = result
            };
        }

        public async Task<SalaryDto?> GetByIdAsync(int id)
        {
            var salary = await _salaryRepository.GetByIdAsync(id);

            if (salary is null)
                return null;

            return new SalaryDto
            {
                SalaryId = salary.SalaryId,
                EmployeeId = salary.EmployeeId,
                BasicSalary = salary.BasicSalary,
                Allowances = salary.Allowances,
                Deductions = salary.Deductions,
                EffectiveDate = salary.EffectiveDate
            };
        }

        public async Task<SalaryDto> CreateAsync(SalaryCreateDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                throw new ValidationException("Employee does not exist.");

            var salary = new Salary
            {
                EmployeeId = dto.EmployeeId,
                BasicSalary = dto.BasicSalary,
                Allowances = dto.Allowances,
                Deductions = dto.Deductions,
                EffectiveDate = dto.EffectiveDate
            };

            await _salaryRepository.AddAsync(salary);

            return new SalaryDto
            {
                SalaryId = salary.SalaryId,
                EmployeeId = salary.EmployeeId,
                BasicSalary = salary.BasicSalary,
                Allowances = salary.Allowances,
                Deductions = salary.Deductions,
                EffectiveDate = salary.EffectiveDate
            };
        }
        public async Task<bool> UpdateAsync(int id,SalaryUpdateDto dto)
        {
            var salary = await _salaryRepository.GetByIdAsync(id);

            if (salary is null)
                return false;

            var employee =await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                throw new ValidationException("Employee does not exist.");

            salary.EmployeeId = dto.EmployeeId;
            salary.BasicSalary = dto.BasicSalary;
            salary.Allowances = dto.Allowances;
            salary.Deductions = dto.Deductions;
            salary.EffectiveDate = dto.EffectiveDate;

            await _salaryRepository.UpdateAsync(salary);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var salary = await _salaryRepository.GetByIdAsync(id);

            if (salary is null)
                return false;

            await _salaryRepository.DeleteAsync(salary);

            return true;
        }
    }
}
