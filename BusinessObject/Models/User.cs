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

        public string UserId { get; set; } = null!;
        public string? UserName { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Password { get; set; } = null;
        public string? Phone { get; set; } = null;
        public string? RoleId { get; set; } = "1";

        public virtual Role? Role { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
