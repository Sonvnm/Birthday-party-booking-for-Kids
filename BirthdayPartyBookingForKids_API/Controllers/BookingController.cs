using BusinessObject.Models;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Repositoties.IRepository;
using Repositoties.Repository;
using System.Security.Claims;

namespace BirthdayPartyBookingForKids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ODataRouteComponent("Bookings")]
    public class BookingController : ODataController
    {
        /*private BookingRepository _bookingRepository;

        public BookingController(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }*/

        private readonly IBookingRepository repo = new BookingRepository();
        private readonly IRoomRepository rRepo = new RoomRepository();
        private readonly IServiceRepository iRepo = new ServiceRepository();

        [HttpGet("GetAllBooking")]
        [EnableQuery]
        [ODataRouteComponent]
        public IActionResult Get()
        {
            try
            {
                // Check if the user is an admin
                if (User.IsInRole("1"))
                {
                    // Admin has full authority, retrieve all bookings
                    var allBookings = repo.GetAllBookings();
                    return Ok(allBookings);
                }

                // For regular users, get their ID from claims
                var userId = User.FindFirst("UserId")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User not authenticated");
                }

                // Retrieve bookings for the authenticated user only
                var userBookings = repo.GetBookingsForUser(userId);

                return Ok(userBookings);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get Bookings: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        [EnableQuery]
        [ODataRouteComponent("({key})")]
        public IActionResult Get([FromODataUri] string key)
        {
            try
            {
                var booking = repo.GetBookingById(key);

                if (booking == null)
                {
                    return NotFound();
                }

                return Ok(booking);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get Booking by Id: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("CreateBooking")]
        [ODataRouteComponent]
        public ActionResult CreateBooking(string userId,int participateAmount,DateTime dateBooking,string locationId,string serviceId,DateTime kidBirthday, string kidName, string kidGender,string time)
        {
            string bookingId = Guid.NewGuid().ToString();
/*            userId = User.FindFirst("UserId")?.Value;
*/            int status = 1;

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID not found in claims.");
            }

            try
            {
				var room = rRepo.GetRoomById(locationId);
				var service = iRepo.GetServiceById(serviceId);

				if (room == null)
				{
					return BadRequest("Invalid room ID.");
				}

				if (service == null)
				{
					return BadRequest("Invalid service ID.");
				}

				// Calculate total price as the sum of room price and service price
				double totalPrice = (room.Price ?? 0) + (service.TotalPrice ?? 0);

				Booking newBooking = new Booking
                {
                    BookingId = bookingId,
                    UserId = userId,
                    ParticipateAmount = participateAmount,
                    TotalPrice = totalPrice,
                    DateBooking = dateBooking,
                    LocationId = locationId,
                    ServiceId = serviceId,
                    KidBirthDay= kidBirthday,
                    KidName= kidName,
                    KidGender= kidGender,
                    Time= time,
                    Status = status
                };
                repo.AddBooking(newBooking);
                return Ok(newBooking);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }

            /*try
            {
                // Remove the UserId from the model since it's passed as a parameter
                model.UserId = HttpContext.Request.Query["userId"].ToString(); // Assuming userId is the parameter name

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if the room and time are already booked for the specified date
                if (repo.IsRoomAlreadyBooked(model.LocationId, model.DateBooking ?? DateTime.MinValue, model.Time))
                {
                    return BadRequest("This room is already booked for the specified date and time. Please choose another room or time.");
                }

                // Generate a unique BookingId using a GUID
                model.BookingId = Guid.NewGuid().ToString();

                repo.AddBooking(model);
                return Created(model);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Post Booking: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }*/
        }





        [HttpPut]
        [ODataRouteComponent("({key})")]
        public IActionResult Update([FromODataUri] string key, [FromBody] Delta<Booking> bookingDelta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var booking = repo.GetBookingById(key);

                if (booking == null)
                {
                    return NotFound();
                }

                bookingDelta.Patch((Booking)booking);
                repo.UpdateBooking((Booking)booking);

                return Updated(booking);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Patch Booking: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{bookingId}")]
        [ODataRouteComponent("({key})")]
        public IActionResult Delete([FromODataUri] string key)
        {
            try
            {
                var booking = repo.GetBookingById(key);

                if (booking == null)
                {
                    return NotFound();
                }

                repo.DeleteBooking(key);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Delete Booking: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
