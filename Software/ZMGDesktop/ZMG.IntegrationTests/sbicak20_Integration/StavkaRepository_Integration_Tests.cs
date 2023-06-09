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
    public class StavkaRepository_Integration_Tests
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
        public void Update_NijeImpelementiranaFunkcija_BacaSeGreska()
        {
            //arrange
            var stavka = new StavkaRacun();
            var stavkaRepo = new StavkaRepository();
            //act
            Action act = () => stavkaRepo.Update(stavka);
            //assert
            Assert.Throws<NotImplementedException>(() => act());
        }

        [Fact]
        public void InitStavka_ProsljedujeSeStavkaRobaIUsluga_StavkaJeInicijalizirana()
        {

            //arrange
            kreirajServis();
            var stavka = new StavkaRacun
            {
                KolikoRobePoJedinici = 123,
                KolicinaRobe = 123,
                DatumIzrade = DateTime.Now,
                JedinicaMjere = "kg",
                JedinicnaCijena = 123,
                UkupnaCijenaStavke = 123
            };
            var usluga = UslugaServices.DohvatiUsluguPoNazivu("Cincanje");
            var roba = new Roba
            {
                Roba_ID = 181
            };

            //act
            var novaStavka = StavkaRacunService.InitStavka(stavka, roba, usluga);

            //assert
            Assert.NotNull(novaStavka);
            Assert.Equal(123, novaStavka.JedinicnaCijena);
            Assert.Equal("Cincanje", novaStavka.Usluga.Naziv);
            Assert.Equal("Šipka e2", novaStavka.Roba.Naziv);
        }

        [Fact]
        public void DohvatiStavkeRacuna_DohvacajuSeStavkePremaIDRacunu_SveStavkeSuDohvacene()
        {
            //arrange
            kreirajServis();
            //act
            var listaStavkiRacuna = StavkaRacunService.DohvatiStavkeRacuna(158);
            //assert
            Assert.NotEmpty(listaStavkiRacuna);
            Assert.Equal(1, listaStavkiRacuna.Count);
            Assert.Equal(150, listaStavkiRacuna[0].KolicinaRobe);
        }
    }
}
