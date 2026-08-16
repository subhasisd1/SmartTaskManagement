using SmartTaskManagement.Application.Adapters;
using SmartTaskManagement.Application.DTOs.Payment;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Application.Interfaces.Payements;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Strategies;

public class RazorpayPaymentStrategy : IPaymentStrategy
{
    private readonly RazorpayPaymentAdapter _adapter;

    public RazorpayPaymentStrategy(
        RazorpayPaymentAdapter adapter)
    {
        _adapter = adapter;
    }

    public async Task<PaymentResultDto> ProcessAsync(Payment payment)
    {
        // Real Razorpay integration will go here

        await Task.CompletedTask;

        var transactionId = await _adapter.ProcessPaymentAsync(payment);

        return new PaymentResultDto
        {
            Success = true,
            TransactionId = transactionId,
            Message = "Payment processed successfully using Razorpay."
        };
    }
}