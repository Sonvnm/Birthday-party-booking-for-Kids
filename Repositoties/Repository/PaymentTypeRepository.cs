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
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        public void Delete(PaymentType paymentType) => PaymentTypeDAO.DeletePaymentType(paymentType);


        public IList<PaymentType> GetPaymentType() => PaymentTypeDAO.GetPaymentType();

        public void Save(PaymentType paymentType) => PaymentTypeDAO.SavePaymentType(paymentType);

        public void Update(PaymentType paymentType) => PaymentTypeDAO.UpdatePaymentType(paymentType);

    }
}
