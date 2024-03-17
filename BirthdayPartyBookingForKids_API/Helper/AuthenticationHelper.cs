using BusinessObject.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BirthdayPartyBookingForKids_API.Helpers
{
    public static class AuthenticationHelper
    {
        public static string GenerateJwtToken(User user, string secretKey, string issuer, string audience)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var claims = new List<Claim>
            {
                new Claim("UserId", user.UserId),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.RoleId),
                // Add other claims as needed
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };

            SecurityToken token;
            try
            {
                token = tokenHandler.CreateToken(tokenDescriptor);
            }
            catch (Exception ex)
            {
                // Handle token creation errors
                Console.WriteLine($"Error creating JWT token: {ex.Message}");
                throw;
            }

            return tokenHandler.WriteToken(token);
        }
    }
}
