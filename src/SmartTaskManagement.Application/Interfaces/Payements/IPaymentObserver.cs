using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Interfaces;

public interface IPaymentObserver
{
    Task UpdateAsync(Payment payment);
}