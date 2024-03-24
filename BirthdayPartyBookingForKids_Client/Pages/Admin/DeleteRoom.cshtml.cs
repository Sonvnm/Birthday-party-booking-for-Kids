using BirthdayPartyBookingForKids_Client.ViewModel;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BirthdayPartyBookingForKids_Client.Pages.Admin
{
    public class DeleteRoomModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        public DeleteRoomModel(IConfiguration configuration, ILogger<LoginModel> logger, IHttpClientFactory httpClientFactory,HttpClient httpClient)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _httpClient = httpClient;
        }

        [BindProperty]
        public RoomViewModel RoomViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = _httpClientFactory.CreateClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            var apiUrl = $"{_configuration["ApiBaseUrl"]}/api/Room/" + id;
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var room = JsonSerializer.Deserialize<Room>(strData, options);
            var roomViewModel = new RoomViewModel
            {
                LocationId = room.LocationId,
                LocationName = room.LocationName,
                Description = room.Description,
                Price= room.Price,
            };
            RoomViewModel = roomViewModel;
            if (RoomViewModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            var apiUrl = $"{_configuration["ApiBaseUrl"]}/api/Room/DeleteRoom/" + id;
            HttpResponseMessage response = await client.DeleteAsync(apiUrl);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToPage("/Admin/ListRoom");
            }
            else
            {
                return Page();
            }
        }
    }
}
