using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class RadniNalogService
    {
        private readonly IRadniNalogRepository radniNalogRepository;

        public RadniNalogService(IRadniNalogRepository radniNalogRepository)
        {
            this.radniNalogRepository = radniNalogRepository;
        }
        public List<RadniNalog> DohvatiRadneNalogeZaKlijenta(Klijent klijent)
        {
            //using(var repo = new RadniNalogRepository())
            //{
            List<RadniNalog> radniNalozi = radniNalogRepository.DohvatiRadneNalogeZaKlijenta(klijent).ToList();
                return radniNalozi;
            //}
        }

        public List<RadniNalog> DohvatiRadneNaloge()
        {
            //using(var repo = new RadniNalogRepository())
            //{
            List<RadniNalog> radniNalozi = radniNalogRepository.DohvatiSveRadneNaloge().ToList();
                return radniNalozi;
            //}
        }

        public List<RadniNalog> DohvatiRadneNalogePoStatusima()
        {
            //using (var repo = new RadniNalogRepository())
            //{
            List<RadniNalog> radniNalozi = radniNalogRepository.DohvatiRadneNalogePoStatusima().ToList();
                return radniNalozi;
            //}
        }

        public bool DodajRadniNalog(RadniNalog radniNalog)
        {
            bool uspjesno = false;

            //using (var repo = new RadniNalogRepository())
            //{
            int red = radniNalogRepository.Add(radniNalog);
                uspjesno = red > 0;
            //}

            return uspjesno;
        }

        public bool ObrisiRadniNalog(RadniNalog radniNalog)
        {
            bool uspjesno = false;

            using (var repo = new RadniNalogRepository())
            {
                int red = radniNalogRepository.Remove(radniNalog);
                uspjesno = red > 0;
            }

            return uspjesno;
        }

        public bool AzurirajRadniNalog(RadniNalog radniNalog)
        {
            bool uspjesno = false;
            //using (var repo = new RadniNalogRepository())
            //{
            int red = radniNalogRepository.Update(radniNalog);
            uspjesno = red > 0;
            //}

            return uspjesno;
        }
    }
}
