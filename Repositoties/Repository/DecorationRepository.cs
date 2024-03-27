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
    public class DecorationRepository : IDecorationRepository
    {
        public void Add(Decoration decoration) => DecorationDAO.AddDecoration(decoration);

        public void Delete(Decoration decoration) => DecorationDAO.DeleteDecoration(decoration);

        public IList<Decoration> GetAllDecorations() => DecorationDAO.GetDecorations();

        public Decoration GetDecorationById(string id) => DecorationDAO.GetDecorationById(id);

        public IList<Decoration> GetDecorationsByName(string name) => DecorationDAO.SearchByName(name);

        public void Update(DecorationDto decorationDto) => DecorationDAO.UpdateDecoration(decorationDto);
    }
}
