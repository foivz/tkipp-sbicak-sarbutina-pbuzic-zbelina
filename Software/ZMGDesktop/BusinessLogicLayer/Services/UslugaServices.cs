using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Services
{
    public class UslugaServices
    {

        private readonly IUslugaRepository uslugaRepository;

        public UslugaServices(IUslugaRepository _uslugaRepository)
        {
            this.uslugaRepository = _uslugaRepository;
        }

        public UslugaServices()
        {
            
        }

        public List<Usluga> DohvatiUsluge()
        {
            using (var repo = new UslugaRepository())
            {
                var usluge = uslugaRepository.GetAll().Distinct().ToList();
                return usluge;
            }
        }

        // kod za popravak dupliciranja
        public List<string> DohvatiUslugeDistinct()
        {
            using (var repo = new UslugaRepository())
            {
                var usluge = uslugaRepository.DohvatiUslugeDistinct().Distinct().ToList();
                return usluge;
            }
        }

        public Usluga DohvatiUsluguPoNazivu(string selectedValue)
        {
            using (var repo = new UslugaRepository())
            {
                var usluga = uslugaRepository.DohvatiUsluguPoNazivu(selectedValue);
                var vracenaUsluga = usluga.FirstOrDefault(u => u.Naziv == selectedValue);
                return vracenaUsluga;
            }
        }
    }
}
