using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Adapters;

public class StripePaymentAdapter : IPaymentGateway
{
    private readonly StripeClient _stripeClient;

    public StripePaymentAdapter(
        StripeClient stripeClient)
    {
        _stripeClient = stripeClient;
    }

    public async Task<string> ProcessPaymentAsync(
        Payment payment)
    {
        var paymentIntentId =
            await _stripeClient.CreatePaymentIntentAsync(
                payment.Amount,
                payment.Currency);

        return paymentIntentId;
    }
}