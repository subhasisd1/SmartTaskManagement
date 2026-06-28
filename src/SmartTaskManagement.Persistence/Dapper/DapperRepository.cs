using Dapper;
using SmartTaskManagement.Application.DTOs.Project;

namespace SmartTaskManagement.Persistence.Dapper;

public class DapperRepository
{
    private readonly IDapperContext _context;

    public DapperRepository(IDapperContext context)
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