using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookingDAO
    {
        private readonly BirthdayPartyBookingForKids_DBContext _context;

        public BookingDAO(BirthdayPartyBookingForKids_DBContext context)
        {
            _context = context;
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            try
            {
                return _context.Bookings.ToList();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error in GetAllBookings: {ex.Message}");
                throw; // Re-throw the exception to propagate it
            }
        }

        public Booking GetBookingById(string bookingId)
        {
            try
            {
                return _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetBookingById: {ex.Message}");
                throw;
            }
        }

        public void AddBooking(Booking booking)
        {
            try
            {
                _context.Bookings.Add(booking);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddBooking: {ex.Message}");
                throw;
            }
        }

        public void UpdateBooking(Booking booking)
        {
            try
            {
                _context.Update(booking);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateBooking: {ex.Message}");
                throw;
            }
        }

        public void DeleteBooking(string bookingId)
        {
            try
            {
                var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
                if (booking != null)
                {
                    _context.Bookings.Remove(booking);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteBooking: {ex.Message}");
                throw;
            }
        }
    }
}
