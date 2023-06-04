using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IKlijentRepository
    {
        IQueryable<Klijent> GetAll();

        IQueryable<Klijent> DohvatiDesetNajboljih();

        int Add(Klijent entity, bool saveChanges = true);

        int Update(Klijent entity, bool saveChanges = true);

        int Remove(Klijent entitiy, bool saveChanges = true);

        bool provjeri(Klijent entity);

        void vecPostoji(Klijent entity, Klijent klijent);
    }
}
