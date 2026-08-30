using Microsoft.AspNetCore.Mvc;
using HRManagement.Core.DTOs;
using HRManagement.Core.Interfaces;

namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetAllAsync();

            return Ok(departments);
        }

        // GET: api/Departments/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);

            if (department is null)
                return NotFound();

            return Ok(department);
        }

        // POST: api/Departments
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentCreateDto dto)
        {
            var department = await _departmentService.CreateAsync(dto);

            return Ok(department);
        }

        // PUT: api/Departments/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,DepartmentUpdateDto dto)
        {
            var result = await _departmentService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        // DELETE: api/Departments/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =await _departmentService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}

