using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Payments = new HashSet<Payment>();
        }

        public string PaymentTypeId { get; set; } = null!;
        public string? PaymentTypeName { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
