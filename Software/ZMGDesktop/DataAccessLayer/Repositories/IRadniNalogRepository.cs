using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IRadniNalogRepository
    {
        IQueryable<RadniNalog> DohvatiRadneNalogeZaKlijenta(Klijent entity);
        IQueryable<RadniNalog> DohvatiSveRadneNaloge();
        IQueryable<RadniNalog> DohvatiRadneNalogePoStatusima();
        int Update(RadniNalog entity, bool saveChanges = true);
        int Add(RadniNalog entity, bool saveChanges = true);
        int Remove(RadniNalog entity, bool saveChanges = true);

    }
}
