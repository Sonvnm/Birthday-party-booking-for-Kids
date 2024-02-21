using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoties.IRepository
{
    public interface IDecorationRepository
    {
        void Add(Decoration decoration);
        void Delete(Decoration decoration);
        void Update(Decoration decoration);
        IList<Decoration> GetAllDecorations();
        Decoration GetDecorationById(int id);
        IList<Decoration> GetDecorationsByName(string name);
    }
}
