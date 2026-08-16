using Microsoft.Extensions.Logging;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Observers;

public class PaymentSmsObserver : IPaymentObserver
{
    private readonly ISmsService _smsService;
    private readonly ILogger<PaymentSmsObserver> _logger;

    public PaymentSmsObserver(
        ISmsService smsService,
        ILogger<PaymentSmsObserver> logger)
    {
        _smsService = smsService;
        _logger = logger;
    }

    public async Task UpdateAsync(Payment payment)
    {
        _logger.LogInformation(
            "Sending payment success SMS for PaymentId: {PaymentId}, TransactionId: {TransactionId}",
            payment.PaymentId,
            payment.TransactionId);

        try
        {
            await _smsService.SendAsync(
                "9876543210",
                $"Payment successful. " +
                $"Transaction ID: {payment.TransactionId}");

            _logger.LogInformation(
                "Payment success SMS sent successfully for PaymentId: {PaymentId}",
                payment.PaymentId);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send payment success SMS for PaymentId: {PaymentId}",
                payment.PaymentId);

            throw;
        }
    }
}