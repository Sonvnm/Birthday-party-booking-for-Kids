﻿using BusinessObject.Models;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MenuDao
    {
        public static List<Menu> GetAllMenus()
        {
            var listMenus = new List<Menu>();
            try
            {
                using (var context = new BirthdayPartyBookingForKids_DBContext())
                {
                    listMenus = context.Menus.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listMenus;
        }

        public static Menu GetMenuByID(string id)
        {
            var menu = new Menu();
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                menu = context.Menus.FirstOrDefault(x => x.FoodId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return menu;
        }
        public static void SaveMenu(Menu menu)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                context.Menus.Add(menu);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateMenu(MenuDto menuDto)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                var menu = new Menu
                {
                    FoodId = menuDto.FoodId,
                    FoodName = menuDto.FoodName,
                    Description = menuDto.Description,
                    Price = menuDto.Price,
                };
                context.Entry<Menu>(menu).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteMenu(Menu menu)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                var checkMenu = context.Menus.SingleOrDefault(p => p.FoodId == menu.FoodId);
                context.Menus.Remove(checkMenu);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
