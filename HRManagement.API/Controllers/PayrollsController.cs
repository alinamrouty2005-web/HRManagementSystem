using Microsoft.AspNetCore.Mvc;
using HRManagement.Core.DTOs;
using HRManagement.Core.Interfaces;

namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollsController : Controller
    {
        private readonly IPayrollService _payrollService;

        public PayrollsController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
     string? search,
     int pageNumber = 1,
     int pageSize = 10,
     int? employeeId = null,
     bool? isPaid = null,
     string? sortBy = null)
        {
            var result = await _payrollService.GetAllAsync(
                search,
                pageNumber,
                pageSize,
                employeeId,
                isPaid,
                sortBy);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payroll = await _payrollService.GetByIdAsync(id);

            if (payroll is null)
                return NotFound();

            return Ok(payroll);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PayrollCreateDto dto)
        {
            var payroll = await _payrollService.CreateAsync(dto);

            return Ok(payroll);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,PayrollUpdateDto dto)
        {
            var result = await _payrollService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _payrollService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}

