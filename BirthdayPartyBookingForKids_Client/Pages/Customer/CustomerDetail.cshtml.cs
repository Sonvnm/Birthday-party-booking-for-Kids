using BirthdayPartyBookingForKids_Client.ViewModel;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BirthdayPartyBookingForKids_Client.Pages.Customer
{
    public class CustomerDetailModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginModel> _logger;
        private readonly HttpClient _httpClient;
        public CustomerDetailModel(IConfiguration configuration, ILogger<LoginModel> logger, HttpClient httpClient)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClient = httpClient;
        }

        [BindProperty]
        public UserViewModel UserViewModel { get; set; }
        [BindProperty]
        public string UserId { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            
            var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;
            var claimUser = User.FindFirst("UserId")?.Value;
            UserId = claimUser;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            var apiUrl = $"{_configuration["ApiBaseUrl"]}/api/User/" + UserId;
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var user = JsonSerializer.Deserialize<User>(strData, options);
            var userViewModel = new UserViewModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Password = user.Password,
                Phone = user.Phone,
                RoleId = user.RoleId,
            };
            UserViewModel = userViewModel;
            if (UserViewModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
