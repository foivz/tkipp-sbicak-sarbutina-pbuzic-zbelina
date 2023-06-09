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
        Task<Radnik> DohvatiRadnikaAsync(string korime, string lozinka);
        //Radnik DohvatiRadnikaAsync(string korime, string lozinka);
        IQueryable<Radnik> DohvatiSveRadnike();
        int Update(Radnik entity, bool saveChanges = true);
    }
}
