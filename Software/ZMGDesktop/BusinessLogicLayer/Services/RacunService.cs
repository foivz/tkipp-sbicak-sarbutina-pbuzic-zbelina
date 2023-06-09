using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class RacunService
    {
        private readonly IRacunRepository racunRepository;

        public RacunService(IRacunRepository racunRepository)
        {
            this.racunRepository = racunRepository;
        }
        public List<Racun> DohvatiRacuneZaKlijenta(Klijent klijent)
        {
            //using (var repo = new RacunRepository())
            //{
            List<Racun> racuni = racunRepository.DohvatiRacuneZaKlijenta(klijent).ToList();
                return racuni;
            //}
        }

        public List<Racun> DohvatiSveRacune()
        {
            //using (var repo = new RacunRepository())
            //{
            List<Racun> racuni = racunRepository.DohvatiSveRacune().ToList();
                return racuni;
            //}
        }

        public int DodajRacun(Racun racun)
        {
            //using (var repo = new RacunRepository())
            //{
            racunRepository.Add(racun);
            //}
            return 1;
        }

        public Racun DohvatiOdredeniRacun(int id)
        {
            List<Racun> listaRacuna = new List<Racun>();
            //using (var repo = new RacunRepository())
            //{
            listaRacuna = racunRepository.DohvatiOdredeniRacun(id).ToList();
            //}
            return listaRacuna[0];
        }

        public Racun DohvatiZadnjiRacun()
        {
            List<Racun> listaRacuna = new List<Racun>();
            //using (var repo = new RacunRepository())
            //{
            listaRacuna = racunRepository.DohvatiSveRacune().ToList();
            //}
            return listaRacuna.Last();
        }

        public List<Racun> DohvatiRacunePretrazivanje(Klijent klijent, int id, PretrazivanjeSortiranje SearchSort)
        {
            //using (var repo = new RacunRepository())
            //{
            List<Racun> racuni = racunRepository.DohvatiPremaPretrazivanju(klijent, id, SearchSort).ToList();
                return racuni;
                //}
            }
        }
}
