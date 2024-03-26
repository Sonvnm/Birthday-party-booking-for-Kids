using BusinessObject.Models;
using DataAccess.DTO;
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
        public ActionResult<Menu> GetMenuByID(string id)
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
        public ActionResult<Menu> PostMenu(string FoodId, string FoodName, string Description, double? Price)
        {
            var menu = new Menu { FoodId = FoodId, FoodName = FoodName, Description = Description, Price = Price };
            repo.SaveMenu(menu);
            return Ok();
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("UpdateMenu")]
        public IActionResult UpdateRoom(string id, MenuDto menuDto)
        {
            var getMenu = repo.GetMenuById(id);
            if (getMenu == null)
            {
                return NotFound();
            }
            repo.UpdateMenu(menuDto);
            return Ok(menuDto);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("DeleteRoom{id}")]
        public IActionResult DeleteMenu(string id, Menu menu)
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
