using Microsoft.AspNetCore.Mvc;
using HRManagement.Core.Entities;
using HRManagement.Core.Interfaces;
using HRManagement.Core.DTOs;
using Microsoft.AspNetCore.Authorization;


namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();

            return Ok(employees);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            if (employee is null)
                return NotFound();

            return Ok(employee);
        }

        // GET: api/Employees/department/1
        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetByDepartment(int departmentId)
        {
            var employees = await _employeeService.GetByDepartmentAsync(departmentId);

            return Ok(employees);
        }

        // GET: api/Employees/count
        [HttpGet("count")]
        public async Task<IActionResult> GetEmployeeCount()
        {
            var count = await _employeeService.GetEmployeeCountAsync();

            return Ok(count);
        }

        // GET: api/Employees/sorted-by-hire-date
        [HttpGet("sorted-by-hire-date")]
        public async Task<IActionResult> GetAllSortedByHireDate()
        {
            var employees = await _employeeService.GetAllSortedByHireDateAsync();

            return Ok(employees);
        }

        // POST: api/Employees
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateDto dto)
        {
            var employee = await _employeeService.CreateAsync(dto);

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,EmployeeUpdateDto dto)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            if (employee is null)
                return NotFound();

            await _employeeService.UpdateAsync(id, dto);

            return Ok();
        }

        // DELETE: api/Employees/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            if (employee is null)
                return NotFound();

            await _employeeService.DeleteAsync(id);

            return Ok();
        }
    }
}

