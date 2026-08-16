using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTaskManagement.Application.Adapters
{
    public class RazorpayClient
    {
        public async Task<string> CreateOrderAsync(
            decimal amount,
            string currency)
        {
            // Razorpay API
            return "RZP_12345";
        }
    }
}
