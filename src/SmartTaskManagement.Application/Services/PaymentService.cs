using FluentValidation;
using SmartTaskManagement.Application.Common.Pagination;
using SmartTaskManagement.Application.DTOs.Task;
using SmartTaskManagement.Application.Exceptions;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(
        IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<Payment> CreatePaymentAsync(
        int orderId,
        string userId,
        decimal amount)
    {
        var payment = new Payment
        {
            OrderId = orderId,
            UserId = userId,
            Amount = amount,
            Currency = "INR",
            Status = "Pending",
            Provider = "Razorpay",
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

        return true;
    }

    public async Task<Payment?> GetPaymentAsync(int paymentId)
    {
        return await _paymentRepository.GetByIdAsync(paymentId);
    }
}