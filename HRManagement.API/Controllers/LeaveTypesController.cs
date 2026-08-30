using Microsoft.AspNetCore.Mvc;
using HRManagement.Core.DTOs;
using HRManagement.Core.Interfaces;

namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypesController(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var leaveTypes =await _leaveTypeService.GetAllAsync();

            return Ok(leaveTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var leaveType = await _leaveTypeService.GetByIdAsync(id);

            if (leaveType is null)
                return NotFound();

            return Ok(leaveType);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeaveTypeCreateDto dto)
        {
            var leaveType = await _leaveTypeService.CreateAsync(dto);

            return Ok(leaveType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,LeaveTypeUpdateDto dto)
        {
            var result = await _leaveTypeService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _leaveTypeService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}

