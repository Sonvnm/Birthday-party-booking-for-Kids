using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Payment
    {
        public string PaymentId { get; set; } = null!;
        public string? BankName { get; set; }
        public string? BankId { get; set; }
        public string? MoneyReceiver { get; set; }
        public string? PaymentTypeId { get; set; }
        public int? Amount { get; set; }
        public string? BookingId { get; set; }

        public virtual Booking? Booking { get; set; }
        public virtual PaymentType? PaymentType { get; set; }
    }
}
