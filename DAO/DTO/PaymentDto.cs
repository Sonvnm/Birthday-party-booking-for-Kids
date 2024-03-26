using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class PaymentDto
    {
        public string PaymentId { get; set; }
        public string BankName { get; set; }
        public string BankId { get; set; }
        public string MoneyReceiver { get; set; }
        public string PaymentTypeId { get; set; }
        public int? Amount { get; set; }
        public string BookingId { get; set; }
    }
}
