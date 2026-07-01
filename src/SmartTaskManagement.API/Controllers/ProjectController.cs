using Microsoft.AspNetCore.Mvc;
using SmartTaskManagement.Application.DTOs.Project;
using SmartTaskManagement.Application.Interfaces;

namespace SmartTaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await _projectService.GetAllAsync();

        return Ok(projects);
    }

    [HttpPost]
    public async Task<IActionResult> PostProject(CreateProjectDto dto)
    {
        var response = await _projectService.CreateAsync(dto);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProject(UpdateProjectDto dto)
    {
       
        var response = await _projectService.UpdateAsync(dto);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProjectById(Guid Id)
    {

        var response = await _projectService.DeleteAsync(Id);

        return Ok(response);
    }

}