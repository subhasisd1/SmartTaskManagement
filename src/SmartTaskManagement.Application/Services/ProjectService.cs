using FluentValidation;
using SmartTaskManagement.Application.DTOs.Project;
using SmartTaskManagement.Application.Exceptions;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectQueries _projectQueries;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateProjectDto> _validator;

    public ProjectService(IProjectQueries projectQueries, IUnitOfWork unitOfWork, IValidator<CreateProjectDto> validator)
    {
        _projectQueries = projectQueries;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<ProjectDto> CreateAsync(CreateProjectDto dto)
    {
        var result = await _validator.ValidateAsync(dto);
        
        if (!result.IsValid)
        {
            throw new AppValidationException(
                string.Join(", ", result.Errors.Select(x => x.ErrorMessage)));
        }

        var project = new Project
        {
            Name = dto.Name,
            Description = dto.Description
        };

        await _unitOfWork.Projects.AddAsync(project);

        await _unitOfWork.CompleteAsync();

        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description
        };
    }

    public async Task<string> DeleteAsync(Guid id)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(id);

        if (project == null)
            throw new NotFoundException("Project not found");

        _unitOfWork.Projects.Remove(project);

        await _unitOfWork.CompleteAsync();

        return "Project deleted Sucessfully";
    }

    public async Task<IEnumerable<ProjectDto>> GetAllAsync()
    {
        return await _projectQueries.GetProjectsAsync();
    }

    public Task<ProjectDto?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<string> UpdateAsync(UpdateProjectDto dto)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(dto.Id);

        if (project == null)
            throw new NotFoundException("Project not found");

        project.Name = dto.Name;
        project.Description = dto.Description;

        _unitOfWork.Projects.Update(project);

        await _unitOfWork.CompleteAsync();

        return "Project Updated Sucessfully";
    }
}