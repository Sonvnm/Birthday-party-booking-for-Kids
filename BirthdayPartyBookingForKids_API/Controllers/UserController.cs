﻿using BirthdayPartyBookingForKids_API.Helpers;
using BusinessObject.Models;
using DataAccess.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositoties.IRepository;
using Repositoties.Repository;

namespace BirthdayPartyBookingForKids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repo = new UserRepository();

        [HttpPost("Login")]
        public IActionResult Login(string username, string password)
        {
            try
            {
                User user = repo.Login(username, password);
                if (user != null)
                {
                    var token = AuthenticationHelper.GenerateJwtToken(user, "birthday-booking-party-project-secret-key", "birthday-booking-party-issuer", "birthday-booking-party-audience");
                    return Ok(new { Token = token });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("Register")]
        public ActionResult Register(string userId, string userName, string email, string password, DateTime birthDate, string phone, string roleId)
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
        public ActionResult<IList<User>> GetAllUser()
        {
            IList<User> users = repo.GetAllUser();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public ActionResult<User> GetUserByID(string id)
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
        public IActionResult PutUser(string id, UserDto userDto)
        {
            if (id != userDto.UserId)
            {
                return BadRequest();
            }

            var p1 = repo.GetUserById(id);

            if (p1 == null)
                return NotFound();
            repo.Update(userDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
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
