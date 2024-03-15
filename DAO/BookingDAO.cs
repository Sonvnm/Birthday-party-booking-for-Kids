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

        // Get User Booking History
        public static IList<Booking> GetBookingsForUser(string userId)
        {
            using var context = new BirthdayPartyBookingForKids_DBContext();
            return context.Bookings.Where(b => b.UserId == userId).ToList();
        }

        public static IList<Booking> GetAllBookings()
        {
            var listBooking = new List<Booking>();
            try
            {
                using (var context = new BirthdayPartyBookingForKids_DBContext())
                {
                    listBooking = context.Bookings.ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error in GetAllBookings: {ex.Message}");
                throw; // Re-throw the exception to propagate it
            }
            return listBooking;
        }

        public static IList<Booking> GetBookingById(string bookingId)
        {
            var listBooking = new List<Booking>();
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                listBooking = context.Bookings.Where(b => b.BookingId== bookingId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetBookingById: {ex.Message}");
                throw;
            }
            return listBooking;
        }

        public static void AddBooking(Booking booking)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                context.Bookings.Add(booking);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddBooking: {ex.Message}");
                throw;
            }
        }

        public static void UpdateBooking(Booking booking)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                context.Update(booking);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateBooking: {ex.Message}");
                throw;
            }
        }

        public static void DeleteBooking(string bookingId)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                var booking = context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
                if (booking != null)
                {
                    context.Bookings.Remove(booking);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteBooking: {ex.Message}");
                throw;
            }
        }

        // Check if the room is booked yet
        public static bool IsRoomAlreadyBooked(string locationId, DateTime? date, string time)
        {
            using var context = new BirthdayPartyBookingForKids_DBContext();

            return context.Bookings.Any(b => b.LocationId == locationId && b.DateBooking == date && b.Time == time);
        }

        // Calculate Booking Total Price
        public static double CalculateTotalPrice(string roomId, string serviceId)
        {
            using var context = new BirthdayPartyBookingForKids_DBContext();

            var roomPrice = context.Rooms
                .Where(r => r.LocationId == roomId)
                .Select(r => r.Price)
                .FirstOrDefault() ?? 0;

            var serviceTotalPrice = context.Services
                .Where(s => s.ServiceId == serviceId)
                .Select(s =>
                    (s.Food != null ? s.Food.Price : 0) +
                    (s.Item != null ? s.Item.Price : 0))
                .FirstOrDefault();

            return (double)(roomPrice + serviceTotalPrice);
        }
    }
}
