using DataAccessLayer.Iznimke;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using PdfSharp.Charting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class KlijentServices
    {
        private readonly IKlijentRepository klijentRepository;

        public KlijentServices(IKlijentRepository klijentRepozitory) { 
        this.klijentRepository = klijentRepozitory;
        }

        public List<Klijent> DohvatiKlijente()
        {
            //  using (var repo = new KlijentRepository())
            // {
            List<Klijent> klijenti = klijentRepository.GetAll().ToList();
                return klijenti;
           // }
        }

        public List<Klijent> SortirajKlijentePoUkupnomBrojuRacuna()
        {
            List<Klijent> sortiraniKlijenti = klijentRepository.SortirajKlijentePoUkupnomBrojuRacuna().ToList();
            return sortiraniKlijenti;
        }

        public List<Klijent> DohvatiDesetNajboljih()
        {
            // using (var repo = new KlijentRepository())
            // {
            List<Klijent> desetNajboljih = klijentRepository.DohvatiDesetNajboljih().ToList();
                return desetNajboljih;
            // }
        }

        public bool Add(Klijent klijent)
        {
            bool uspjesno = false;

            //  using (var repo = new KlijentRepository())
            //  {
            int red = klijentRepository.Add(klijent);
                uspjesno = red > 0;
                //  }
                return uspjesno;
        }

        public bool Update(Klijent klijent)
        {
            bool uspjesno = false;

            // using(var repo = new KlijentRepository())
            // {
            int red = klijentRepository.Update(klijent);
                uspjesno = red > 0;
            //  }

            return uspjesno;
        }

        public bool Remove(Klijent klijent)
        {
            bool uspjesno = false;
            //  using (var repo = new KlijentRepository())
            //  {
            int red = klijentRepository.Remove(klijent);
                uspjesno = red > 0;
            //    }
            return uspjesno;
        }

        public List<Klijent> Pretrazi(string izraz)
        {
            var pretrazeniKlijenti =  klijentRepository.Pretrazi(izraz).ToList();
            return pretrazeniKlijenti;
        }
    }
}
