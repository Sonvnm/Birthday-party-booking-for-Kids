using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using BirthdayPartyBookingForKids_Client.Pages.ViewModels;
using Microsoft.AspNetCore.Http;

namespace BirthdayPartyBookingForKids_Client.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        [BindProperty]
        public LoginViewModel Login { get; set; }

        [ViewData]
        public string Message { get; set; }
        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> OnPost()
        {
            try
            {
                HttpClient client = new();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                string CustomerApiUrl = "https://localhost:7212/api/User/Login";
                string param = $"?email={Login.Email}&password={Login.Password}";
                HttpContent content = new StringContent(param, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(CustomerApiUrl + param, content);
                //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                //{
                //    string strData = await response.Content.ReadAsStringAsync();
                //    var options = new JsonSerializerOptions
                //    {
                //        PropertyNameCaseInsensitive = true
                //    };
                //    var result = JsonSerializer.Deserialize<BusinessObject.Models.User>(strData, options);
                //    HttpContext.Session.SetInt32("id", result.UserId);
                //    return RedirectToPage("/User/Index");
                //}
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return RedirectToPage("/User/Index");
                }
                else
                {
                    Message = "Incorrect email or password";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Page();
        }
    }
}
