using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;
using SmartTaskManagement.Persistence.Contexts;
using SmartTaskManagement.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IGenericRepository<Project> Projects { get; }

    public IGenericRepository<TaskItem> Tasks { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;

        Projects = new GenericRepository<Project>(_context);
        Tasks = new GenericRepository<TaskItem>(_context);
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