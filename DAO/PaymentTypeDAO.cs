using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PaymentTypeDAO
    {
        public static List<PaymentType> GetPaymentType()
        {
            var list = new List<PaymentType>();
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                list = context.PaymentTypes.ToList();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public static void SavePaymentType(PaymentType paymentType)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                context.PaymentTypes.Add(paymentType);
                context.SaveChanges();
            }catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeletePaymentType(PaymentType paymentType)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                var checkPaymentType = context.PaymentTypes.SingleOrDefault(p => p.PaymentTypeId == paymentType.PaymentTypeId);
                context.PaymentTypes.Remove(checkPaymentType);
                context.SaveChanges();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdatePaymentType(PaymentType paymentType)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                context.Entry(paymentType).State = EntityState.Modified;
                context.SaveChanges();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
