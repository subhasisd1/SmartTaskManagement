using Dapper;
using SmartTaskManagement.Application.DTOs.Project;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Persistence.Dapper;

public class ProjectQueries : IProjectQueries
{
    private readonly IDapperContext _context;

    public ProjectQueries(IDapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectDto>> GetProjectsAsync()
    {
        using var connection = _context.CreateConnection();

        var sql = @"
            SELECT
                Id,
                Name,
                Description
            FROM Projects";

        return await connection.QueryAsync<ProjectDto>(sql);
    }
}