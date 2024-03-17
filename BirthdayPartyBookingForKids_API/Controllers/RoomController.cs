using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositoties.IRepository;
using Repositoties.Repository;

namespace BirthdayPartyBookingForKids_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository repo = new RoomRepository();

        [HttpGet("ListRoom")]
        public ActionResult<IEnumerable<Room>> GetAllRoom() => repo.GetAllRooms();

        [HttpGet("{id}")]
        public ActionResult<Room>GetRoomByID(string id)
        {
            var room = repo.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("Post new Room")]
        public IActionResult PostRoom(string LocationID, string LocationName, String Description, double Price)
        {
            var room = new Room();
            room.LocationId = LocationID;
            room.LocationName = LocationName;
            room.Description = Description;
            room.Price = Price;
                
            repo.SaveRoom(room);
            return Ok(room);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("UpdateRoom")]
        public IActionResult UpdateRoom(string id, Room room)
        {
            var getroom = repo.GetRoomById(id);
            if(getroom == null)
            {
                return NotFound();
            }
            repo.UpdateRoom(room);
            return Ok(room);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("DeleteRoom")]
        public IActionResult DeleteRoom(string id, Room room) 
        {
            var getroom = repo.GetRoomById(id);
            if (getroom == null)
            {
                return NotFound();
            }
            repo.DeleteRoom(room);
            return Ok();
        }
    }
}
