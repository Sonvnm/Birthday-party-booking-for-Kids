using BusinessObject.Models;
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
        public ActionResult<Room>GetRoomByID(int id)
        {
            var room = repo.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }
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
        [HttpPut("UpdateRoom")]
        public IActionResult UpdateRoom(int id, Room room)
        {
            var getroom = repo.GetRoomById(id);
            if(getroom == null)
            {
                return NotFound();
            }
            repo.UpdateRoom(room);
            return Ok(room);
        }
        [HttpDelete("DeleteRoom")]
        public IActionResult DeleteRoom(int id, Room room) 
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
