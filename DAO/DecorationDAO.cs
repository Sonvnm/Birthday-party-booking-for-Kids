using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DecorationDAO
    {
        public static List<Decoration> GetDecorations()
        {
            var list = new List<Decoration>();

            try
            {
                using (var context = new BirthdayPartyBookingForKids_DBContext())
                {
                    list = context.Decorations.ToList();
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

            return list;
        }

        public static Decoration GetDecorationById(int id)
        {
            Decoration decoration = new();

            try
            {
                using (var context = new BirthdayPartyBookingForKids_DBContext())
                {
                    decoration = context.Decorations.SingleOrDefault(c => c.ItemId.Equals(id)); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return decoration;
        }

        public static void AddDecoration(Decoration decoration)
        {
            try
            {
                using (var context = new BirthdayPartyBookingForKids_DBContext())
                {
                    context.Decorations.Add(decoration);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateDecoration(Decoration decoration)
        {
            try
            {
                using (var context = new BirthdayPartyBookingForKids_DBContext())
                {
                    context.Entry(decoration).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteDecoration(Decoration decoration)
        {
            try
            {
                using (var context = new BirthdayPartyBookingForKids_DBContext())
                {
                    var checkDecoration = context.Decorations.SingleOrDefault(c => c.ItemId.Equals(decoration.ItemId));
                    context.Decorations.Remove(checkDecoration);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static IList<Decoration> SearchByName(string name)
        {
            var list = new List<Decoration>();

            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();

                if (name != null)
                {
                    list = context.Decorations.Where(c => c.ItemName.Contains(name)).ToList();
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
