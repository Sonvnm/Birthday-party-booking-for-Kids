using BusinessObject.Models;
using DataAccess;
using DataAccess.DTO;
using Repositoties.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoties.Repository
{
    public class MenuRepository : IMenuRepository
    {
        public void DeleteMenu(Menu menu) => MenuDao.DeleteMenu(menu);

        public List<Menu> GetAllMenus() => MenuDao.GetAllMenus();

        public Menu GetMenuById(string menuId) => MenuDao.GetMenuByID(menuId);

        public void SaveMenu(Menu menu) => MenuDao.SaveMenu(menu);

        public void UpdateMenu(MenuDto menuDto) => MenuDao.UpdateMenu(menuDto);
    }
}
