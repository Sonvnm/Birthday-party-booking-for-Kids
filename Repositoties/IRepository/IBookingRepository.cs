using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoties.IRepository
{
    public interface IBookingRepository
    {
        IList<Booking> GetAllBookings();
        IList<Booking> GetBookingsForUser(string userId);
        bool IsRoomAlreadyBooked(string locationId, DateTime date, string time);

        IList<Booking> GetBookingById(string bookingId);
        void AddBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void DeleteBooking(string bookingId);
    }
}
