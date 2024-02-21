using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoties.IRepository
{
    public interface IPaymentRepository
    {
        void Save(Payment payment);
        void Delete(Payment payment);
        void Update(Payment payment);
        IList<Payment> GetPaymentBypaymentId(string paymentId);
        bool Exist(string paymentId);
    }
}
