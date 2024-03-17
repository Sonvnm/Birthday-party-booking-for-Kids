using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositoties.IRepository;
using Repositoties.Repository;

namespace BirthdayPartyBookingForKids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecorationController : ControllerBase
    {
        private readonly IDecorationRepository repo = new DecorationRepository();
        public class DecorationModel
        {
            public string ItemName { get; set; }
            public string Description { get; set; }
            public double? Price { get; set; }
        }

        [HttpGet("GetAllDecoration")]
        public ActionResult<IList<Decoration>> GetAllDecoration()
        {
            IList<Decoration> decorations = repo.GetAllDecorations();
            return Ok(decorations);
        }

        [HttpGet("{id}")]
        public ActionResult<Decoration> GetDecorationById(string id)
        {
            var decoration = repo.GetDecorationById(id);
            if (decoration == null)
            {
                return NotFound();
            }

            return Ok(decoration);
        }

        [HttpGet("Search")]
        public ActionResult<Decoration> GetDecorationByName(string name)
        {
            var decoration = repo.GetDecorationsByName(name);
            if (decoration == null)
            {
                return NotFound();
            }

            return Ok(decoration);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("Add")]
        public ActionResult<Decoration> Add(string ItemId, string ItemName, string Description, double? Price) 
        {
            var dec = new Decoration {  ItemId= ItemId, ItemName = ItemName, Description = Description, Price = Price };
            try
            {
                repo.Add(dec);
                return Ok(dec);
            }
            catch (DbUpdateException) 
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{id}")]
        public IActionResult PutDecoration(string id, DecorationModel model)
        {
            var decoration = repo.GetDecorationById(id);
            if (decoration == null)
            {
                return NotFound();
            }
            
            decoration.ItemName = model.ItemName;
            decoration.Description = model.Description;
            decoration.Price = model.Price;
            repo.Update(decoration);
            return Ok();
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public  IActionResult DeleteDecoration(string id)
        {
            var decoration = repo.GetDecorationById(id);
            if (decoration == null)
                return NotFound();
            repo.Delete(decoration);
            return Ok();
        }
    }
}
