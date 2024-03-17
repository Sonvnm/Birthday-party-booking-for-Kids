using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BirthdayPartyBookingForKids_Client.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginModel> _logger;
        private readonly HttpClient _httpClient;

        public LoginModel(IConfiguration configuration, ILogger<LoginModel> logger, HttpClient httpClient)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClient = httpClient;
        }

        /*[BindProperty]
        public User User { get; set; }
*/
        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // Prepare the login request data
/*                var loginData = new { UserName, Password };
*/
                // Send the login request to the API
                var apiUrl = $"{_configuration["ApiBaseUrl"]}/api/User/Login?username={UserName}&password={Password}";

                var response = await _httpClient.PostAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    // Retrieve the token from the response
                    var tokenResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    var token = tokenResponse.Token;

                    // Store the token in session or cookies for future use
                    await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, UserName), new Claim(ClaimTypes.Role, GetRoleFromToken(token)) }, "Cookies")));

                    // Redirect based on user role
                    return RedirectToRoleSpecificPage(token);
                }
                else
                {
                    _logger.LogError($"Failed to log in: {response.ReasonPhrase}");
                    ModelState.AddModelError(string.Empty, "Failed to log in. Please check your credentials and try again.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred. Please try again later.");
            }

            return Page();
        }

        private string GetRoleFromToken(string token)
        {
            /*var claims = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
            var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            return roleClaim ?? "2"*/;

            /*var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                var claims = new ClaimsPrincipal(new ClaimsIdentity(jsonToken.Claims));
                var roleClaim = claims.FindFirst(ClaimTypes.Role)?.Value;
                return roleClaim ?? "2"; // Default to "2" if role claim not found
            }
            else
            {
                return "2"; 
            }*/

            var claims = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
            var roleClaim = claims.FirstOrDefault(c => c.Type == "role")?.Value;

            if (roleClaim == null)
            {
                _logger.LogError("Role claim not found in token.");
            }
            else
            {
                _logger.LogInformation($"Role claim found: {roleClaim}");
            }

            return roleClaim ?? "2";
        }

        private IActionResult RedirectToRoleSpecificPage(string token)
        {
            var role = GetRoleFromToken(token);

            // Redirect based on user role
            if (role == "1")
            {
                return RedirectToPage("/Admin/Index");
            }
            else if (role == "2")
            {
                return RedirectToPage("/Customer/Booking");
            }
            else
            {
                // Handle unknown role
                _logger.LogError($"Unknown role: {role}");
                return RedirectToPage("/Index"); // Redirect to home page for now
            }
        }
    }
}
