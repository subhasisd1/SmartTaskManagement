using SmartTaskManagement.Application.DTOs.Payment;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Interfaces.Payements;

public interface IPaymentStrategy
{
    Task<PaymentResultDto> ProcessAsync(Payment payment);
}