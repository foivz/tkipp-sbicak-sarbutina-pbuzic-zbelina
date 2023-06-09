using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class RadnikServices
    {
        private readonly IRadnikRepository _radnikRepository;

        public RadnikServices(IRadnikRepository radnikRepository) {
            this._radnikRepository = radnikRepository;
        }
        public RadnikServices()
        {
            
        }
        public async Task<Radnik> ProvjeriRadnikaAsync(string korime, string lozinka) {
            using (var repo = new RadnikRepository()) {
                var radnik = await repo.DohvatiRadnikaAsync(korime, lozinka);
                return radnik;
                //}
            }
        }
        public List<Radnik> DohvatiSveRadnike()
        {
            using (var repo = new RadnikRepository()) {
            
            var radnici = repo.DohvatiSveRadnike().ToList();
            return radnici;
            }
        }
    }
}
