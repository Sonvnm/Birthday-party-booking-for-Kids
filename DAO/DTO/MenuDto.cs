using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class MenuDto
    {
        public string FoodId { get; set; }
        public string FoodName { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
    }
}
