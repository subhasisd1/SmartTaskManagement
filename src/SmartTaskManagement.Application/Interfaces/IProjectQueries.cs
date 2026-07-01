using SmartTaskManagement.Application.DTOs.Project;

namespace SmartTaskManagement.Application.Interfaces;

public interface IProjectQueries
{
    Task<IEnumerable<ProjectDto>> GetProjectsAsync();
}