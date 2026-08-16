using SmartTaskManagement.Application.DTOs.Payment;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Adapters;

public class RazorpayPaymentAdapter : IPaymentGateway
{
    private readonly RazorpayClient _razorpayClient;

    public RazorpayPaymentAdapter(
        RazorpayClient razorpayClient)
    {
        _razorpayClient = razorpayClient;
    }

    public async Task<string> ProcessPaymentAsync(
        Payment payment)
    {
        // Convert your application's Payment
        // into Razorpay's expected format.

        var razorpayOrderId =
            await _razorpayClient.CreateOrderAsync(
                payment.Amount,
                payment.Currency);

        return razorpayOrderId;
    }
}