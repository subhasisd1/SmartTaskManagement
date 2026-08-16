using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartTaskManagement.Application.Adapters;
using SmartTaskManagement.Application.Decorators;
using SmartTaskManagement.Application.DTOs.Configuration;
using SmartTaskManagement.Application.Factories;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Application.Interfaces.Time;
using SmartTaskManagement.Application.Observers;
using SmartTaskManagement.Application.Services;
using SmartTaskManagement.Application.Strategies;
using SmartTaskManagement.Infrastructure.Services;


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

        services.AddScoped<ITimeService, TimeService>();

        // Notification
        services.AddScoped<INotificationFactory, NotificationFactory>();
        services.AddTransient<IEmailService, EmailNotificationService>();
        services.AddTransient<ISmsService, SmsNotificationService>();

        // Payment Strategy
        services.AddTransient<RazorpayPaymentStrategy>();
        services.AddTransient<StripePaymentStrategy>();
        services.AddScoped<PaymentStrategyFactory>();

        // Payment Observers
        services.AddScoped<PaymentEmailObserver>();
        services.AddScoped<PaymentSmsObserver>();
        services.AddScoped<PaymentAuditObserver>();

        // Payment Subject
        services.AddScoped<PaymentSubject>(sp =>
        {
            var subject = new PaymentSubject();

            var emailObserver =
                sp.GetRequiredService<PaymentEmailObserver>();

            var smsObserver =
                sp.GetRequiredService<PaymentSmsObserver>();

            var auditObserver =
                sp.GetRequiredService<PaymentAuditObserver>();

            subject.Subscribe(emailObserver);
            subject.Subscribe(smsObserver);
            subject.Subscribe(auditObserver);

            return subject;
        });

        // Interface -> same PaymentSubject instance
        services.AddScoped<IPaymentSubject>(sp =>
            sp.GetRequiredService<PaymentSubject>());



        // ✅ Correct
        services.AddScoped<PaymentService>();

        services.AddScoped<IPaymentService>(sp =>
        {
            var paymentService =
                sp.GetRequiredService<PaymentService>();

            var logger =
                sp.GetRequiredService<
                    ILogger<LoggingPaymentServiceDecorator>>();

            return new LoggingPaymentServiceDecorator(
                paymentService,
                logger);
        });

        services.AddScoped<RazorpayClient>();
        services.AddScoped<StripeClient>();

        services.AddScoped<RazorpayPaymentAdapter>();
        services.AddScoped<StripePaymentAdapter>();


        return services;
    }
}