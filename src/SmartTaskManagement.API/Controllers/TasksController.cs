using Microsoft.AspNetCore.Mvc;
using SmartTaskManagement.Application.Common.Pagination;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _taskService.GetAllAsync());
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetAll([FromQuery] TaskFilter filter)
    {
        var result = await _taskService.GetAllAsync(filter);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _taskService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        return Ok(await _taskService.CreateAsync(dto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTaskDto dto)
    {
        return Ok(await _taskService.UpdateAsync(dto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await _taskService.DeleteAsync(id));
    }
}