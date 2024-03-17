using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositoties.Repository;

namespace BirthdayPartyBookingForKids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleRepository repo = new RoleRepository();

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<ActionResult<IList<Role>>> GetRoles()
        {
            var items = repo.GetRoles();
            return Ok(items);
        }
    }
}
