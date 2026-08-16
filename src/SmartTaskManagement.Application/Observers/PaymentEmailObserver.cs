using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Observers;

public class PaymentEmailObserver : IPaymentObserver
{
    private readonly IEmailService _emailService;

    public PaymentEmailObserver(
        IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task UpdateAsync(Payment payment)
    {
        await _emailService.SendAsync(
            "subhasispattanaik281@gmail.com",
            $"Hello Subhasis, Payment successful. " +
            $"Transaction ID: {payment.TransactionId}");
    }
}