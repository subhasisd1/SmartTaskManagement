using AutoMapper;
using SmartTaskManagement.Application.DTOs.Project;
using SmartTaskManagement.Application.DTOs.Task;
using SmartTaskManagement.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartTaskManagement.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity -> DTO
        CreateMap<Project, ProjectDto>();

        // DTO -> Entity
        CreateMap<CreateProjectDto, Project>();

        CreateMap<UpdateProjectDto, Project>();

        CreateMap<CreateTaskDto, TaskItem>();

        CreateMap<UpdateTaskDto, TaskItem>();

        CreateMap<TaskItem, TaskDto>();
    }
}