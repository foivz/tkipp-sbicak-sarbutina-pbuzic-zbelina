using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IStavkaRepository
    {
        StavkaRacun InitStavka(StavkaRacun entity, Roba _roba, Usluga _usluga);
        IQueryable<StavkaRacun> DohvatiStavkeZaRacun(int id);
        int Update(StavkaRacun entity, bool saveChanges = true);
    }
}
