using FluentValidation;
using SmartTaskManagement.Application.DTOs.Task;
using SmartTaskManagement.Application.Exceptions;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskQueries _taskQueries;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateTaskDto> _validator;

    public TaskService(
        ITaskQueries taskQueries,
        IUnitOfWork unitOfWork,
        IValidator<CreateTaskDto> validator)
    {
        _taskQueries = taskQueries;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<TaskDto> CreateAsync(CreateTaskDto dto)
    {
        var result = await _validator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            throw new AppValidationException(
                string.Join(", ", result.Errors.Select(x => x.ErrorMessage)));
        }

        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            Priority = dto.Priority,
            DueDate = dto.DueDate,
            EstimatedHours = dto.EstimatedHours,
            ActualHours = 0,
            ProjectId = dto.ProjectId
        };

        await _unitOfWork.Tasks.AddAsync(task);

        await _unitOfWork.CompleteAsync();

        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            DueDate = task.DueDate,
            EstimatedHours = task.EstimatedHours,
            ActualHours = task.ActualHours,
            ProjectId = task.ProjectId
        };
    }

    public async Task<IEnumerable<TaskDto>> GetAllAsync()
    {
        return await _taskQueries.GetTasksAsync();
    }

    public async Task<TaskDto?> GetByIdAsync(Guid id)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(id);

        if (task == null)
            throw new NotFoundException("Task not found");

        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            DueDate = task.DueDate,
            EstimatedHours = task.EstimatedHours,
            ActualHours = task.ActualHours,
            ProjectId = task.ProjectId
        };
    }

    public async Task<string> UpdateAsync(UpdateTaskDto dto)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(dto.Id);

        if (task == null)
            throw new NotFoundException("Task not found");

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Status = dto.Status;
        task.Priority = dto.Priority;
        task.DueDate = dto.DueDate;
        task.EstimatedHours = dto.EstimatedHours;
        task.ActualHours = dto.ActualHours;

        _unitOfWork.Tasks.Update(task);

        await _unitOfWork.CompleteAsync();

        return "Task updated successfully.";
    }

    public async Task<string> DeleteAsync(Guid id)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(id);

        if (task == null)
            throw new NotFoundException("Task not found");

        _unitOfWork.Tasks.Remove(task);

        await _unitOfWork.CompleteAsync();

        return "Task deleted successfully.";
    }
}