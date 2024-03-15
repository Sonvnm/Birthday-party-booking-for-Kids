using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repositoties.IRepository;
using Repositoties.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BirthdayPartyBookingForKids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository repo = new UserRepository();
        private readonly IConfiguration configuration;
        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("Login")]
        public IActionResult Login(string email,  string password)
        {
            try
            {
                User user = repo.Login(email, password);
                if (user != null) { return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Authenticate success",
                    Data = GenerateToken(user)
                }); }
                else { return NotFound(); }
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        private string GenerateToken(User user)
        {
            var JwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(configuration["SecretKey"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("User Name", user.UserName),
                    new Claim("Email", user.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = JwtTokenHandler.CreateToken(tokenDescription);

            return JwtTokenHandler.WriteToken(token);

        }

        [HttpPost("Register")]
        public ActionResult Register(int userId, string userName, string email, string password, DateTime birthDate, string phone, string roleId)
        {

            try
            {
                User newUser = new User
                {
                    UserId = userId,
                    UserName = userName,
                    Email = email,
                    Password = password,
                    BirthDate = birthDate,
                    Phone = phone,
                    RoleId = roleId
                };
                repo.Register(newUser);
                return Ok();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
        }
        [HttpGet("GetAllUser")]
        [EnableQuery]
        [ODataRouteComponent]
        public ActionResult<IList<User>> GetAllUser()
        {
            IList<User> users = repo.GetAllUser();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public ActionResult<User> GetUserByID(int id)
        {
            var user = repo.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpGet("Search")]
        public ActionResult<User> GetUserByName(string name)
        {
            var user = repo.GetUserByName(name);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id.Equals( user.UserId))
            {
                return BadRequest();
            }

            var p1 = repo.GetUserById(id);

            if (p1 == null)
                return NotFound();
            repo.Update(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = repo.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            repo.Delete(user);
            return Ok();
        }

    }
}
