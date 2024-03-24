using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ServiceDAO
    {
        public static IList<Service> GetServices()
        {
            var list = new List<Service>();

            try
            {
                using (var context = new BirthdayPartyBookingForKids_DBContext())
                {
                    list = context.Services.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return list;
        }

        public static Service GetServiceById(string id)
        {
            Service sv = new();

            try
            {
                using (var context = new BirthdayPartyBookingForKids_DBContext())
                {
                    sv = context.Services.SingleOrDefault(c => c.ServiceId.Equals(id));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return sv;
        }

        public static void AddService(Service service)
        {
            try
            {
                using (var context = new BirthdayPartyBookingForKids_DBContext())
                {
                    context.Services.Add(service);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateService(Service service)
        {
            try
            {
                using (var context = new BirthdayPartyBookingForKids_DBContext())
                {
                    context.Entry(service).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteService(Service service)
        {
            try
            {
                using (var context = new BirthdayPartyBookingForKids_DBContext())
                {
                    var checkDecoration = context.Services.SingleOrDefault(c => c.ServiceId.Equals(service.ServiceId));
                    context.Services.Remove(checkDecoration);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static IList<Service> SearchByName(string name)
        {
            var list = new List<Service>();

            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();

                if (name != null)
                {
                    list = context.Services.Where(c => c.ServiceName.Contains(name)).ToList();
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
