using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Observers;

public class PaymentSubjectDI : IPaymentSubject
{
    private readonly IEnumerable<IPaymentObserver> _observers;

    public PaymentSubjectDI(
        IEnumerable<IPaymentObserver> observers)
    {
        _observers = observers;
    }

    public void Subscribe(IPaymentObserver observer)
    {
        // Not needed when using DI
    }

    public void Unsubscribe(IPaymentObserver observer)
    {
        // Not needed when using DI
    }

    public async Task NotifyAsync(Payment payment)
    {
        foreach (var observer in _observers)
        {
            await observer.UpdateAsync(payment);
        }
    }
}