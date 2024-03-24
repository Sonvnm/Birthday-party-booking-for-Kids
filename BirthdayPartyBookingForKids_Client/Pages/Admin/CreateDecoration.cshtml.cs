using BirthdayPartyBookingForKids_Client.ViewModel;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BirthdayPartyBookingForKids_Client.Pages.Admin
{
    public class CreateDecorationModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BookingModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateDecorationModel(IConfiguration configuration, ILogger<BookingModel> logger, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        [BindProperty]
        public DecorationViewModel DecorationViewModel { get; set; }
        [BindProperty]
        public string ItemId { get; set; }
        [BindProperty]
        public string ItemName { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public double? Price { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            /*_httpClient.DefaultRequestHeaders.Accept.Add(contentType);*/
            string ApiUrl = $"{_configuration["ApiBaseUrl"]}/api/Decoration/Add?itemId={ItemId}&itemName={ItemName}&description={Description}&price={Price}";
            var decoration = new Decoration
            {
                ItemId = DecorationViewModel.ItemId,
                ItemName = DecorationViewModel.ItemId,
                Description = DecorationViewModel.ItemId,
                Price = DecorationViewModel.Price
            };
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var jsonObject = JsonSerializer.Serialize(decoration);
            HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(ApiUrl, content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToPage("/Admin/ListDecoration");
            }
            else
            {
                return Page();
            }
        }
    }
}
