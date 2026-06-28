using SmartTaskManagement.Application.DTOs.Project;

namespace SmartTaskManagement.Application.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetAllAsync();

    Task<ProjectDto?> GetByIdAsync(Guid id);

    Task<ProjectDto> CreateAsync(CreateProjectDto dto);

    Task UpdateAsync(UpdateProjectDto dto);

    Task DeleteAsync(Guid id);
}