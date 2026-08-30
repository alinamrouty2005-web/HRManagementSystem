using Microsoft.AspNetCore.Mvc;
using HRManagement.Core.DTOs;
using HRManagement.Core.Interfaces;

namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalariesController : Controller
    {
        private readonly ISalaryService _salaryService;

        public SalariesController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var salaries = await _salaryService.GetAllAsync();

            return Ok(salaries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var salary = await _salaryService.GetByIdAsync(id);

            if (salary is null)
                return NotFound();

            return Ok(salary);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SalaryCreateDto dto)
        {
            var salary = await _salaryService.CreateAsync(dto);

            return Ok(salary);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,SalaryUpdateDto dto)
        {
            var result = await _salaryService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _salaryService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}
