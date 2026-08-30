using System;
using System.Collections.Generic;
using System.Text;
using HRManagement.Core.Entities;
using HRManagement.Core.DTOs;


namespace HRManagement.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllAsync();
        Task UpdateAsync(int id, EmployeeUpdateDto dto);
        Task DeleteAsync(int id);

        Task<List<Employee>> GetByDepartmentAsync(int departmentId);

        Task<List<Employee>> GetAllSortedByHireDateAsync();

        Task<int> GetEmployeeCountAsync();

        Task<EmployeeDto> CreateAsync(EmployeeCreateDto dto);


        Task<Employee?> GetByIdAsync(int id);

    }
}
