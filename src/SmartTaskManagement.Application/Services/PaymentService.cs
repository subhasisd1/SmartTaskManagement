using FluentValidation;
using SmartTaskManagement.Application.Common.Pagination;
using SmartTaskManagement.Application.DTOs.Task;
using SmartTaskManagement.Application.Exceptions;
using SmartTaskManagement.Application.Factories;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly INotificationFactory _notificationFactory;


    public PaymentService(IPaymentRepository paymentRepository, INotificationFactory notificationFactory)
    {
        _paymentRepository = paymentRepository;
        _notificationFactory = notificationFactory;
    }

    public async Task<Payment> CreatePaymentAsync(
        int orderId,
        string userId,
        decimal amount,
        string paymentMethod)
    {
       
        //    var paymentId =
        //$"PAY-{userId.ToString()[..4].ToUpper()}-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid():N[..6].ToUpper()}";
        var paymentId = (orderId * 1000) + DateTime.UtcNow.Millisecond;
        var payment = new Payment
        {
            PaymentId = paymentId,
            OrderId = orderId,
            UserId = userId,
            Amount = amount,
            Currency = "INR",
            Status = "Pending",
            Provider = "Razorpay",
            PaymentMethod = paymentMethod,
            CreatedAt = DateTime.UtcNow
        };

        await _paymentRepository.AddAsync(payment);

        return payment;
    }

    public async Task<bool> ProcessPaymentAsync(int paymentId)
    {
        var payment =
            await _paymentRepository.GetByIdAsync(paymentId);

        if (payment == null)
            return false;

        // Actual payment provider integration goes here.

        payment.Status = "Success";
        payment.TransactionId =
            Guid.NewGuid().ToString();

        payment.UpdatedAt = DateTime.UtcNow;

        await _paymentRepository.UpdateAsync(payment);

        // Send notification after successful payment
        var notification =
            _notificationFactory.Create("email");

        await notification.SendAsync(
            "subhasispattanaik281@gmail.com",
            $"Payment successful. Transaction ID: {payment.TransactionId}");


        return true;
    }

    public async Task<Payment?> GetPaymentAsync(int paymentId)
    {
        return await _paymentRepository.GetByIdAsync(paymentId);
    }
}