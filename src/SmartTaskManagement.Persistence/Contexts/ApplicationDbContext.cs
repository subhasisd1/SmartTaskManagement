using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Persistence.Contexts;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Project>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                  .HasMaxLength(200)
                  .IsRequired();

            entity.Property(x => x.Description)
                  .HasMaxLength(1000);
        });

        builder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Title)
                  .HasMaxLength(200)
                  .IsRequired();

            entity.Property(x => x.Description)
                  .HasMaxLength(1000);
        });
    }
}