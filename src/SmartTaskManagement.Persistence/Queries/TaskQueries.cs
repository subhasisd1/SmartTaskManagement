using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SmartTaskManagement.Application.Common.Pagination;
using SmartTaskManagement.Application.DTOs.Task;
using SmartTaskManagement.Application.Interfaces;

namespace SmartTaskManagement.Persistence.Queries;

public class TaskQueries : ITaskQueries
{
    private readonly IConfiguration _configuration;

    public TaskQueries(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private SqlConnection Connection =>
        new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

    public async Task<IEnumerable<TaskDto>> GetTasksAsync()
    {
        const string sql = @"
            SELECT
                Id,
                Title,
                Description,
                Status,
                Priority,
                DueDate,
                EstimatedHours,
                ActualHours,
                ProjectId
            FROM Tasks
            WHERE IsDeleted = 0
            ORDER BY CreatedDate DESC";

        using var connection = Connection;

        return await connection.QueryAsync<TaskDto>(sql);
    }

    public async Task<PagedResult<TaskDto>> GetTasksAsync(TaskFilter filter)
    {
        using var connection = Connection;

        var sql = @"
                    SELECT
                        Id,
                        Title,
                        Description,
                        Status,
                        Priority,
                        DueDate,
                        EstimatedHours,
                        ActualHours,
                        ProjectId
                    FROM Tasks
                    WHERE IsDeleted = 0
                    AND (@Search IS NULL OR Title LIKE '%' + @Search + '%' OR Description LIKE '%' + @Search + '%')
                    AND (@ProjectId IS NULL OR ProjectId = @ProjectId)
                    AND (@Status IS NULL OR Status = @Status)
                    AND (@Priority IS NULL OR Priority = @Priority)
                    ORDER BY CreatedDate DESC
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM Tasks
                    WHERE IsDeleted = 0
                    AND (@Search IS NULL OR Title LIKE '%' + @Search + '%' OR Description LIKE '%' + @Search + '%')
                    AND (@ProjectId IS NULL OR ProjectId = @ProjectId)
                    AND (@Status IS NULL OR Status = @Status)
                    AND (@Priority IS NULL OR Priority = @Priority);
                    ";

        var parameters = new
        {
            filter.Search,
            filter.ProjectId,
            filter.Status,
            filter.Priority,
            Offset = (filter.PageNumber - 1) * filter.PageSize,
            filter.PageSize
        };

        using var multi = await connection.QueryMultipleAsync(sql, parameters);

        var tasks = await multi.ReadAsync<TaskDto>();
        var total = await multi.ReadSingleAsync<int>();

        return new PagedResult<TaskDto>
        {
            Items = tasks,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize,
            TotalRecords = total
        };
    }


    public async Task<TaskDto?> GetTaskByIdAsync(Guid id)
    {
        const string sql = @"
            SELECT
                Id,
                Title,
                Description,
                Status,
                Priority,
                DueDate,
                EstimatedHours,
                ActualHours,
                ProjectId
            FROM Tasks
            WHERE Id = @Id
            AND IsDeleted = 0";

        using var connection = Connection;

        return await connection.QueryFirstOrDefaultAsync<TaskDto>(
            sql,
            new { Id = id });
    }
}