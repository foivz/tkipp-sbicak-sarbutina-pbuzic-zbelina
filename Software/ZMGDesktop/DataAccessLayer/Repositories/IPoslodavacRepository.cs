using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IPoslodavacRepository
    {
        int Update(Poslodavac entity, bool saveChanges = true);
        IQueryable<Poslodavac> DohvatiPoslodavca(int id);
    }
}
