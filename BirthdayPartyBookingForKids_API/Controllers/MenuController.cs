using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositoties.IRepository;
using Repositoties.Repository;

namespace BirthdayPartyBookingForKids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ODataController
    {
        private readonly IMenuRepository repo = new MenuRepository();

        [HttpGet("ListMenu")]
        public ActionResult<IEnumerable<Menu>> GetAllMenu() => repo.GetAllMenus();

        [HttpGet("{id}")]
        public ActionResult<Menu> GetMenuByID(int id)
        {
            var menu = repo.GetMenuById(id);
            if (menu == null)
            {
                return NotFound();
            }
            return Ok(menu);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("Post new Menu")]
        public ActionResult<Menu> PostMenu(Menu menu)
        {
            repo.SaveMenu(menu);
            return CreatedAtAction(nameof(GetMenuByID), new { id = menu.FoodId }, menu);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("UpdateMenu")]
        public IActionResult UpdateRoom(int id, Menu menu)
        {
            var getMenu = repo.GetMenuById(id);
            if (getMenu == null)
            {
                return NotFound();
            }
            repo.UpdateMenu(menu);
            return Ok(menu);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("DeleteRoom{id}")]
        public IActionResult DeleteMenu(int id, Menu menu)
        {
            var getMenu = repo.GetMenuById(id);
            if (getMenu == null)
            {
                return NotFound();
            }
            repo.DeleteMenu(menu);
            return Ok();
        }
    }
}
