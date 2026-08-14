using SmartTaskManagement.Application.Interfaces;

namespace SmartTaskManagement.Application.Services;

public class SmsNotificationService : ISmsService
{
    public async Task SendAsync(string recipient, string message)
    {
        // SMS sending logic will go here

        Console.WriteLine($"Sending SMS to: {recipient}");
        Console.WriteLine($"Message: {message}");

        await Task.CompletedTask;
    }
}