using BusinessObject.Models;
using DataAccess.DTO;
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
        public static void Save(PaymentDto paymentDto)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                var payment = new Payment
                {
                    PaymentId = paymentDto.PaymentId,
                    BankId = paymentDto.BankId,
                    BankName = paymentDto.BankName,
                    MoneyReceiver = paymentDto.MoneyReceiver,
                    Amount = paymentDto.Amount,
                    PaymentTypeId = paymentDto.PaymentTypeId,
                    BookingId = paymentDto.BookingId,
                };
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
        public static void Update(PaymentDto paymentDto)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                var payment = new Payment
                {
                    PaymentId = paymentDto.PaymentId,
                    BankId = paymentDto.BankId,
                    BankName = paymentDto.BankName,
                    MoneyReceiver = paymentDto.MoneyReceiver,
                    Amount = paymentDto.Amount,
                    PaymentTypeId = paymentDto.PaymentTypeId,
                    BookingId = paymentDto.BookingId,
                };
                context.Entry(payment).State = EntityState.Modified;
                context.SaveChanges();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static bool Exist(string paymentId)
        {
            using var context = new BirthdayPartyBookingForKids_DBContext();
            return context.Payments.Any(e => e.PaymentId == paymentId);
        }
        public static List<Payment> GetAllPayment()
        {
            var listPayment = new List<Payment>();
            try
            {
                using (var context = new BirthdayPartyBookingForKids_DBContext())
                {
                    listPayment = context.Payments.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listPayment;
        }
    }
}
