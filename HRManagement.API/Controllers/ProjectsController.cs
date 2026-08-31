using Microsoft.AspNetCore.Mvc;
using HRManagement.Core.DTOs;
using HRManagement.Core.Interfaces;

namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
     string? search,
     int pageNumber = 1,
     int pageSize = 10,
     string? sortBy = null)
        {
            var result = await _projectService.GetAllAsync(
                search,
                pageNumber,
                pageSize,
                sortBy);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _projectService.GetByIdAsync(id);

            if (project is null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateDto dto)
        {
            var project = await _projectService.CreateAsync(dto);

            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,ProjectUpdateDto dto)
        {
            var result = await _projectService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _projectService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}

