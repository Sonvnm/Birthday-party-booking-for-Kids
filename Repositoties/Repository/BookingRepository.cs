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
        private readonly BookingDAO _bookingDAO;

        public BookingRepository(BookingDAO bookingDAO)
        {
            _bookingDAO = bookingDAO;
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _bookingDAO.GetAllBookings();
        }

        public Booking GetBookingById(string bookingId)
        {
            return _bookingDAO.GetBookingById(bookingId);
        }

        public void AddBooking(Booking booking)
        {
            _bookingDAO.AddBooking(booking);
        }

        public void UpdateBooking(Booking booking)
        {
            _bookingDAO.UpdateBooking(booking);
        }

        public void DeleteBooking(string bookingId)
        {
            _bookingDAO.DeleteBooking(bookingId);
        }
    }
}
