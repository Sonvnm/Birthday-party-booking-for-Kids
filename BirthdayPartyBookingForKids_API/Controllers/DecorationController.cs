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
    public class DecorationController : ControllerBase
    {
        private readonly IDecorationRepository repo = new DecorationRepository();

        [HttpGet("GetAllDecoration")]
        public ActionResult<IList<Decoration>> GetAllDecoration()
        {
            IList<Decoration> decorations = repo.GetAllDecorations();
            return Ok(decorations);
        }

        [HttpGet("{id}")]
        public ActionResult<Decoration> GetDecorationById(int id)
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

        [HttpPost("Add")]
        public ActionResult<Decoration> Add(string ItemName, string Description, double? Price) 
        {
            var dec = new Decoration {  ItemName = ItemName, Description = Description, Price = Price };
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

        [HttpPut("{id}")]
        public IActionResult PutDecoration(int id, Decoration decoration)
        {
            if(id.Equals(decoration.ItemId))
            {
                return BadRequest();
            }

            var dec1 = repo.GetDecorationById(id);
            if (dec1 == null)
                return NotFound();
            repo.Update(decoration);
            return Ok();
        }

        [HttpDelete("{id}")]
        public  IActionResult DeleteDecoration(int id)
        {
            var decoration = repo.GetDecorationById(id);
            if (decoration == null)
                return NotFound();
            repo.Delete(decoration);
            return Ok();
        }
    }
}
