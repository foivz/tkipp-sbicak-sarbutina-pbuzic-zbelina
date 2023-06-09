using BusinessLogicLayer.PDF;
using BusinessLogicLayer.Services;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ZMG.IntegrationTests.sbicak20_Integration
{
    public class GeneriranjePDF_Integration_Tests
    {
        private KlijentServices _klijentServices;
        private RadnikServices RadnikServices;
        private RobaService RobaService;
        private UslugaServices UslugaServices;
        private PoslodavacServices PoslodavacServices;
        private RacunService RacunService;
        private StavkaRacunService StavkaRacunService;

        private void kreirajServis()
        {
            _klijentServices = new KlijentServices(new KlijentRepository());
            RadnikServices = new RadnikServices(new RadnikRepository());
            RobaService = new RobaService(new RobaRepository());
            UslugaServices = new UslugaServices(new UslugaRepository());
            PoslodavacServices = new PoslodavacServices(new PoslodavacRepository());
            RacunService = new RacunService(new RacunRepository());
            StavkaRacunService = new StavkaRacunService(new StavkaRepository());
        }
        [Fact]
        public void SacuvajPDF_ProsljedenRacunIJednaStavkaUListi_GeneriraniPDF()
        {
            //arrage
            kreirajServis();

            var radnik = RadnikServices.DohvatiSveRadnike().FirstOrDefault(r => r.Radnik_ID == 24);
            var poslodavac = PoslodavacServices.GetPoslodavac();
            var klijent = _klijentServices.DohvatiKlijente().FirstOrDefault(k => k.Klijent_ID == 150);

            List<StavkaRacun> lista = new List<StavkaRacun>
            {
                new StavkaRacun
                {
                    KolikoRobePoJedinici = 123,
                    KolicinaRobe = 123,
                    DatumIzrade = DateTime.Now,
                    JedinicaMjere = "kg",
                    JedinicnaCijena = 123,
                    UkupnaCijenaStavke = 123,
                    Usluga = UslugaServices.DohvatiUsluguPoNazivu("Cincanje"),
                    Roba = RobaService.DohvatiSvuRobu().FirstOrDefault()
                }
            };
            Racun racun = new Racun
            {
                Klijent = klijent,
                Poslodavac = poslodavac,
                Radnik = radnik,
                Fakturirao = "asddasf",
                Opis = "asddasf",
                NacinPlacanja = "asddasf",
                UkupnaCijena = 1.2,
                PDV = 1.2,
                UkupnoStavke = 3.4,
                DatumIzdavanja = DateTime.Now,
                StavkaRacun = lista,
                RokPlacanja = "asddasf"
            };


            //act
            int rezultat = GeneriranjePDF.SacuvajPDF(racun, lista);

            //assert
            Assert.Equal(1, rezultat);
        }

        [Fact]
        public void OtvoriPDF_VecJeStvorenPDF_GeneriraniPDF()
        {
            //arrage
            kreirajServis();
            var racun = RacunService.DohvatiSveRacune().FirstOrDefault();
            var stavkaList = StavkaRacunService.DohvatiStavkeRacuna(racun.Racun_ID);
            GeneriranjePDF.SacuvajPDF(racun, stavkaList);

            //act
            int rezultat = GeneriranjePDF.OtvoriPDF();

            //assert
            Assert.Equal(1, rezultat);
        }
    }
}
