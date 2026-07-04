using SmartTaskManagement.Domain.Enums;

namespace SmartTaskManagement.Application.DTOs.Task;
public class TaskDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public TaskItemStatus Status { get; set; }

    public TaskItemPriority Priority { get; set; }

    public DateTime DueDate { get; set; }

    public int EstimatedHours { get; set; }

    public int ActualHours { get; set; }

    public Guid ProjectId { get; set; }
}