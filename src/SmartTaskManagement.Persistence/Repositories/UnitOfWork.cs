using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;
using SmartTaskManagement.Persistence.Contexts;

namespace SmartTaskManagement.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IGenericRepository<Project> Projects { get; }

    public IGenericRepository<TaskItem> Tasks { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;

        Projects = new GenericRepository<Project>(context);

        Tasks = new GenericRepository<TaskItem>(context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}