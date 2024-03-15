using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDAO
    {
        public static User Login(string email, string password)
        {
            using var context = new BirthdayPartyBookingForKids_DBContext();
            return context.Users.SingleOrDefault(c => c.Email == email && c.Password == password);
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
        
        public static User GetUserByID(string id)
        {
            User user = new User();
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                if (id != null)
                {
                    user = context.Users.SingleOrDefault(c => id.Equals(c.UserId));
                }
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
