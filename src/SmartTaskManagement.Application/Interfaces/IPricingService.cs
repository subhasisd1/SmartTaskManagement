using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTaskManagement.Application.Interfaces
{
    public interface IPricingService
    {
        decimal Calculate(decimal amount);
    }

    public class StandardPricing : IPricingService
    {
        public decimal Calculate(decimal amount)
            => amount * 1.05m;
    }

    public class PremiumPricing : IPricingService
    {
        public decimal Calculate(decimal amount)
            => amount * 1.10m;
    }
}
