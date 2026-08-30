using Microsoft.AspNetCore.Mvc;
using HRManagement.Core.DTOs;
using HRManagement.Core.Interfaces;

namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionsController : Controller
    {
        private readonly IPositionService _positionService;

        public PositionsController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var positions = await _positionService.GetAllAsync();

            return Ok(positions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var position = await _positionService.GetByIdAsync(id);

            if (position is null)
                return NotFound();

            return Ok(position);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PositionCreateDto dto)
        {
            var position = await _positionService.CreateAsync(dto);

            return Ok(position);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,PositionUpdateDto dto)
        {
            var result = await _positionService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _positionService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}

