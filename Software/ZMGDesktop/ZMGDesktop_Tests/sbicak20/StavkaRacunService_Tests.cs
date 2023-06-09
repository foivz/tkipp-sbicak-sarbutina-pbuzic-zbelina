using BusinessLogicLayer.LogikaZaRacune;
using BusinessLogicLayer.PDF;
using BusinessLogicLayer.Services;
using DataAccessLayer.Iznimke;
using DataAccessLayer.Repositories;
using Email;
using EntitiesLayer.Entities;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xunit;
using ZMGDesktop.ValidacijaUnosa;

namespace ZMGDesktop_Tests.sbicak20
{
    public class StavkaRacunService_Tests
    {
        [Fact]
        public void InitStavka_ProsljedujeSeStavkaRobaIUsluga_StavkaJeInicijalizirana()
        {
            //arrange
            var fakeRepo = A.Fake<IStavkaRepository>();
            var stavka = new StavkaRacun
            {
                JedinicnaCijena = 20
            };
            var roba = new Roba
            {
                Roba_ID = 1
            };
            var usluga = new Usluga
            {
                Usluga_ID = 1
            };
            var servis = new StavkaRacunService(fakeRepo);

            A.CallTo(() => fakeRepo.InitStavka(stavka, roba, usluga)).Returns(new StavkaRacun
            {
                Roba = roba,
                Usluga = usluga,
                JedinicnaCijena = stavka.JedinicnaCijena
            });
            //act
            var initStavka = servis.InitStavka(stavka, roba, usluga);

            //assert
            Assert.NotNull(initStavka);
            Assert.IsType<StavkaRacun>(initStavka);
            Assert.Equal(20, initStavka.JedinicnaCijena);
            Assert.Equal(1, initStavka.Roba.Roba_ID);
            Assert.Equal(1, initStavka.Usluga.Usluga_ID);
        }

        [Fact]
        public void DohvatiStavkeRacuna_DohvacajuSeStavkePremaIDRacunu_SveStavkeSuDohvacene()
        {
            //arrange
            var fakeRepo = A.Fake<IStavkaRepository>();
            var listaStavki = new List<StavkaRacun>
            {
                new StavkaRacun(){JedinicnaCijena = 10},
                new StavkaRacun(){JedinicnaCijena = 200}
            };
            var servis = new StavkaRacunService(fakeRepo);
            A.CallTo(() => fakeRepo.DohvatiStavkeZaRacun(4324)).Returns(listaStavki.AsQueryable());

            //act
            var dohvaceneStavke = servis.DohvatiStavkeRacuna(4324);

            //assert
            Assert.True(dohvaceneStavke.Count == 2);
            Assert.IsType < List<StavkaRacun> >(dohvaceneStavke);
            Assert.Equal(10, dohvaceneStavke[0].JedinicnaCijena);
            Assert.Equal(200, dohvaceneStavke[1].JedinicnaCijena);
        }

        [Fact]
        public void ProvjeriDuplikat_ProsljedenaJeListaIRazlicitaStavka_DuplikatPostoji()
        {
            //arrange
            List<StavkaRacun> lista = new List<StavkaRacun>
            {
                new StavkaRacun{ Roba = new Roba {Roba_ID = 1 }, Roba_ID = 1 },
                new StavkaRacun{ Roba = new Roba {Roba_ID = 2 }, Roba_ID = 2 }
            };

            StavkaRacun stavka = new StavkaRacun
            {
                Roba = new Roba { Roba_ID = 2 },
                Roba_ID = 2
            };

            var servis = new StavkaRacunService(new StavkaRepository());
            //act
            bool rezultat = servis.ProvjeriDuplikat(lista, stavka);
            //assert
            Assert.True(rezultat);
        }

        [Fact]
        public void ProvjeriDuplikat_ProsljedenaJeListaIRazlicitaStavka_DuplikatNePostoji()
        {
            List<StavkaRacun> lista = new List<StavkaRacun>
            {
                new StavkaRacun{ Roba = new Roba {Roba_ID = 1 }, Roba_ID = 1 },
                new StavkaRacun{ Roba = new Roba {Roba_ID = 2 }, Roba_ID = 2 }
            };

            StavkaRacun stavka = new StavkaRacun
            {
                Roba = new Roba { Roba_ID = 3 },
                Roba_ID = 3
            };

            var servis = new StavkaRacunService(new StavkaRepository());
            //act
            bool rezultat = servis.ProvjeriDuplikat(lista, stavka);
            //assert
            Assert.False(rezultat);
        }
    }
}
