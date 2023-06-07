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

        public RadnikServices(IRadnikRepository _radnikRepository)
        {
            this.radnikRepository = _radnikRepository;
        }
        public async Task<Radnik> ProvjeriRadnikaAsync(Radnik prosljRadnik)
        {
            using (var repo = new RadnikRepository())
            {
                var radnik = await radnikRepository.DohvatiRadnikaAsync(prosljRadnik);
                return radnik;
            }
        }

        public List<Radnik> DohvatiSveRadnike()
        {
            using (var repo = new RadnikRepository())
            {
                var radnici = radnikRepository.DohvatiSveRadnike().ToList();
                return radnici;
            }
        }
    }
}
