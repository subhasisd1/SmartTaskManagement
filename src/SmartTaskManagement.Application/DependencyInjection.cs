using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartTaskManagement.Application.Factories;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Application.Services;
using Microsoft.Extensions.Options;
using SmartTaskManagement.Application.DTOs.Configuration;

namespace SmartTaskManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailSettings>(
            configuration.GetSection("EmailSettings"));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITaskService, TaskService>();

        services.AddTransient<IEmailService, EmailNotificationService>();
        services.AddTransient<ISmsService, SmsNotificationService>();

        services.AddScoped<INotificationFactory, NotificationFactory>();
        services.AddScoped<IPaymentService, PaymentService>();


        return services;
    }
}