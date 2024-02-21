using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositoties.IRepository;
using Repositoties.Repository;

namespace BirthdayPartyBookingForKids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeRepository repo = new PaymentTypeRepository();
        [HttpGet]
        public async Task<ActionResult<IList<PaymentType>>> GetPaymentType()
        {
            var item = repo.GetPaymentType();
            return Ok(item);
        }
    }
}
