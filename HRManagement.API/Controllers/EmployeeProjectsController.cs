using Microsoft.AspNetCore.Mvc;
using HRManagement.Core.DTOs;
using HRManagement.Core.Interfaces;

namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeProjectsController : Controller
    {
        private readonly IEmployeeProjectService _service;

        public EmployeeProjectsController(IEmployeeProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{employeeId}/{projectId}")]
        public async Task<IActionResult> GetById(int employeeId,int projectId)
        {
            var result =await _service.GetByIdAsync(employeeId,projectId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeProjectCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);

            return Ok(result);
        }

        [HttpPut("{employeeId}/{projectId}")]
        public async Task<IActionResult> Update(int employeeId,int projectId,EmployeeProjectUpdateDto dto)
        {
            var result =await _service.UpdateAsync(employeeId,projectId,dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{employeeId}/{projectId}")]
        public async Task<IActionResult> Delete(int employeeId,int projectId)
        {
            var result = await _service.DeleteAsync(employeeId,projectId);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}

