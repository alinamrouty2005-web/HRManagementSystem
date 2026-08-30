using HRManagement.Core.DTOs;
using HRManagement.Core.Entities;
using HRManagement.Core.Interfaces;
using HRManagement.DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using HRManagement.Core.Exceptions;


namespace HRManagement.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ILogger<EmployeeService> _logger;

        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository,ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all employees.");

            var employees = await _employeeRepository.GetAllAsync();

            _logger.LogInformation("Retrieved {Count} employees.",employees.Count);

            return employees.Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    HireDate = e.HireDate,
                    DepartmentId = e.DepartmentId
                }).ToList();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting employee with ID {EmployeeId}", id);

            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                _logger.LogWarning("Employee with ID {EmployeeId} was not found", id);
                return null;
            }

            _logger.LogInformation("Employee with ID {EmployeeId} retrieved successfully", id);

            return employee;
        }

        public async Task<EmployeeDto> CreateAsync(EmployeeCreateDto dto)
        {
            _logger.LogInformation("Creating employee {FirstName} {LastName}.",dto.FirstName,dto.LastName);

            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                HireDate = dto.HireDate,
                DepartmentId = dto.DepartmentId
            };

            await _employeeRepository.AddAsync(employee);

            _logger.LogInformation("Employee created successfully with ID {EmployeeId}.",employee.EmployeeId);

            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HireDate = employee.HireDate,
                DepartmentId = employee.DepartmentId
            };
        }

        public async Task UpdateAsync(int id, EmployeeUpdateDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
                return;

            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.PhoneNumber = dto.PhoneNumber;
            employee.HireDate = dto.HireDate;
            employee.DepartmentId = dto.DepartmentId;

            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting employee with ID {EmployeeId}.",id);

            var employee =await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
            {
                _logger.LogWarning("Cannot delete employee with ID {EmployeeId} because it was not found.",id);

                return;
            }

            await _employeeRepository.DeleteAsync(employee);

            _logger.LogInformation("Employee with ID {EmployeeId} deleted successfully.",id);
        }

        public async Task<List<Employee>> GetByDepartmentAsync(int departmentId)
        {
            var employees = await _employeeRepository.GetAllAsync();

            return employees.Where(e => e.DepartmentId == departmentId).OrderBy(e => e.HireDate).ToList();
        }

        public async Task<List<Employee>> GetAllSortedByHireDateAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            return employees.OrderBy(e => e.HireDate).ToList();
        }

        public async Task<int> GetEmployeeCountAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            return employees.Count();
        }
    }
}