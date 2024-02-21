using BusinessObject.Models;
using DataAccess;
using Repositoties.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoties.Repository
{
    public class UserRepository : IUserRepository
    {
        public void Delete(User user)
            => UserDAO.DeleteUser(user);
        public IList<User> GetAllUser()
            => UserDAO.GetAllUser();

        public User GetUserById(int id)
        => UserDAO.GetUserByID(id);

        public IList<User> GetUserByName(string username)
        => UserDAO.SearchByName(username);

        public User Login(string username, string password)
        => UserDAO.Login(username, password);

        public void Register(User user)
        => UserDAO.Register(user);

        public void Update(User user)
        => UserDAO.UpdateUser(user);
    }
}
