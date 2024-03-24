using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BirthdayPartyBookingForKids_Client.Pages.Admin
{
    public class ListMenuModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginModel> _logger;
        private readonly HttpClient _httpClient;
        public ListMenuModel(IConfiguration configuration, ILogger<LoginModel> logger, HttpClient httpClient)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClient = httpClient;
        }

        public IList<Menu> Menu { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            var apiUrl = $"{_configuration["ApiBaseUrl"]}/api/Menu/ListMenu";
            var response = await _httpClient.GetAsync(apiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Menu = JsonSerializer.Deserialize<IList<Menu>>(strData, options);
            return Page();
        }
    }
}
