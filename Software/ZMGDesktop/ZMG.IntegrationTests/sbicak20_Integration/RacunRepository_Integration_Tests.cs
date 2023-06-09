using BusinessLogicLayer.Services;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ZMG.IntegrationTests.sbicak20_Integration
{
    public class RacunRepository_Integration_Tests
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
        public void DohvatiSveRacune()
        {
            //arrange

            //act

            //assert
        }

        [Fact]
        public void DodajRacun()
        {
            //arrange

            //act

            //assert
        }

        [Fact]
        public void DohvatiOdredeniRacun()
        {
            //arrange

            //act

            //assert
        }

        [Fact]
        public void DohvatiZadnjiRacun()
        {
            //arrange

            //act

            //assert
        }

        [Fact]
        public void DohvatiRacunePretrazivanje()
        {
            //arrange

            //act

            //assert
        }
    }
}
