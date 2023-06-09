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
        public void Add()
        {
            //arrange


            //act

            //assert
        }

        [Fact]
        public void DohvatiRacuneZaKlijenta()
        {
            //arrange
            kreirajServis();
            Klijent klijent = _klijentServices.DohvatiKlijente().FirstOrDefault(k => k.Naziv == "Aggreko");
            //act
            List<Racun> listaSvihRacuna = RacunService.DohvatiRacuneZaKlijenta(klijent);
            //assert
            Assert.NotEmpty(listaSvihRacuna);
            Assert.Equal(158, listaSvihRacuna.Find(r => r.Racun_ID == 158).Racun_ID);
            Assert.Equal(159, listaSvihRacuna.Find(r => r.Racun_ID == 159).Racun_ID);
        }

        [Fact]
        public void DohvatiSveRacune_PostojeRacuniUBazi_DohvaceniSuSviRacuniIzBaze()
        {
            //arrange
            kreirajServis();
            //act
            List<Racun> listaSvihRacuna = RacunService.DohvatiSveRacune();
            //assert
            Assert.NotEmpty(listaSvihRacuna);
            Assert.Equal(158, listaSvihRacuna.Find(r => r.Racun_ID == 158).Racun_ID);
        }

       

        [Fact]
        public void DohvatiOdredeniRacun_UBaziPostojiRacunSIDjem158_DohvacenJeOdredeniRacunSIDjem158()
        {
            //arrange
            kreirajServis();
            //act
            Racun racun = RacunService.DohvatiOdredeniRacun(158);
            //assert
            Assert.NotNull(racun);
            Assert.Equal(158, racun.Racun_ID);
            Assert.Equal("Sebastijan Bicak", racun.Fakturirao);
        }


        [Fact]
        public void DohvatiPremaPretrazivanju()
        {
            //arrange

            //act

            //assert
        }

        [Fact]
        public void Update_NijeImpelementiranaFunkcija_BacaSeGreska()
        {
            //arrange
            var racun = new Racun();
            var racunRepo = new RacunRepository();
            //act
            Action act = () => racunRepo.Update(racun);
            //assert
            Assert.Throws<NotImplementedException>(() => act());
        }
    }
}
