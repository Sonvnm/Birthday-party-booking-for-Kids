using BusinessObject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using static BirthdayPartyBookingForKids_API.Controllers.ServiceController;

namespace BirthdayPartyBookingForKids_Client.Pages
{
    public class BookingModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BookingModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingModel(IConfiguration configuration, ILogger<BookingModel> logger, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public Booking Booking { get; set; }
        [BindProperty]
        public string UserId { get; set; }
        [BindProperty]
        public int ParticipateAmount { get; set; }
        [BindProperty]
        public DateTime DateBooking { get; set; }
        [BindProperty]
        public string LocationId { get; set; }
        [BindProperty]
        public string ServiceId { get; set; }
        [BindProperty]
        public DateTime KidBirthday { get; set; }
        [BindProperty]
        public string KidName { get; set; }
        [BindProperty]
        public string KidGender { get; set; }
        [BindProperty]
        public string Time { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public IEnumerable<SelectListItem> Locations { get; private set; }
        public IEnumerable<SelectListItem> Services { get; private set; }

        public async Task OnGetAsync()
        {
            await LoadLocationsAsync();
            await LoadServicesAsync();
        }

        private async Task LoadLocationsAsync()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiHttpClient");
                var apiUrl = "api/Room/ListRoom"; 
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var rooms = await response.Content.ReadFromJsonAsync<IEnumerable<Room>>();
                    Locations = ConvertToSelectListItems(rooms);
                }
                else
                {
                    _logger.LogError($"Failed to fetch rooms: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading locations: {ex.Message}");
            }
        }

        private async Task LoadServicesAsync()
        {
            try
            {
                // Create an instance of HttpClient using the factory
                var client = _httpClientFactory.CreateClient("ApiHttpClient");

                var apiUrl = $"{_configuration["ApiBaseUrl"]}/api/Service/GetAllService";
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var services = await response.Content.ReadFromJsonAsync<IEnumerable<Service>>();
                    Services = ConvertToSelectListItems(services);
                }
                else
                {
                    _logger.LogError($"Failed to fetch services: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading services: {ex.Message}");
            }
        }

        private IEnumerable<SelectListItem> ConvertToSelectListItems(IEnumerable<Room> rooms)
        {
            // Convert Room Model objects to SelectListItem objects
            var selectListItems = new List<SelectListItem>();
            foreach (var room in rooms)
            {
                selectListItems.Add(new SelectListItem { Value = room.LocationId.ToString(), Text = room.LocationName });
            }
            return selectListItems;
        }

        private IEnumerable<SelectListItem> ConvertToSelectListItems(IEnumerable<Service> services)
        {
            // Convert Service Model objects to SelectListItem objects
            var selectListItems = new List<SelectListItem>();
            foreach (var service in services)
            {
                selectListItems.Add(new SelectListItem { Value = service.ServiceId.ToString(), Text = service.ServiceName });
            }
            return selectListItems;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            { 
                var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;
                var claimUser = User.FindFirst("UserId")?.Value;
                UserId = claimUser;
                var apiUrl = $"{_configuration["ApiBaseUrl"]}/api/Booking/CreateBooking?userId={UserId}&participateAmount={ParticipateAmount}&dateBooking={DateBooking}&locationId={LocationId}&serviceId={ServiceId}&kidBirthday={KidBirthday}&kidName={KidName}&kidGender={KidGender}&time={Time}";


                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var content = new StringContent(JsonConvert.SerializeObject(Booking), Encoding.UTF8, "application/json");


                var response = await client.PostAsync(apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    // Read and log the response content
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error response content: {errorContent}");                 
                }

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content which should contain the booked information
                    var responseBody = await response.Content.ReadAsStringAsync();
                    SuccessMessage = "Booking successfully";
                    return Page();
                }
                else
                {
                    // Handle the error response
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    return Page();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating booking: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return Page();
            }
        }


    }

}
