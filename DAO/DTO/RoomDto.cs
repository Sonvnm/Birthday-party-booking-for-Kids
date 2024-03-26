using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class RoomDto
    {
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
    }
}
