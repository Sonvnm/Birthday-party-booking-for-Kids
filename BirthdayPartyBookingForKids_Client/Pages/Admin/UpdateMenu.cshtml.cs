using BirthdayPartyBookingForKids_Client.ViewModel;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BirthdayPartyBookingForKids_Client.Pages.Admin
{
    public class UpdateMenuModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BookingModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient client = null;

        public UpdateMenuModel(IConfiguration configuration, ILogger<BookingModel> logger, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        [BindProperty]
        public MenuViewModel MenuViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            client = new HttpClient();
            var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            var apiUrl = $"{_configuration["ApiBaseUrl"]}/api/Menu/" + id;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var menu = System.Text.Json.JsonSerializer.Deserialize<MenuViewModel>(strData, options);
            var menuViewModel = new MenuViewModel
            {
                FoodId = menu.FoodId,
                FoodName = menu.FoodName,
                Description = menu.Description,
                Price = menu.Price,
            };
            MenuViewModel = menuViewModel;
            if (MenuViewModel == null)
            {
                return NotFound();
            }
            return Page();

            /*if (id == null)
            {
                return NotFound();
            }
            var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;
            var client = _httpClientFactory.CreateClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            var apiUrl = $"{_configuration["ApiBaseUrl"]}/api/Menu/" + id;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var menu = System.Text.Json.JsonSerializer.Deserialize<Menu>(strData, options);
            var menuViewModel = new MenuViewModel
            {
                FoodId = menu.FoodId,
                FoodName = menu.FoodName,
                Description = menu.Description,
                Price = menu.Price,
            };
            MenuViewModel = menuViewModel;
            if (MenuViewModel == null)
            {
                return NotFound();
            }
            return Page();*/
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                client = new HttpClient();
                var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var apiUrl = $"{_configuration["ApiBaseUrl"]}/api/Menu/UpdateMenu";
                string param = $"/{MenuViewModel.FoodId}";
                var menu = new MenuViewModel
                {
                    FoodId = MenuViewModel.FoodId,
                    FoodName = MenuViewModel.FoodName,
                    Description = MenuViewModel.Description,
                    Price = MenuViewModel.Price,
                };
                var jsonObject = System.Text.Json.JsonSerializer.Serialize(menu);
                HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(apiUrl + param, content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    return Page();
                }
                /* var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                 client.DefaultRequestHeaders.Accept.Add(contentType);*/
                /*var apiUrl = $"{_configuration["ApiBaseUrl"]}/api/Menu/UpdateMenu";
                string param = $"/{MenuViewModel.FoodId}";

                var menu = new MenuViewModel
                {
                    FoodId = MenuViewModel.FoodId,
                    FoodName= MenuViewModel.FoodName,
                    Description = MenuViewModel.Description,
                    Price = MenuViewModel.Price,
                };
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var jsonObject = JsonConvert.SerializeObject(menu);
                HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(apiUrl + param , content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    return Page();
                }*/
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
