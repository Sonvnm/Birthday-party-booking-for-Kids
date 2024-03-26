using BusinessObject.Models;
using DataAccess.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositoties.IRepository;
using Repositoties.Repository;

namespace BirthdayPartyBookingForKids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository repo = new PaymentRepository();
        [HttpGet("{id}")]
        public ActionResult<IList<Payment>> GetPaymentBypaymentId(string paymentId)
        {
            IList<Payment> list = repo.GetPaymentBypaymentId(paymentId);
            if(list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpPut]
        public IActionResult PutPayment(PaymentDto paymentDto)
        {
            try
            {
                var getpayment = repo.GetPaymentBypaymentId(paymentDto.PaymentId);
                if(getpayment == null)
                {
                    return NotFound();
                }
                repo.Update(paymentDto);
            }catch(Exception ex)
            {
                return NoContent();
            }
            return Ok();
        }
        /*[HttpDelete("id")]
        public IActionResult DeletePayment(string paymentId)
        {
            var getPayment = repo.GetPaymentBypaymentId(paymentId);
            if(getPayment == null)
            {
                return NoContent();
            }
            repo.Delete(getPayment);
            return NoContent();
        }*/
        [HttpPost]
        public ActionResult<Payment> PostPayment(PaymentDto paymentDto) 
        {
            try
            {
                repo.Save(paymentDto);
                return Ok();
            }catch(Exception ex)
            {
                if(paymentExist == null)
                {
                    return NotFound();
                }
                return Ok(paymentDto);
            }
        }
        private bool paymentExist(string paymentId)
        {
            return repo.Exist(paymentId);
        }
        [HttpGet("GetAllPayment")]
        public ActionResult<IList<Payment>> GetAllPayment()
        {
            IList<Payment> payment = repo.GetAll();
            return Ok(payment);
        }
    }
}  
