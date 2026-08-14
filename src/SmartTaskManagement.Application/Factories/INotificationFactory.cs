using SmartTaskManagement.Application.Interfaces;

namespace SmartTaskManagement.Application.Factories;

public interface INotificationFactory
{
    INotificationService Create(string notificationType);
}