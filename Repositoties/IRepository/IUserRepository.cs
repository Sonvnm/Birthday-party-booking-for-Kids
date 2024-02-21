﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoties.IRepository
{
    public interface IUserRepository
    {
        User Login(string username,string password);
        void Register(User user);
        void Delete(User user);
        void Update(User user);
        IList<User> GetAllUser();
        User GetUserById(int id);
        IList<User> GetUserByName(string username);
    }
}