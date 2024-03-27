using BusinessObject;
using BusinessObject.Models;
using DataAccess.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = BusinessObject.Models.User;

namespace DataAccess
{
    public class UserDAO
    {
        public static User Login(string Username, string password)
        {
            using var context = new BirthdayPartyBookingForKids_DBContext();
            return context.Users.SingleOrDefault(c => c.UserName == Username && c.Password == password);
        }
        public static IList<BusinessObject.Models.User> GetAllUser()
        {
            var listUsers = new List<BusinessObject.Models.User>();
            try
            {
                using (var context = new BirthdayPartyBookingForKids_DBContext())
                {
                    listUsers = context.Users.Where(c => c.RoleId.Equals("2")).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listUsers;
        }
        public static BusinessObject.Models.User GetUserByID(string id)
        {
            User user = new();

            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                user = context.Users.FirstOrDefault(c => c.UserId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return user;
        }
        public static void Register(User user)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                context.Users.Add(user);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateUser(UserDto userDto)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                var user = new User
                {
                    UserId = userDto.UserId,
                    UserName = userDto.UserName,
                    Email = userDto.Email,
                    Password = userDto.Password,
                    Phone = userDto.Phone,
                    BirthDate = userDto.BirthDate,
                    RoleId = userDto.RoleId,
                };
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void DeleteUser(User user)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                var check_user = context.Users.SingleOrDefault(c => c.UserId.Equals(user.UserId));
                context.Users.Remove(check_user); ;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static IList<User> SearchByName(string name)
        {
            var list = new List<User>();

            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();

                if (name != null)
                {
                    list = context.Users.Where(c => c.UserName.Contains(name)).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return list;
        }
    }
}
