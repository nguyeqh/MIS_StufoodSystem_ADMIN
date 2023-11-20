using StufoodSystem_ADMIN.Models.StaticsValue;
using System;

namespace StufoodSystem_ADMIN.Models
{
    internal class PaymentRecord
    {
        public string PaymentId { get; set; } = "";
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string PaymentMethod { get; set; } = "";
        public int Status { get; set; } = PaymentStatus.PendingPayment;
        public double Amount { get; set; } = 0;

    }
}
