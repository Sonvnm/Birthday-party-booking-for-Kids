using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace BirthdayPartyBookingForKids_Client.Pages.Admin
{
    public class CreateServiceModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BookingModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateServiceModel(IConfiguration configuration, ILogger<BookingModel> logger, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            /*_httpClient.DefaultRequestHeaders.Accept.Add(contentType);*/


            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            /*_httpClient.DefaultRequestHeaders.Accept.Add(contentType);*/
            var responseD = await client.GetAsync("http://localhost:5297/api/Menu/ListMenu");
            var dataStringD = await responseD.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var menu = JsonSerializer.Deserialize<IEnumerable<Menu>>(dataStringD, options);

            responseD = await client.GetAsync("http://localhost:5297/api/Decoration/GetAllDecoration");
            dataStringD = await responseD.Content.ReadAsStringAsync();
            var decorations = JsonSerializer.Deserialize<IEnumerable<Decoration>>(dataStringD, options);

            ViewData["FoodId"] = new SelectList(menu, "FoodId", "FoodName");
            ViewData["ItemId"] = new SelectList(decorations, "ItemId", "ItemName");
            return Page();
        }

        [BindProperty]
        public Service Service { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            /*_httpClient.DefaultRequestHeaders.Accept.Add(contentType);*/

            var ApiUrl = $"{_configuration["ApiBaseUrl"]}/api/Service/Add?ServiceId={Service.ServiceId}&ServiceName={Service.ServiceName}&Description={Service.Description}&FoodId={Service.FoodId}&ItemId={Service.ItemId}&TotalPrice={Service.TotalPrice}";
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var jsonObject = JsonSerializer.Serialize(Service);
            HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(ApiUrl, content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToPage("/Admin/ListService");
            }
            else
            {
                return Page();
            }
        }
    }
}
