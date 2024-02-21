using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoties.IRepository
{
    public interface IPaymentTypeRepository
    {
        void Save(PaymentType paymentType);
        void Delete(PaymentType paymentType);
        void Update(PaymentType paymentType);
        IList<PaymentType> GetPaymentType();
    }
}
