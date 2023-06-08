using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class StavkaRacunService
    {

        private readonly IStavkaRepository stavkaRepository;

        public StavkaRacunService(IStavkaRepository _stavkaRepository)
        {
            this.stavkaRepository = _stavkaRepository;
        }
        public List<StavkaRacun> DohvatiStavkeRacuna(int id)
        {
            using (var repo = new StavkaRepository())
            {
                List<StavkaRacun> lista = stavkaRepository.DohvatiStavkeZaRacun(id).ToList();
                return lista;
            }
        }

        public StavkaRacun InitStavka(StavkaRacun _stavka, Roba roba, Usluga usluga)
        {
            using (var repo = new StavkaRepository())
            {
                StavkaRacun stavka = stavkaRepository.InitStavka(_stavka, roba, usluga);
                return stavka;
            }
        }

        public bool ProvjeriDuplikat(List<StavkaRacun> listaStavki, StavkaRacun stavka)
        {
            bool provjera = false;
            foreach(var s in listaStavki)
            {
                if (s.Roba == stavka.Roba || s.Roba_ID == stavka.Roba_ID)
                {
                    provjera = true;
                    break;
                }
            }
            return provjera;
        }
    }
}
