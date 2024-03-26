using BusinessObject.Models;
using DataAccess.DTO;
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
        Menu GetMenuById(string menuId);
        void DeleteMenu(Menu menu);
        void UpdateMenu(MenuDto menuDto);
        List<Menu> GetAllMenus();
    }
}
