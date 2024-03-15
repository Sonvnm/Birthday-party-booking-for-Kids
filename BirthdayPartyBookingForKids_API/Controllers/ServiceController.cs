using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositoties.IRepository;
using Repositoties.Repository;

namespace BirthdayPartyBookingForKids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository repo = new ServiceRepository();

        public class ServiceModel
        {
            public string ServiceName { get; set; }
            public string Description { get; set; }
            public string FoodId { get; set; }
            public string ItemId { get; set; }
            public double? TotalPrice { get; set; }
        }

        [HttpGet("GetAllService")]
        public ActionResult<IList<Service>> GetAllServices()
        {
            IList<Service> services = repo.GetAllServices();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public ActionResult<Service> GetServiceById(string id)
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

        [HttpPost("Add")]
        public ActionResult<Service> Add(string ServiceId, string ServiceName, string Description, string FoodId, string ItemId, double TotalPrice)
        {
            var sev = new Service { ServiceId = ServiceId, ServiceName = ServiceName, Description = Description, FoodId = FoodId, ItemId = ItemId, TotalPrice = TotalPrice };
            try
            {
                repo.Add(sev);
                return Ok(sev);
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutService(string id, ServiceModel model)
        {
            var service = repo.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }

            service.ServiceName = model.ServiceName;
            service.Description = model.Description;
            service.FoodId = model.FoodId;
            service.ItemId = model.ItemId;
            service.TotalPrice = model.TotalPrice;
            repo.Update(service);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteService(string id)
        {
            var service = repo.GetServiceById(id);
            if (service == null)
                return NotFound();
            repo.Delete(service);
            return Ok();
        }
    }
}
