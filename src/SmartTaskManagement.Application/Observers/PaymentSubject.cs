using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Observers;

public class PaymentSubject : IPaymentSubject
{
    private readonly List<IPaymentObserver> _observers = new();

    public void Subscribe(IPaymentObserver observer)
    {
        _observers.Add(observer);
    }

    public void Unsubscribe(IPaymentObserver observer)
    {
        _observers.Remove(observer);
    }

    public async Task NotifyAsync(Payment payment)
    {
        Console.WriteLine($"Observer count: {_observers.Count}");

        foreach (var observer in _observers)
        {
            await observer.UpdateAsync(payment);
        }
    }
}