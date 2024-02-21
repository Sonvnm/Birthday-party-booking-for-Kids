using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositoties.IRepository;
using Repositoties.Repository;

namespace BirthdayPartyBookingForKids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository repo = new ServiceRepository();

        [HttpGet("GetAllService")]
        public ActionResult<IList<Service>> GetAllServices()
        {
            IList<Service> services = repo.GetAllServices();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public ActionResult<Service> GetServiceById(int id)
        {
            var service = repo.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        [HttpGet("Search")]
        public ActionResult<Service> GetServiceByName(string name)
        {
            var decoration = repo.GetServicesByName(name);
            if (decoration == null)
            {
                return NotFound();
            }

            return Ok(decoration);
        }

        [HttpPut("{id}")]
        public IActionResult PutService(int id, Service service)
        {
            if (id.Equals(service.ServiceId))
            {
                return BadRequest();
            }

            var sv1 = repo.GetServiceById(id);
            if (sv1 == null)
                return NotFound();
            repo.Update(sv1);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            var service = repo.GetServiceById(id);
            if (service == null)
                return NotFound();
            repo.Delete(service);
            return Ok();
        }
    }
}
