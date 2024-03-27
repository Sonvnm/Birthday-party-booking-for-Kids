using BusinessObject.Models;
using DataAccess.DTO;
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
        void Update(DecorationDto decorationDto);
        IList<Decoration> GetAllDecorations();
        Decoration GetDecorationById(string id);
        IList<Decoration> GetDecorationsByName(string name);
    }
}
