using Microsoft.Extensions.DependencyInjection;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Application.Services;

namespace SmartTaskManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IPaymentService, PaymentService>();


        //builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();


        return services;
    }
}