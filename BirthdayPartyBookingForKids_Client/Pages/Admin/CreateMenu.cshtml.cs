using BirthdayPartyBookingForKids_Client.ViewModel;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BirthdayPartyBookingForKids_Client.Pages.Admin
{
    public class CreateMenuModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BookingModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateMenuModel(IConfiguration configuration, ILogger<BookingModel> logger, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        [BindProperty]
        public MenuViewModel MenuViewModel { get; set; }
        [BindProperty]
        public string FoodId { get; set; }
        [BindProperty]
        public string FoodName { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public double? Price { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            /*_httpClient.DefaultRequestHeaders.Accept.Add(contentType);*/
            string ApiUrl = $"{_configuration["ApiBaseUrl"]}/api/Menu/Post new Menu?foodId={FoodId}&foodName={FoodName}&description={Description}&price={Price}";
            var menu = new Menu
            {
                FoodId = MenuViewModel.FoodId,
                FoodName = MenuViewModel.FoodId,
                Description = MenuViewModel.Description,
                Price = MenuViewModel.Price
            };
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var jsonObject = JsonSerializer.Serialize(menu);
            HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(ApiUrl, content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToPage("/Admin/ListMenu");
            }
            else
            {
                return Page();
            }
        }
    }
}
