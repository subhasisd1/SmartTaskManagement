using SmartTaskManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTaskManagement.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> CreatePaymentAsync(
            int orderId,
            string userId,
            decimal amount,
            string payMethod);

        Task<bool> ProcessPaymentAsync(
            int paymentId);

        Task<Payment?> GetPaymentAsync(int paymentId);
    }
}
