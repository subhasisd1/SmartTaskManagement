using SmartTaskManagement.Application.Interfaces;

namespace SmartTaskManagement.Application.Services;

public class EmailNotificationService : IEmailService
{
    public async Task SendAsync(string recipient, string message)
    {
        // Email sending logic will go here

        Console.WriteLine($"Sending EMAIL to: {recipient}");
        Console.WriteLine($"Message: {message}");

        await Task.CompletedTask;
    }
}