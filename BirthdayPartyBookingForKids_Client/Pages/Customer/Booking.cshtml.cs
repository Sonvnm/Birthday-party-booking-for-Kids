using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirthdayPartyBookingForKids_Client.Pages
{
    public class BookingModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BookingModel> _logger;

        public BookingModel(IConfiguration configuration, ILogger<BookingModel> logger)
        {
            _configuration = configuration;
            _logger = logger;

            AvailableMenus = GetAvailableMenusAsync().Result;

        }

        [BindProperty]
        public Booking Booking { get; set; }

        // Store the available menu items
        public List<Menu> AvailableMenus { get; set; }

        // Store the selected food items
        [BindProperty]
        public string[] SelectedFoods { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                AvailableMenus = await GetAvailableMenusAsync();

                // Assign the selected food items to the Booking model
                Booking.SelectedFoods = await GetSelectedFoodsAsync();

                // Submit the booking data to the BookingController
                using (HttpClient client = new HttpClient())
                {
                    // Adjust the URL based on your project structure and routing
                    string apiUrl = $"{_configuration["ApiBaseUrl"]}/api/Booking/SubmitBooking";
                    HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, Booking);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToPage("/Confirmation");
                    }
                    else
                    {
                        _logger.LogError($"Failed to submit booking: {response.ReasonPhrase}");
                        ModelState.AddModelError(string.Empty, "Failed to submit booking. Please try again later.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred. Please try again later.");
            }

            AvailableMenus = await GetAvailableMenusAsync();

            return Page();
        }

        private async Task<List<Menu>> GetAvailableMenusAsync()
        {
            // Implement logic to retrieve available menu items
            // This could involve fetching menu items from your API or another data source

            // For simplicity, let's assume you have a method in your API for getting all menus
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"{_configuration["ApiBaseUrl"]}/api/MenuController/GetAllMenus";
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Menu>>();
                }

                // Handle error or return an empty list
                return new List<Menu>();
            }
        }

        private async Task<List<Menu>> GetSelectedFoodsAsync()
        {
            // Implement logic to retrieve selected food items based on the selected IDs
            // This could involve fetching menu items from your API or another data source

            // For simplicity, let's assume you have a method in your API for getting menus by IDs
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"{_configuration["ApiBaseUrl"]}/api/MenuController/GetMenuByID";
                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, SelectedFoods);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Menu>>();
                }

                // Handle error or return an empty list
                return new List<Menu>();
            }
        }
    }


}
