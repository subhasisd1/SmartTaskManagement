using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Interfaces;

//The key is: Strategy chooses which payment provider to use; Adapter makes each provider's different API look like your common interface.

public interface IPaymentGateway
{
    Task<string> ProcessPaymentAsync(Payment payment);
}