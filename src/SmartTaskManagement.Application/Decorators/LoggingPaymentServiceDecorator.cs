using Microsoft.Extensions.Logging;
using SmartTaskManagement.Application.DTOs;
using SmartTaskManagement.Application.DTOs.Payment;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Decorators;

public class LoggingPaymentServiceDecorator : IPaymentService
{
    private readonly IPaymentService _inner;
    private readonly ILogger<LoggingPaymentServiceDecorator> _logger;

    public LoggingPaymentServiceDecorator(
        IPaymentService inner,
        ILogger<LoggingPaymentServiceDecorator> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public async Task<ProcessPaymentResponseDto> ProcessPaymentAsync(
        int paymentId)
    {
        _logger.LogInformation(
            "Payment processing started. PaymentId: {PaymentId}",
            paymentId);

        try
        {
            var result =
                await _inner.ProcessPaymentAsync(paymentId);

            _logger.LogInformation(
                "Payment processing completed. PaymentId: {PaymentId}",
                paymentId);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Payment processing failed. PaymentId: {PaymentId}",
                paymentId);

            throw;
        }
    }
    public async Task<Payment?> GetPaymentAsync(int paymentId)
    {
        _logger.LogInformation(
            "Getting payment started. PaymentId: {PaymentId}",
            paymentId);

        try
        {
            var result =
                await _inner.GetPaymentAsync(paymentId);

            _logger.LogInformation(
                "Getting payment completed. PaymentId: {PaymentId}",
                paymentId);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Getting payment failed. PaymentId: {PaymentId}",
                paymentId);

            throw;
        }
    }

    public async Task<Payment> CreatePaymentAsync(
        int orderId,
        string userId,
        decimal amount,
        string payMethod)
    {
        _logger.LogInformation(
            "Payment creation started. OrderId: {OrderId}, UserId: {UserId}, Amount: {Amount}, PaymentMethod: {PaymentMethod}",
            orderId,
            userId,
            amount,
            payMethod);

        try
        {
            var payment = await _inner.CreatePaymentAsync(
                orderId,
                userId,
                amount,
                payMethod);

            _logger.LogInformation(
                "Payment created successfully. PaymentId: {PaymentId}, OrderId: {OrderId}",
                payment.PaymentId,
                orderId);

            return payment;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Payment creation failed. OrderId: {OrderId}, UserId: {UserId}",
                orderId,
                userId);

            throw;
        }
    }


}