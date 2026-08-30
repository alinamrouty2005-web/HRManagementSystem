using Microsoft.AspNetCore.Mvc;
using HRManagement.Core.DTOs;
using HRManagement.Core.Interfaces;

namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerformanceReviewsController : Controller
    {
      
            private readonly IPerformanceReviewService _reviewService;

            public PerformanceReviewsController( IPerformanceReviewService reviewService)
            {
                _reviewService = reviewService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var reviews = await _reviewService.GetAllAsync();

                return Ok(reviews);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var review = await _reviewService.GetByIdAsync(id);

                if (review is null)
                    return NotFound();

                return Ok(review);
            }

            [HttpPost]
            public async Task<IActionResult> Create(
                PerformanceReviewCreateDto dto)
            {
                var review = await _reviewService.CreateAsync(dto);

                return Ok(review);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update( int id, PerformanceReviewUpdateDto dto)
            {
                var result = await _reviewService.UpdateAsync(id, dto);

                if (!result)
                    return NotFound();

                return Ok();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var result = await _reviewService.DeleteAsync(id);

                if (!result)
                    return NotFound();

                return Ok();
            }
        }
    }


