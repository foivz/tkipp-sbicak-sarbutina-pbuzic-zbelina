using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IRadnikRepository
    {
        Task<Radnik> DohvatiRadnikaAsync(Radnik entity);
        IQueryable<Radnik> DohvatiSveRadnike();
        int Update(Radnik entity, bool saveChanges = true);
    }
}
