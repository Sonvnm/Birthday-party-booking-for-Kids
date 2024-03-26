using BusinessObject.Models;
using DataAccess;
using DataAccess.DTO;
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

        public void Save(PaymentDto paymentDto) => PaymentDAO.Save(paymentDto);
        

        public void Update(PaymentDto paymentDto) => PaymentDAO.Update(paymentDto);

        public bool Exist(string paymentId) => PaymentDAO.Exist(paymentId);

        public IList<Payment> GetAll()
        => PaymentDAO.GetAllPayment();
    }
}
