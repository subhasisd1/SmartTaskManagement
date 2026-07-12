using SmartTaskManagement.Domain.Enums;

namespace SmartTaskManagement.Application.Common.Pagination;

public class TaskFilter : PaginationRequest
{
    public string? Search { get; set; }

    public Guid? ProjectId { get; set; }

    public TaskItemStatus? Status { get; set; }

    public TaskItemPriority? Priority { get; set; }

    public string SortBy { get; set; } = "CreatedDate";

    public bool Descending { get; set; } = true;
}