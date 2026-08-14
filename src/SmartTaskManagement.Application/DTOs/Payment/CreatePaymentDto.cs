using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTaskManagement.Application.DTOs.Payment
{
    public class CreatePaymentDto
    {
        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "INR";

        public string PaymentMethod { get; set; } = string.Empty;
    }
}
