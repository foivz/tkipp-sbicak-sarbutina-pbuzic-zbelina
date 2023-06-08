using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class RobaService
    {
        private readonly IRobaRepository robaRepository;
        public RobaService(IRobaRepository RobaRepository)
        {
            this.robaRepository = RobaRepository;
        }
        public List<Roba> DohvatiRobuKlijenta(int klijentID)
        {
            //using (var repo = new RobaRepository())
            //{
                List<Roba> roba = robaRepository.DohvatiRobuKlijenta(klijentID).ToList();
                return roba;
            //}
        }


        // metode za rjesenje dupliciranja
        public List<string> DohvatiRobuKlijentaDistinct(int id)
        {
            //using (var repo = new RobaRepository())
            //{
                var robe = robaRepository.DohvatiRobuKlijentaDistinct(id).Distinct().ToList();
                return robe;
            //}
        }

        public Roba DohvatiRobuPoNazivu(string selectedValue)
        {
            //using (var repo = new RobaRepository())
            //{
                var roba = robaRepository.DohvatiRobuPoNazivu(selectedValue);
                var vracenaRoba = roba.FirstOrDefault(u => u.Naziv == selectedValue);
                return vracenaRoba;
            //}
        }

        

        public bool Add(Roba roba)
        {
            bool uspjesno = false;

            //using (var repo = new RobaRepository())
            //{
                int red = robaRepository.Add(roba);
                uspjesno = red > 0;
            //}
            return uspjesno;
        }

        public bool Update(Roba roba)
        {
            bool uspjesno = false;

            //using (var repo = new RobaRepository())
            //{
                int red = robaRepository.Update(roba);
                uspjesno = red > 0;
            //}

            return uspjesno;
        }
    }
}
