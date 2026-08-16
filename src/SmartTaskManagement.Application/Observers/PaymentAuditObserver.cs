using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Observers;

public class PaymentAuditObserver : IPaymentObserver
{
    public async Task UpdateAsync(Payment payment)
    {
        Console.WriteLine(
            $"AUDIT: Payment {payment.PaymentId} " +
            $"processed successfully.");

        await Task.CompletedTask;
    }
}