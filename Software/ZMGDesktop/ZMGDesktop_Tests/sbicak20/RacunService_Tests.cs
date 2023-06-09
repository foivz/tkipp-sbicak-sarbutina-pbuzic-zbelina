using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FakeItEasy;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using BusinessLogicLayer.Services;

namespace ZMGDesktop_Tests.sbicak20
{
    public class RacunService_Tests
    {
        [Fact]
        public void DohvatiSveRacune_TrebaDohvatitiSveRacune_DohvaceniSuSviRacuni()
        {
            //arrange
            var fakeRepo = A.Fake<IRacunRepository>();
            List<Racun> fakeListaRacuna = new List<Racun>
            {
                new Racun {Racun_ID = 1, Fakturirao = "Sebastijan Bicak"},
                new Racun {Racun_ID = 2, Fakturirao = "Sebastijan Bicak"},
            };
            var racunServis = new RacunService(fakeRepo);
            A.CallTo(() => fakeRepo.DohvatiSveRacune()).Returns(fakeListaRacuna.AsQueryable());

            //act
            List<Racun> listaRacuna = racunServis.DohvatiSveRacune();
            //assert
            Assert.NotNull(listaRacuna);
            Assert.IsType<List<Racun>>(listaRacuna);
            Assert.Equal(1, listaRacuna[0].Racun_ID);
            Assert.Equal(2, listaRacuna[1].Racun_ID);
            Assert.Equal("Sebastijan Bicak", listaRacuna[0].Fakturirao);
            Assert.Equal("Sebastijan Bicak", listaRacuna[1].Fakturirao);
        }

        [Fact]
        public void DodajRacun_RacunSeDodajeUBazu_RacunJeDodanUBazu()
        {
            //arrange
            var fakeRepo = A.Fake<IRacunRepository>();
            var fakeRacun = new Racun()
            {
                Racun_ID = 1,
                Klijent = new Klijent { Naziv = "Test Testic" }
            };

            var racunService = new RacunService(fakeRepo);
            A.CallTo(() => fakeRepo.Add(fakeRacun, true)).Returns(1);
            //act
            int rezultat = racunService.DodajRacun(fakeRacun);

            //assert
            Assert.Equal(1, rezultat);
        }

        [Fact]
        public void DohvatiOdredeniRacun_ProsljedenJeBroj1ZaDohvacanjeRacuna_DohvacenJeRacunSIDjemJedan()
        {
            //arrange
            var fakeRepo = A.Fake<IRacunRepository>();
            var fakeRacun = new List<Racun>()
            {
                new Racun{
                    Racun_ID = 1,
                    Klijent = new Klijent { Naziv = "Test Testic" }
                }
            };

            var racunService = new RacunService(fakeRepo);
            A.CallTo(() => fakeRepo.DohvatiOdredeniRacun(1)).Returns(fakeRacun.AsQueryable());
            //act
            Racun racun = racunService.DohvatiOdredeniRacun(1);

            //assert
            Assert.Equal(1, racun.Racun_ID);
            Assert.IsType<Racun>(racun);
        }

        [Fact]
        public void DohvatiZadnjiRacun_PostojiNekolikoRacunaUBazi_DohvacenZadnjiRacun()
        {
            //arrange
            var fakeRepo = A.Fake<IRacunRepository>();
            var fakeRacun = new List<Racun>()
            {
                new Racun{
                    Racun_ID = 1,
                    Klijent = new Klijent { Naziv = "Test Testic" }
                },
                new Racun{
                    Racun_ID = 2,
                    Klijent = new Klijent { Naziv = "Petar Petrovic" }
                }
            };

            var racunService = new RacunService(fakeRepo);
            A.CallTo(() => fakeRepo.DohvatiSveRacune()).Returns(fakeRacun.AsQueryable());
            //act
            Racun racun = racunService.DohvatiZadnjiRacun();

            //assert
            Assert.Equal(2, racun.Racun_ID);
            Assert.IsType<Racun>(racun);
        }
        [Fact]
        public void DohvatiRacunePretrazivanje_InicijalniPrikazRacuna_PrikazaniInicijalniRacuni()
        {
            //arrange
            var fakeRepo = A.Fake<IRacunRepository>();
            var fakeListaRacuna = new List<Racun>
            {
                new Racun{
                    Racun_ID = 1,
                    Radnik = new Radnik{Ime = "Sebastijan", Prezime = "Bicak", Radnik_ID = 1},
                    Radnik_ID = 1,
                    Klijent = new Klijent { Naziv = "Test Testic" },
                    UkupnaCijena = 125,
                    UkupnoStavke = 100
                },
                new Racun{
                    Racun_ID = 2,
                    Radnik = new Radnik{Ime = "Sebastijan", Prezime = "Bicak", Radnik_ID = 1},
                    Radnik_ID = 1,
                    Klijent = new Klijent { Naziv = "Test Testic" },
                    UkupnaCijena = 225,
                    UkupnoStavke = 200,
                },
                new Racun
                {
                    Racun_ID = 3,
                    Radnik = new Radnik{Ime = "Sebastijan", Prezime = "Bicak", Radnik_ID = 1},
                    Radnik_ID = 1,
                    Klijent = new Klijent { Naziv = "Test Testic" },
                    UkupnaCijena = 325,
                    UkupnoStavke = 300
                }
            };
            var fakeKlijent = new Klijent
            {
                Naziv = "Test Testic"
            };
            var fakeSearchSort = new PretrazivanjeSortiranje();

            var racunService = new RacunService(fakeRepo);
            A.CallTo(() => fakeRepo.DohvatiPremaPretrazivanju(fakeKlijent, 1, fakeSearchSort)).Returns(fakeListaRacuna.AsQueryable());
            //act
            List<Racun> listaRacuna = racunService.DohvatiRacunePretrazivanje(fakeKlijent, 1, fakeSearchSort);
            //assert
            Assert.NotEmpty(listaRacuna);
            Assert.IsType<List<Racun>>(listaRacuna);
            Assert.Equal(1, listaRacuna[0].Racun_ID);
            Assert.Equal(3, listaRacuna[2].Racun_ID);
        }
    }
}
