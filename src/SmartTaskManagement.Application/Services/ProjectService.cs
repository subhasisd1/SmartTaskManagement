using SmartTaskManagement.Application.DTOs.Project;
using SmartTaskManagement.Application.Interfaces;

namespace SmartTaskManagement.Application.Services;

public class ProjectService : IProjectService
{
    public Task<ProjectDto> CreateAsync(CreateProjectDto dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProjectDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProjectDto?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UpdateProjectDto dto)
    {
        throw new NotImplementedException();
    }
}