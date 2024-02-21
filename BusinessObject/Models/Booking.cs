using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Payments = new HashSet<Payment>();
        }

        public string BookingId { get; set; }
        public string UserId { get; set; }
        public int? ParticipateAmount { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime? DateBooking { get; set; }
        public string LocationId { get; set; }
        public string ServiceId { get; set; }

        public virtual Room Location { get; set; }
        public virtual Service Service { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
