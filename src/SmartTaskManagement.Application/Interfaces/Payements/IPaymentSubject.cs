using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Interfaces;

public interface IPaymentSubject
{
    void Subscribe(IPaymentObserver observer);

    void Unsubscribe(IPaymentObserver observer);

    Task NotifyAsync(Payment payment);
}