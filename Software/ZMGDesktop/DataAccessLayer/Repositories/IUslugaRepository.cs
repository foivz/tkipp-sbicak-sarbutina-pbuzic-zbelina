using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IUslugaRepository
    {
        int Update(Usluga entity, bool saveChanges = true);
        IQueryable<Usluga> GetAll();
        IQueryable<Usluga> DohvatiUsluguPoNazivu(string naziv);
        IQueryable<string> DohvatiUslugeDistinct();
    }
}
