using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IMaterijalRepository
    {

        IQueryable<Materijal> GetAll();
        bool ProvjeriQR(string qrKod);
        Materijal Azuriraj(string qrKod, int kolicina);
        int Add(Materijal entity, bool saveChanges = true);
        int Remove(Materijal entity, bool saveChanges = true);
        int Update(Materijal entity, bool saveChanges = true);
    }
}
