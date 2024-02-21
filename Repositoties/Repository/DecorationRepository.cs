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
    public class DecorationRepository : IDecorationRepository
    {
        public void Add(Decoration decoration) => DecorationDAO.AddDecoration(decoration);

        public void Delete(Decoration decoration) => DecorationDAO.DeleteDecoration(decoration);

        public IList<Decoration> GetAllDecorations() => DecorationDAO.GetDecorations();

        public Decoration GetDecorationById(int id) => DecorationDAO.GetDecorationById(id);

        public IList<Decoration> GetDecorationsByName(string name) => DecorationDAO.SearchByName(name);

        public void Update(Decoration decoration) => DecorationDAO.UpdateDecoration(decoration);
    }
}
