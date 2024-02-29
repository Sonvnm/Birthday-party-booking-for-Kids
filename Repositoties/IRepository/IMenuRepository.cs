using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoties.IRepository
{
    public interface IMenuRepository
    {
        void SaveMenu(Menu menu);
        Menu GetMenuById(int menuId);
        void DeleteMenu(Menu menu);
        void UpdateMenu(Menu menu);
        List<Menu> GetAllMenus();
    }
}
