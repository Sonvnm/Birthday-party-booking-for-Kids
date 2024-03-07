using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Service
    {
        public Service()
        {
            Bookings = new HashSet<Booking>();
        }

        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string FoodId { get; set; }
        public string ItemId { get; set; }
        public double? TotalPrice { get; set; }

        public virtual Menu Food { get; set; }
        public virtual Decoration Item { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
