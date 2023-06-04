using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IRacunRepository
    {
        int Add(Racun entity, bool saveChanges = true);
        IQueryable<Racun> DohvatiRacuneZaKlijenta(Klijent entity);
        IQueryable<Racun> DohvatiSveRacune();
        IQueryable<Racun> DohvatiOdredeniRacun(int id);
        IQueryable<Racun> DohvatiPremaPretrazivanju(Klijent entity, int Radnik_ID, int pretrazivanje = 0, int _sortiranje = 0);
        int Update(Racun entity, bool saveChanges = true);


    }
}
