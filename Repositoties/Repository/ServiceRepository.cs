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
    public class ServiceRepository : IServiceRepository
    {
        public void Add(Service service) => ServiceDAO.AddService(service);

        public void Delete(Service service) => ServiceDAO.DeleteService(service);

        public IList<Service> GetAllServices() => ServiceDAO.GetServices();

        public Service GetServiceById(string id) => ServiceDAO.GetServiceById(id);

        public IList<Service> GetServicesByName(string name) => ServiceDAO.SearchByName(name);

        public void Update(Service service) => ServiceDAO.UpdateService(service);
    }
}
