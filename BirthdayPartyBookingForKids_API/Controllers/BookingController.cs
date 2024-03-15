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

        [HttpPost]
        [ODataRouteComponent]
        public IActionResult Post([FromBody] Booking booking)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if the room and time are already booked for the specified date
                if (repo.IsRoomAlreadyBooked(booking.LocationId, booking.DateBooking ?? DateTime.MinValue, booking.Time))
                {
                    return BadRequest("This room is already booked for the specified date and time. Please choose another room or time.");
                }

                // Generate a unique BookingId using a GUID
                booking.BookingId = Guid.NewGuid().ToString();

                repo.AddBooking(booking);
                return Created(booking);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Post Booking: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
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
