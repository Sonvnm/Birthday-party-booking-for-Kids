using BusinessObject;
using BusinessObject.Models;
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
                    listUsers = context.Users.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listUsers;
        }
        public static BusinessObject.Models.User GetUserByID(int id)
        {
            var list = new List<BusinessObject.Models.User>();
            BusinessObject.Models.User user = new();

            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                user = list.SingleOrDefault(c => c.UserId.Equals(id));
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
        public static void UpdateUser(User user)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
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
