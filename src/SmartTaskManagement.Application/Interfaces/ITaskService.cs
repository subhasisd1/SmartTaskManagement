using SmartTaskManagement.Application.DTOs.Task;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllAsync();

    Task<TaskDto?> GetByIdAsync(Guid id);

    Task<TaskDto> CreateAsync(CreateTaskDto dto);

    Task<string> UpdateAsync(UpdateTaskDto dto);

    Task<string> DeleteAsync(Guid id);
}