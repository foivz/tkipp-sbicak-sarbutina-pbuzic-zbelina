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
    }
}
