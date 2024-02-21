using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
