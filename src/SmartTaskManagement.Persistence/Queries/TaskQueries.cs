using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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