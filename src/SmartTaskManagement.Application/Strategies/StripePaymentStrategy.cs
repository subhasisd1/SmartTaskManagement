using SmartTaskManagement.Application.DTOs.Payment;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Application.Interfaces.Payements;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Strategies;

public class StripePaymentStrategy : IPaymentStrategy
{
    public async Task<PaymentResultDto> ProcessAsync(Payment payment)
    {
        // Real Stripe integration will go here

        await Task.CompletedTask;

        return new PaymentResultDto
        {
            Success = true,
            TransactionId = Guid.NewGuid().ToString(),
            Message = "Payment processed successfully using Stripe."
        };
    }
}