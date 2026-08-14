using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetByIdAsync(int id);

        Task<Payment?> GetByOrderIdAsync(int orderId);

        Task<Payment?> GetByTransactionIdAsync(string transactionId);

        Task AddAsync(Payment payment);

        Task UpdateAsync(Payment payment);
    }
}
