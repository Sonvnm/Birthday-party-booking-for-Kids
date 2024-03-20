using System;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BirthdayPartyBookingForKids_Client.ViewModel;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BirthdayPartyBookingForKids_Client.Pages
{
    public class RegisterModel : PageModel
    {
        
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginModel> _logger;
        private readonly HttpClient _httpClient;

        public RegisterModel(IConfiguration configuration, ILogger<LoginModel> logger, HttpClient httpClient)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClient = httpClient;
            
        }
        //public IActionResult OnGet()
        //{
        //    return Page();
        //}

        [BindProperty]
        public UserViewModel UserViewModel { get; set; }
        [BindProperty]
        public string UserId { get; set; }
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public DateTime BirthDay { get; set; }
        [BindProperty]
        public string Phone { get; set; }
        [BindProperty]
        public string RoleId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            string ApiUrl = $"{_configuration["ApiBaseUrl"]}/api/User/Register?userId={UserId}&userName={UserName}&email={Email}&password={Password}&birthDate={BirthDay}&phone={Phone}&roleId={RoleId}";
            var user = new User
            {
                UserId = UserViewModel.UserId,
                UserName = UserViewModel.UserName,
                Email = UserViewModel.Email,
                BirthDate = UserViewModel.BirthDate,
                Password = UserViewModel.Password,
                Phone = UserViewModel.Phone,
                RoleId = UserViewModel.RoleId,
            };
            var jsonObject = JsonSerializer.Serialize(user);
            HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(ApiUrl, content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                return Page();
            }
        }

    }
}
