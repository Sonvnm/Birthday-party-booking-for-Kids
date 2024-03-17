using BusinessObject.Models;
using DataAccess;
using Repositoties.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoties.Repository
{
    public class BookingRepository : IBookingRepository
    {
        /*private readonly BookingDAO _bookingDAO;

        public BookingRepository(BookingDAO bookingDAO)
        {
            _bookingDAO = bookingDAO;
        }*/

        public IList<Booking> GetAllBookings() => BookingDAO.GetAllBookings();

        public IList<Booking> GetBookingById(string bookingId) => BookingDAO.GetBookingById(bookingId);
        

        public void AddBooking(Booking booking)
        {

            // Check kid age
            if (CalculateAge(booking.KidBirthDay) >= 16)
            {
                throw new InvalidOperationException("Kid age must be below 16 for booking.");
            }
            // Calculate total price
            booking.TotalPrice = BookingDAO.CalculateTotalPrice(booking.LocationId, booking.ServiceId);
            BookingDAO.AddBooking(booking);
        }

        private int CalculateAge(DateTime? birthdate)
        {
            if (birthdate.HasValue)
            {
                DateTime today = DateTime.Today;
                int age = today.Year - birthdate.Value.Year;
                if (birthdate.Value.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }

            return 0; // Default age if birthdate is null
        }

        public void UpdateBooking(Booking booking) => BookingDAO.UpdateBooking(booking);
        

        public void DeleteBooking(string bookingId) => BookingDAO.DeleteBooking(bookingId);
        

        public IList<Booking> GetBookingsForUser(string userId) => BookingDAO.GetBookingsForUser(userId);


        public bool IsRoomAlreadyBooked(string locationId, DateTime date, string time) => BookingDAO.IsRoomAlreadyBooked(locationId, date, time);
    }
}
