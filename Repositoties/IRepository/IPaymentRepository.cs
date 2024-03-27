using BusinessObject.Models;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoties.IRepository
{
    public interface IPaymentRepository
    {
        void Save(PaymentDto paymentDto);
        void Delete(Payment payment);
        void Update(PaymentDto paymentDto);
        IList<Payment> GetPaymentBypaymentId(string paymentId);
        bool Exist(string paymentId);
        IList<Payment> GetAll();
    }
}
