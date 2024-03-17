using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var participateAmount = Booking.ParticipateAmount;
                var dateBooking = Booking.DateBooking;
                var locationId = Booking.LocationId;
                var serviceId = Booking.ServiceId;
                var kidBirthday = Booking.KidBirthDay;
                var kidName = Booking.KidName;
                var kidGender = Booking.KidGender;
                var time = Booking.Time;

                var apiUrl = $"{_configuration["ApiBaseUrl"]}/api/Booking/CreateBooking?userId={userId}&participateAmount={participateAmount}&dateBooking={dateBooking}&locationId={locationId}&serviceId={serviceId}&kidBirthday={kidBirthday}&kidName={kidName}&kidGender={kidGender}&time={time}";
/*				var content = new StringContent(JsonConvert.SerializeObject(Booking), Encoding.UTF8, "application/json");
*/
                // Make the HTTP POST request
                var response = await _httpClient.PostAsync(apiUrl, null);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content which should contain the booked information
                    var responseBody = await response.Content.ReadAsStringAsync();

                    // Pass the booked information to the success page using TempData
                    var bookingInfo = JsonConvert.DeserializeObject<Booking>(responseBody);

                    double totalPrice = (double)(bookingInfo.Location.Price + bookingInfo.Service.TotalPrice);

                    TempData["BookingInfo"] = bookingInfo;
                    TempData["TotalPrice"] = totalPrice.ToString("C");

                    // Redirect to the success page
                    return RedirectToPage("/Customer/BookingSuccessShow");
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
