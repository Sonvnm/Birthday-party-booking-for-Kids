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
    public class RoleRepository : IRoleRepository
    {
        public IList<Role> GetRoles()
        => RoleDAO.GetRoles();
    }
}
