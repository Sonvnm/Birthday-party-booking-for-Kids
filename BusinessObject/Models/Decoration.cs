using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Decoration
    {
        public Decoration()
        {
            Services = new HashSet<Service>();
        }

        public string ItemId { get; set; } = null!;
        public string? ItemName { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
