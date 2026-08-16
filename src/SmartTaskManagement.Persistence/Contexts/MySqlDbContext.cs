using Microsoft.EntityFrameworkCore;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Persistence.Contexts;

public class MySqlDbContext : DbContext
{
    public MySqlDbContext(
        DbContextOptions<MySqlDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(MySqlDbContext).Assembly);
    }

}