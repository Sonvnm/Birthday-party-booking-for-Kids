using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using static BirthdayPartyBookingForKids_API.Controllers.ServiceController;

namespace BirthdayPartyBookingForKids_Client.Pages
{
    public class BookingModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BookingModel> _logger;
        private readonly HttpClient _httpClient;

        public BookingModel(IConfiguration configuration, ILogger<BookingModel> logger, HttpClient httpClient)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClient = httpClient;
        }

        [BindProperty]
        public Booking Booking { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<SelectListItem> Locations { get; private set; }
        public IEnumerable<SelectListItem> Services { get; private set; }

        public async Task OnGetAsync()
        {
            await LoadLocationsAsync();
            await LoadServicesAsync();
        }

        private async Task LoadLocationsAsync()
        {
            var apiUrl = $"{_configuration["ApiBaseUrl"]}/api/Room/ListRoom";
            var response = await _httpClient.GetAsync(apiUrl);
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

        private async Task LoadServicesAsync()
        {
            var apiUrl = $"{_configuration["ApiBaseUrl"]}/api/Service/GetAllService";
            var response = await _httpClient.GetAsync(apiUrl);
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
    }



}
