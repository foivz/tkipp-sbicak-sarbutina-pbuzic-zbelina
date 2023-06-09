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
        public void InitStavka_ProsljedujeSeStavkaRobaIUsluga_StavkaJeInicijalizirana()
        {
            
            //arrange
           
            //act

            //assert
        
        }

        [Fact]
        public void DohvatiStavkeRacuna_DohvacajuSeStavkePremaIDRacunu_SveStavkeSuDohvacene()
        {
            //arrange
           
            //act

            //assert
        }

        [Fact]
        public void ProvjeriDuplikat_ProsljedenaJeListaIRazlicitaStavka_DuplikatPostoji()
        {
            //arrange

            //act

            //assert
        }

        [Fact]
        public void ProvjeriDuplikat_ProsljedenaJeListaIRazlicitaStavka_DuplikatNePostoji()
        {
            //arrange

            //act

            //assert
        }
    }
}
