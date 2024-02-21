using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Room
    {
        public Room()
        {
            Bookings = new HashSet<Booking>();
        }

        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
