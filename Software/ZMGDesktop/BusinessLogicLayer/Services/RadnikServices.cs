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
        private readonly IRadnikRepository radnikRepository;

        public RadnikServices(IRadnikRepository radnikRepository) {
            this.radnikRepository = radnikRepository;
        }
        public async Task<Radnik> ProvjeriRadnikaAsync(string korime, string lozinka) {
            //using (var repo = new RadnikRepository()) {
                var radnik = await radnikRepository.DohvatiRadnikaAsync(korime, lozinka);
                return radnik;
                
            //}
        }
        /*public Radnik ProvjeriRadnikaAsync(string korime, string lozinka) {
            //using (var repo = new RadnikRepository()) {
                var radnik =  radnikRepository.DohvatiRadnikaAsync(korime, lozinka); // await
                return radnik;

            //}
        }*/

        public List<Radnik> DohvatiSveRadnike()
        {
            using (var repo = new RadnikRepository()) {
            
            var radnici = repo.DohvatiSveRadnike().ToList();
            return radnici;
            }
        }
    }
}
