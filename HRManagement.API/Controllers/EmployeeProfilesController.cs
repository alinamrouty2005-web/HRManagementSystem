using Microsoft.AspNetCore.Mvc;
using HRManagement.Core.DTOs;
using HRManagement.Core.Interfaces;

namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeProfilesController : Controller
    {
        private readonly IEmployeeProfileService _service;

        public EmployeeProfilesController(IEmployeeProfileService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
      string? search,
      int pageNumber = 1,
      int pageSize = 10,
      int? employeeId = null,
      string? sortBy = null)
        {
            var result = await _service.GetAllAsync(
                search,
                pageNumber,
                pageSize,
                employeeId,
                sortBy);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var profile = await _service.GetByIdAsync(id);

            if (profile is null)
                return NotFound();

            return Ok(profile);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployeeId(int employeeId)
        {
            var profile = await _service.GetByEmployeeIdAsync(employeeId);

            if (profile is null)
                return NotFound();

            return Ok(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeProfileCreateDto dto)
        {
            var profile = await _service.CreateAsync(dto);

            return Ok(profile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,EmployeeProfileUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}

