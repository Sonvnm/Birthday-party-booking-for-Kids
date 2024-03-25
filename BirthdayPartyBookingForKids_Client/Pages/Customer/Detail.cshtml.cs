using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BirthdayPartyBookingForKids_Client.Pages.Customer
{
    public class DetailModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public DetailModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public IList<Booking> Bookings { get; private set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                // Get the access token from the HttpContext
                var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;


                // Call the API to get the user's booking history using the access token
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{_configuration["ApiBaseUrl"]}/api/Booking/GetBookingById?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response and display the user's booking history
                    var bookingData = await response.Content.ReadAsStringAsync();
                    var bookings = JsonConvert.DeserializeObject<List<Booking>>(bookingData);

                    // Fetch Location and Service details and replace IDs with names
                    await FetchLocationAndServiceDetails(bookings);

                    Bookings = bookings;
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

        private async Task FetchLocationAndServiceDetails(List<Booking> bookings)
        {
            var client = _httpClientFactory.CreateClient();
            var locationIds = bookings.Select(b => b.LocationId).Distinct().ToList();
            var serviceIds = bookings.Select(b => b.ServiceId).Distinct().ToList();

            foreach (var booking in bookings)
            {
                // Fetch Location details
                var locationResponse = await client.GetAsync($"{_configuration["ApiBaseUrl"]}/api/Room/GetRoomByID?id={booking.LocationId}");
                if (locationResponse.IsSuccessStatusCode)
                {
                    var locationData = await locationResponse.Content.ReadAsStringAsync();
                    var location = JsonConvert.DeserializeObject<Room>(locationData);
                    booking.LocationId = location.LocationName;
                }

                // Fetch Service details
                var serviceResponse = await client.GetAsync($"{_configuration["ApiBaseUrl"]}/api/Service/GetServiceById?id={booking.ServiceId}");
                if (serviceResponse.IsSuccessStatusCode)
                {
                    var serviceData = await serviceResponse.Content.ReadAsStringAsync();
                    var service = JsonConvert.DeserializeObject<Service>(serviceData);
                    booking.ServiceId = service.ServiceName;
                }
            }
        }
    }
}
