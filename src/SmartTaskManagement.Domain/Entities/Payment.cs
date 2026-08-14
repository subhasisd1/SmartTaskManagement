using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTaskManagement.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "INR";

        public string PaymentMethod { get; set; } = string.Empty;

        public string? TransactionId { get; set; }

        public string Status { get; set; } = "Pending";

        public string Provider { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
