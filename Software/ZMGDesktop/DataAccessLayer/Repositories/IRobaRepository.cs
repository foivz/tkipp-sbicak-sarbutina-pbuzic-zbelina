using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IRobaRepository
    {
        IQueryable<Roba> DohvatiRobuKlijenta(int klijentID);
        IQueryable<Roba> DohvatiRobuPoNazivu(string naziv);
        IQueryable<string> DohvatiRobuKlijentaDistinct(int id);
        IQueryable<Roba> DohvatiSvuRobu();
        int Add(Roba entity, bool saveChanges = true);
        int Update(Roba entity, bool saveChanges = true);

    }
}
