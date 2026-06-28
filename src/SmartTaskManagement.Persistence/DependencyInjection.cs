using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Persistence.Contexts;
using SmartTaskManagement.Persistence.Dapper;
using SmartTaskManagement.Persistence.Repositories;

namespace SmartTaskManagement.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDapperContext, DapperContext>();
        services.AddScoped<DapperRepository>();

        return services;
    }
}