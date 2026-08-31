using Microsoft.AspNetCore.Mvc;
using HRManagement.Core.DTOs;
using HRManagement.Core.Interfaces;

namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendancesController : Controller
    {
        private readonly IAttendanceService _attendanceService;

        public AttendancesController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? search,int pageNumber = 1,int pageSize = 10,int? employeeId = null,bool? isPresent = null,string? sortBy = null)
        {
            var result = await _attendanceService.GetAllAsync(search,pageNumber,pageSize,employeeId,isPresent,sortBy);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var attendance = await _attendanceService.GetByIdAsync(id);

            if (attendance is null)
                return NotFound();

            return Ok(attendance);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AttendanceCreateDto dto)
        {
            var attendance = await _attendanceService.CreateAsync(dto);

            return Ok(attendance);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,AttendanceUpdateDto dto)
        {
            var result = await _attendanceService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _attendanceService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}

