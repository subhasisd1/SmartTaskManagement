namespace SmartTaskManagement.Application.Interfaces;

public interface INotificationService
{
    Task SendAsync(string recipient, string message);
}