using SmartTaskManagement.Application.DTOs.Task;

namespace SmartTaskManagement.Application.Interfaces;

public interface ITaskQueries
{
    Task<IEnumerable<TaskDto>> GetTasksAsync();

    Task<TaskDto?> GetTaskByIdAsync(Guid id);
}