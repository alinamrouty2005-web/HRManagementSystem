using Microsoft.AspNetCore.Mvc;
using HRManagement.Core.DTOs;
using HRManagement.Core.Interfaces;

namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeePositionsController : Controller
    {
        private readonly IEmployeePositionService _service;

        public EmployeePositionsController(IEmployeePositionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
    string? search,
    int pageNumber = 1,
    int pageSize = 10,
    int? employeeId = null,
    int? positionId = null,
    string? sortBy = null)
        {
            var result = await _service.GetAllAsync(
                search,
                pageNumber,
                pageSize,
                employeeId,
                positionId,
                sortBy);

            return Ok(result);
        }

        [HttpGet("{employeeId}/{positionId}")]
        public async Task<IActionResult> GetById(int employeeId,int positionId)
        {
            var result =await _service.GetByIdAsync(employeeId,positionId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeePositionCreateDto dto)
        {
            var result =await _service.CreateAsync(dto);

            return Ok(result);
        }

        [HttpPut("{employeeId}/{positionId}")]
        public async Task<IActionResult> Update(int employeeId,int positionId,EmployeePositionUpdateDto dto)
        {
            var result =await _service.UpdateAsync(employeeId,positionId, dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{employeeId}/{positionId}")]
        public async Task<IActionResult> Delete(int employeeId,int positionId)
        {
            var result =await _service.DeleteAsync(employeeId,positionId);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}

