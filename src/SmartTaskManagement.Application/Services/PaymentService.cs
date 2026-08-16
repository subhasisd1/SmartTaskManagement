using FluentValidation;
using SmartTaskManagement.Application.Common.Pagination;
using SmartTaskManagement.Application.DTOs.Payment;
using SmartTaskManagement.Application.DTOs.Task;
using SmartTaskManagement.Application.Exceptions;
using SmartTaskManagement.Application.Factories;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Application.Interfaces.Time;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly INotificationFactory _notificationFactory;
    private readonly PaymentStrategyFactory _paymentStrategyFactory;
    private readonly ITimeService _timeService;

    private readonly IPaymentSubject _paymentSubject;

    public PaymentService(
    IPaymentRepository paymentRepository,
    INotificationFactory notificationFactory,
    PaymentStrategyFactory paymentStrategyFactory,
    ITimeService timeService,
    IPaymentSubject paymentSubject)
    {
        _paymentRepository = paymentRepository;
        _notificationFactory = notificationFactory;
        _paymentStrategyFactory = paymentStrategyFactory;
        _timeService = timeService;
        _paymentSubject = paymentSubject;
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

    public async Task<ProcessPaymentResponseDto?> ProcessPaymentAsync(
    int paymentId)
    {
        var payment =
            await _paymentRepository.GetByIdAsync(paymentId);

        if (payment == null)
            return null;

        // Factory selects the appropriate Strategy
        var strategy =
            _paymentStrategyFactory.Create(payment.Provider);

        // Strategy processes the payment
        var result =
            await strategy.ProcessAsync(payment);

        if (!result.Success)
        {
            payment.Status = "Failed";
            
            // convert to indian time
            payment.UpdatedAt = _timeService.GetCurrentIstTime();

            await _paymentRepository.UpdateAsync(payment);

            // Notify observers
            await _paymentSubject.NotifyAsync(payment);

            return null;
        }

        payment.Status = "Success";
        payment.TransactionId = result.TransactionId;


        payment.UpdatedAt = _timeService.GetCurrentIstTime();

        await _paymentRepository.UpdateAsync(payment);

        // Notify observers
        await _paymentSubject.NotifyAsync(payment);

        // Send notification using your existing Factory
        var notification =
            _notificationFactory.Create("email");

        await notification.SendAsync(
            "subhasispattanaik281@gmail.com",
            $"Payment successful. " +
            $"Transaction ID: {payment.TransactionId}");

        return new ProcessPaymentResponseDto
        {
            PaymentId = payment.PaymentId,
            OrderId = payment.OrderId,
            Amount = payment.Amount,
            Currency = payment.Currency,
            Status = payment.Status,
            TransactionId = payment.TransactionId,
            PaymentMethod = payment.PaymentMethod,
            ProcessedAt = payment.UpdatedAt
        };
    }

    public async Task<Payment?> GetPaymentAsync(int paymentId)
    {
        return await _paymentRepository.GetByIdAsync(paymentId);
    }
}