using BusinessObject.Models;
using DataAccess;
using Repositoties.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoties.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        public void Delete(Payment payment) => PaymentDAO.Delete(payment);
        

        public IList<Payment> GetPaymentBypaymentId(string paymentId) => PaymentDAO.GetPaymentBypaymentId(paymentId);

        public void Save(Payment payment) => PaymentDAO.Save(payment);
        

        public void Update(Payment payment) => PaymentDAO.Update(payment);
    }
}
