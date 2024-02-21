using BusinessObject.Models;
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
        public IActionResult PutPayment(Payment payment)
        {
            try
            {
                repo.Update(payment);
            }catch(Exception ex)
            {
                return NoContent();
            }
            return Ok();
        }
         
    }
}
