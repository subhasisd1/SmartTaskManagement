using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Project> Projects { get; }

    IGenericRepository<TaskItem> Tasks { get; }

    Task<int> CompleteAsync();
}