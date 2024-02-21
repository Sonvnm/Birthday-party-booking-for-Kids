using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Bookings = new HashSet<Booking>();
        }

        public string PaymentId { get; set; }
        public string BankName { get; set; }
        public string BankId { get; set; }
        public string MoneyReceiver { get; set; }
        public string PaymentTypeId { get; set; }
        public int? Amount { get; set; }

        public virtual PaymentType PaymentType { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
