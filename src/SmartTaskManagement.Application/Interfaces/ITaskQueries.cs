
using SmartTaskManagement.Application.DTOs.Task;
using SmartTaskManagement.Application.Common.Pagination;

namespace SmartTaskManagement.Application.Interfaces;

public interface ITaskQueries
{
    Task<IEnumerable<TaskDto>> GetTasksAsync();

    Task<PagedResult<TaskDto>> GetTasksAsync(TaskFilter filter);

    Task<TaskDto?> GetTaskByIdAsync(Guid id);
}