using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PaymentDAO
    {
        public static void Save(Payment payment)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                var booking = context.Bookings.FirstOrDefault(o => o.BookingId == payment.BookingId);
                booking.TotalPrice = payment.Amount;
                context.Payments.Add(payment);
                context.SaveChanges();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void Delete(Payment payment)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                context.Remove(payment);
                context.SaveChanges();
            }catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public static IList<Payment> GetPaymentBypaymentId(string paymentId) 
        {
            var list = new List<Payment>();
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                list = context.Payments.Where(o => o.PaymentId == paymentId).ToList();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public static void Update(Payment payment)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                context.Entry(payment).State = EntityState.Modified;
                context.SaveChanges();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
