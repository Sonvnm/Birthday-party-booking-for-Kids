using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirthdayPartyBookingForKids_Client.Pages.Customer
{
    public class BookingSuccessShowModel : PageModel
    {
		public string BookingInfo { get; set; }

		public IActionResult OnGet()
        {
            // Get the booked information from TempData
            BookingInfo = TempData["BookingInfo"]?.ToString();

            // Check if the booking information is available
            if (string.IsNullOrEmpty(BookingInfo))
            {
                // If not available, redirect to an error page or handle it accordingly
                return RedirectToPage("/ErrorPage");
            }

            return Page();
        }

    }
}
