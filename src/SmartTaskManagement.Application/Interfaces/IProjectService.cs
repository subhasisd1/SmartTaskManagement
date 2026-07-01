using SmartTaskManagement.Application.DTOs.Project;

namespace SmartTaskManagement.Application.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetAllAsync();

    Task<ProjectDto?> GetByIdAsync(Guid id);

    Task<ProjectDto> CreateAsync(CreateProjectDto dto);

    Task<string> UpdateAsync(UpdateProjectDto dto);

    Task<string> DeleteAsync(Guid id);
}