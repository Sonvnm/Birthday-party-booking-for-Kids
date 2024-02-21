using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoties.IRepository
{
    public interface IServiceRepository
    {
        void Add(Service service);
        void Delete(Service service);
        void Update(Service service);
        IList<Service> GetAllServices();
        Service GetServiceById(int id);
        IList<Service> GetServicesByName(string name);
    }
}
