using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;

namespace BirthdayPartyBookingForKids_Client.Pages.Customer
{
    public class HistoryModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HistoryModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public IList<Booking> Bookings { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                // Get the access token from the HttpContext
                var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;


                // Call the API to get the user's booking history using the access token
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{_configuration["ApiBaseUrl"]}/api/Booking/GetAllBooking");

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response and display the user's booking history
                    var bookingData = await response.Content.ReadAsStringAsync();
                    Bookings = JsonConvert.DeserializeObject<List<Booking>>(bookingData);
                    return Page();
                }
                else
                {
                    // Handle unsuccessful API response
                    return RedirectToPage("/Error", new { message = "Failed to retrieve booking history." });
                }
            }
            catch (Exception ex)
            {
                // Log and handle any exceptions
                Console.WriteLine($"Error in retrieving booking history: {ex.Message}");
                return RedirectToPage("/Error", new { message = "An error occurred while processing your request." });
            }
        }
    }
}
