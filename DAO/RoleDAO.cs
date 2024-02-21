using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoleDAO
    {
        public static List<Role> GetRoles()
        {
            var listRole = new List<Role>();

            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                listRole = context.Roles.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listRole;
        }

    }
}
