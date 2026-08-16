using Microsoft.EntityFrameworkCore;
using SmartTaskManagement.Application.Exceptions;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;
using SmartTaskManagement.Persistence.Contexts;

namespace SmartTaskManagement.Persistence.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(x => x.PaymentId == id);
        }

        public async Task<Payment?> GetByOrderIdAsync(int orderId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        public async Task<Payment?> GetByTransactionIdAsync(
            string transactionId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(x =>
                    x.TransactionId == transactionId);
        }

        public async Task AddAsync(Payment payment)
        {
            var existingPayment = await _context.Payments
                                    .AnyAsync(p => p.OrderId == payment.OrderId);

            if (existingPayment)
            {
                throw new ConflictException(
                    $"Payment already exists for OrderId: {payment.OrderId}");
            }

            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }
    }
}
