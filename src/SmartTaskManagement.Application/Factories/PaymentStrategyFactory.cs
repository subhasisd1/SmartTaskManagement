using SmartTaskManagement.Application.Exceptions;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Application.Interfaces.Payements;
using SmartTaskManagement.Application.Strategies;

namespace SmartTaskManagement.Application.Factories;

public class PaymentStrategyFactory
{
    private readonly RazorpayPaymentStrategy _razorpay;
    private readonly StripePaymentStrategy _stripe;

    public PaymentStrategyFactory(
        RazorpayPaymentStrategy razorpay,
        StripePaymentStrategy stripe)
    {
        _razorpay = razorpay;
        _stripe = stripe;
    }

    public IPaymentStrategy Create(string provider)
    {
        return provider.ToLowerInvariant() switch
        {
            "razorpay" => _razorpay,

            "stripe" => _stripe,

            _ => throw new BadRequestException(
                $"Unsupported payment provider: {provider}")
        };
    }
}