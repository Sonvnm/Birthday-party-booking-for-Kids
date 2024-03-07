using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Menu
    {
        public Menu()
        {
            Services = new HashSet<Service>();
        }

        public string FoodId { get; set; }
        public string FoodName { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
