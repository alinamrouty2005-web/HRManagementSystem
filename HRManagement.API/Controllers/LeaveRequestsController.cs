using Microsoft.AspNetCore.Mvc;
using HRManagement.Core.DTOs;
using HRManagement.Core.Interfaces;

namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveRequestsController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestsController( ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? search,int pageNumber = 1,int pageSize = 10,int? employeeId = null,int? leaveTypeId = null,bool? isApproved = null,string? sortBy = null)
        {
            var result = await _leaveRequestService.GetAllAsync(search,pageNumber,pageSize,employeeId,leaveTypeId,isApproved,sortBy);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = await _leaveRequestService.GetByIdAsync(id);

            if (request is null)
                return NotFound();

            return Ok(request);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeaveRequestCreateDto dto)
        {
            var request = await _leaveRequestService.CreateAsync(dto);

            return Ok(request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update( int id, LeaveRequestUpdateDto dto)
        {
            var result =await _leaveRequestService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _leaveRequestService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}

