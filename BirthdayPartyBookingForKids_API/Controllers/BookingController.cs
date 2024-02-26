using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositoties.IRepository;

namespace BirthdayPartyBookingForKids_API.Controllers
{
    
    [ApiController]
    [ODataRouteComponent("Bookings")]
    public class BookingController : ODataController
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [EnableQuery]
        [ODataRouteComponent]
        public IActionResult Get()
        {
            try
            {
                var bookings = _bookingRepository.GetAllBookings();
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get Bookings: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [EnableQuery]
        [ODataRouteComponent("({key})")]
        public IActionResult Get([FromODataUri] string key)
        {
            try
            {
                var booking = _bookingRepository.GetBookingById(key);

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

                _bookingRepository.AddBooking(booking);
                return Created(booking);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Post Booking: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPatch]
        [ODataRouteComponent("({key})")]
        public IActionResult Update([FromODataUri] string key, [FromBody] Delta<Booking> bookingDelta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var booking = _bookingRepository.GetBookingById(key);

                if (booking == null)
                {
                    return NotFound();
                }

                bookingDelta.Patch(booking);
                _bookingRepository.UpdateBooking(booking);

                return Updated(booking);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Patch Booking: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete]
        [ODataRouteComponent("({key})")]
        public IActionResult Delete([FromODataUri] string key)
        {
            try
            {
                var booking = _bookingRepository.GetBookingById(key);

                if (booking == null)
                {
                    return NotFound();
                }

                _bookingRepository.DeleteBooking(key);

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
