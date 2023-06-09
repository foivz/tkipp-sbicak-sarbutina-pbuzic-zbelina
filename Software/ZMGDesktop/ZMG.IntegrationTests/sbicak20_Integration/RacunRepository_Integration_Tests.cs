using BusinessLogicLayer.Services;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
        public void Add_PostojiUslugaSIDjem133_DodanRacunUBazuPodataka()
        {
            //arrange
            kreirajServis();

            Klijent klijent = _klijentServices.DohvatiKlijente().FirstOrDefault(k => k.Naziv == "Sebastijan Bicak");
            Poslodavac poslodavac = PoslodavacServices.GetPoslodavac();
            Radnik radnik = RadnikServices.DohvatiSveRadnike().FirstOrDefault(r => r.Korime == "sbicak20");
            Roba roba = new Roba { Roba_ID = 181 };
            Usluga usluga = new Usluga { Usluga_ID = 133 };
            StavkaRacun stavka = new StavkaRacun
            {
                KolikoRobePoJedinici = 123,
                KolicinaRobe = 123,
                DatumIzrade = DateTime.Now,
                JedinicaMjere = "kg",
                JedinicnaCijena = 123,
                UkupnaCijenaStavke = 123
            };
            stavka = StavkaRacunService.InitStavka(stavka, roba, usluga);
            List<StavkaRacun> listaStavki = new List<StavkaRacun>
            {
                stavka
            };

            Racun racun = new Racun
            {

                Klijent = klijent,
                Klijent_ID = klijent.Klijent_ID,
                Poslodavac = poslodavac,
                Radnik = radnik,
                Fakturirao = "Sebastijan Bicak",
                Opis = "Automatsko testno izdanje",
                NacinPlacanja = "Testing",
                UkupnaCijena = 123,
                PDV = 123,
                UkupnoStavke = 123,
                DatumIzdavanja = DateTime.Now,
                StavkaRacun = listaStavki,
                RokPlacanja = "Nije navedeno"
            };
            //act
            int rezultat = RacunService.DodajRacun(racun);
            //assert
            Assert.Equal(1, rezultat);
        }

        [Fact]
        public void Add_PostojiUslugaSIDjem136_DodanRacunUBazuPodataka()
        {
            //arrange
            kreirajServis();

            Klijent klijent = _klijentServices.DohvatiKlijente().FirstOrDefault(k => k.Naziv == "Sebastijan Bicak");
            Poslodavac poslodavac = PoslodavacServices.GetPoslodavac();
            Radnik radnik = RadnikServices.DohvatiSveRadnike().FirstOrDefault(r => r.Korime == "sbicak20");
            Roba roba = new Roba { Roba_ID = 181 };
            Usluga usluga = new Usluga { Usluga_ID = 136 };
            StavkaRacun stavka = new StavkaRacun
            {
                KolikoRobePoJedinici = 123,
                KolicinaRobe = 123,
                DatumIzrade = DateTime.Now,
                JedinicaMjere = "kg",
                JedinicnaCijena = 123,
                UkupnaCijenaStavke = 123
            };
            stavka = StavkaRacunService.InitStavka(stavka, roba, usluga);
            List<StavkaRacun> listaStavki = new List<StavkaRacun>
            {
                stavka
            };

            Racun racun = new Racun
            {

                Klijent = klijent,
                Klijent_ID = klijent.Klijent_ID,
                Poslodavac = poslodavac,
                Radnik = radnik,
                Fakturirao = "Sebastijan Bicak",
                Opis = "Automatsko testno izdanje",
                NacinPlacanja = "Testing",
                UkupnaCijena = 123,
                PDV = 123,
                UkupnoStavke = 123,
                DatumIzdavanja = DateTime.Now,
                StavkaRacun = listaStavki,
                RokPlacanja = "Nije navedeno"
            };
            //act
            int rezultat = RacunService.DodajRacun(racun);
            //assert
            Assert.Equal(1, rezultat);
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
        public void DohvatiPremaPretrazivanju_SwitchCase0NemaKriterija_SortiranjeJeUzlazno()
        {
            //arrange
            kreirajServis();
            PretrazivanjeSortiranje searchSort = new PretrazivanjeSortiranje(); // case 0, pretrazivanje = 0, sortiranje = 0
            Klijent klijent = _klijentServices.DohvatiKlijente().FirstOrDefault(k => k.Naziv == "Aggreko");
            Radnik radnik = RadnikServices.DohvatiSveRadnike().FirstOrDefault(r => r.Korime == "sbicak20");
            //act
            List<Racun> listaRacunaPremaPretrazivanju = RacunService.DohvatiRacunePretrazivanje(klijent, radnik.Radnik_ID, searchSort);
            //assert
            Assert.NotEmpty(listaRacunaPremaPretrazivanju);
            Assert.Equal(158, listaRacunaPremaPretrazivanju[0].Racun_ID);
            Assert.Equal(159, listaRacunaPremaPretrazivanju[1].Racun_ID);
        }

        [Fact]
        public void DohvatiPremaPretrazivanju_SwitchCase0NemaKriterija_SortiranjeJeSilazno()
        {
            //arrange
            kreirajServis();
            PretrazivanjeSortiranje searchSort = new PretrazivanjeSortiranje(); // case 0, pretrazivanje = 0, sortiranje = 1
            searchSort.PostaviSortiranje(1);
            Klijent klijent = _klijentServices.DohvatiKlijente().FirstOrDefault(k => k.Naziv == "Aggreko");
            Radnik radnik = RadnikServices.DohvatiSveRadnike().FirstOrDefault(r => r.Korime == "sbicak20");
            //act
            List<Racun> listaRacunaPremaPretrazivanju = RacunService.DohvatiRacunePretrazivanje(klijent, radnik.Radnik_ID, searchSort);
            //assert
            Assert.NotEmpty(listaRacunaPremaPretrazivanju);
            Assert.Equal(2061, listaRacunaPremaPretrazivanju[0].Racun_ID);
            Assert.Equal(2060, listaRacunaPremaPretrazivanju[1].Racun_ID);
        }

        [Fact]
        public void DohvatiPremaPretrazivanju_SwitchCase1KriterijDatum_SortiranjeJeUzlazno()
        {
            //arrange
            kreirajServis();
            PretrazivanjeSortiranje searchSort = new PretrazivanjeSortiranje(); // case 1, pretrazivanje = 1, sortiranje = 0
            searchSort.PostaviPretrazivanje(1);
            searchSort.PostaviSortiranje(0);
            Klijent klijent = _klijentServices.DohvatiKlijente().FirstOrDefault(k => k.Naziv == "Aggreko");
            Radnik radnik = RadnikServices.DohvatiSveRadnike().FirstOrDefault(r => r.Korime == "sbicak20");
            //act
            List<Racun> listaRacunaPremaPretrazivanju = RacunService.DohvatiRacunePretrazivanje(klijent, radnik.Radnik_ID, searchSort);
            //assert
            Assert.NotEmpty(listaRacunaPremaPretrazivanju);
            Assert.Equal("1/30/2023 11:31:49 AM", listaRacunaPremaPretrazivanju[0].DatumIzdavanja.ToString());
            Assert.Equal("1/30/2023 11:32:17 AM", listaRacunaPremaPretrazivanju[1].DatumIzdavanja.ToString());
        }

        [Fact]
        public void DohvatiPremaPretrazivanju_SwitchCase1KriterijDatum_SortiranjeJeSilazno()
        {
            //arrange
            kreirajServis();
            PretrazivanjeSortiranje searchSort = new PretrazivanjeSortiranje(); // case 1, pretrazivanje = 1, sortiranje = 1  
            searchSort.PostaviPretrazivanje(1);
            searchSort.PostaviSortiranje(1);
            Klijent klijent = _klijentServices.DohvatiKlijente().FirstOrDefault(k => k.Naziv == "Aggreko");
            Radnik radnik = RadnikServices.DohvatiSveRadnike().FirstOrDefault(r => r.Korime == "sbicak20");
            //act
            List<Racun> listaRacunaPremaPretrazivanju = RacunService.DohvatiRacunePretrazivanje(klijent, radnik.Radnik_ID, searchSort);
            //assert
            Assert.NotEmpty(listaRacunaPremaPretrazivanju);
            Assert.Equal("6/8/2023 5:39:01 PM", listaRacunaPremaPretrazivanju[0].DatumIzdavanja.ToString());
            Assert.Equal("6/8/2023 5:24:41 PM", listaRacunaPremaPretrazivanju[1].DatumIzdavanja.ToString());
        }

        [Fact]
        public void DohvatiPremaPretrazivanju_SwitchCase2KriterijCijena_SortiranjeJeUzlazno()
        {
            //arrange
            kreirajServis();
            PretrazivanjeSortiranje searchSort = new PretrazivanjeSortiranje(); // case 2, pretrazivanje = 2, sortiranje = 0
            searchSort.PostaviPretrazivanje(2);
            searchSort.PostaviSortiranje(0);
            Klijent klijent = _klijentServices.DohvatiKlijente().FirstOrDefault(k => k.Naziv == "Aggreko");
            Radnik radnik = RadnikServices.DohvatiSveRadnike().FirstOrDefault(r => r.Korime == "sbicak20");
            //act
            List<Racun> listaRacunaPremaPretrazivanju = RacunService.DohvatiRacunePretrazivanje(klijent, radnik.Radnik_ID, searchSort);
            //assert
            Assert.NotEmpty(listaRacunaPremaPretrazivanju);
            Assert.Equal(28.75, listaRacunaPremaPretrazivanju[0].UkupnaCijena);
            Assert.Equal(49.5, listaRacunaPremaPretrazivanju[1].UkupnaCijena);
        }

        [Fact]
        public void DohvatiPremaPretrazivanju_SwitchCase2KriterijCijena_SortiranjeJeSilazno()
        {
            //arrange
            kreirajServis();
            PretrazivanjeSortiranje searchSort = new PretrazivanjeSortiranje(); // case 2, pretrazivanje = 2, sortiranje = 1
            searchSort.PostaviPretrazivanje(2);
            searchSort.PostaviSortiranje(1);
            Klijent klijent = _klijentServices.DohvatiKlijente().FirstOrDefault(k => k.Naziv == "Aggreko");
            Radnik radnik = RadnikServices.DohvatiSveRadnike().FirstOrDefault(r => r.Korime == "sbicak20");
            //act
            List<Racun> listaRacunaPremaPretrazivanju = RacunService.DohvatiRacunePretrazivanje(klijent, radnik.Radnik_ID, searchSort);
            //assert
            Assert.NotEmpty(listaRacunaPremaPretrazivanju);
            Assert.Equal(18911.25, listaRacunaPremaPretrazivanju[0].UkupnaCijena);
            Assert.Equal(18911.25, listaRacunaPremaPretrazivanju[1].UkupnaCijena);
        }

        [Fact]
        public void DohvatiPremaPretrazivanju_SwitchCase3RacuniRadnikaKriterij_SortiranjeJeUzlazno()
        {
            //arrange
            kreirajServis();
            PretrazivanjeSortiranje searchSort = new PretrazivanjeSortiranje(); // case 3, pretrazivanje = 3, sortiranje = 0
            searchSort.PostaviPretrazivanje(3);
            searchSort.PostaviSortiranje(0);
            Klijent klijent = _klijentServices.DohvatiKlijente().FirstOrDefault(k => k.Naziv == "Aggreko");
            Radnik radnik = RadnikServices.DohvatiSveRadnike().FirstOrDefault(r => r.Korime == "sarbutina20");
            //act
            List<Racun> listaRacunaPremaPretrazivanju = RacunService.DohvatiRacunePretrazivanje(klijent, radnik.Radnik_ID, searchSort);
            //assert
            Assert.NotEmpty(listaRacunaPremaPretrazivanju);
            Assert.Equal(1060, listaRacunaPremaPretrazivanju[0].Racun_ID);
            Assert.Equal(1061, listaRacunaPremaPretrazivanju[1].Racun_ID);
        }

        [Fact]
        public void DohvatiPremaPretrazivanju_SwitchCase3RacuniRadnikaKriterij_SortiranjeJeSilazno()
        {
            //arrange
            kreirajServis();
            PretrazivanjeSortiranje searchSort = new PretrazivanjeSortiranje(); // case 3, pretrazivanje = 3, sortiranje = 1
            searchSort.PostaviPretrazivanje(3);
            searchSort.PostaviSortiranje(1);
            Klijent klijent = _klijentServices.DohvatiKlijente().FirstOrDefault(k => k.Naziv == "Aggreko");
            Radnik radnik = RadnikServices.DohvatiSveRadnike().FirstOrDefault(r => r.Korime == "sarbutina20");
            //act
            List<Racun> listaRacunaPremaPretrazivanju = RacunService.DohvatiRacunePretrazivanje(klijent, radnik.Radnik_ID, searchSort);
            //assert
            Assert.NotEmpty(listaRacunaPremaPretrazivanju);
            Assert.Equal(1061, listaRacunaPremaPretrazivanju[0].Racun_ID);
            Assert.Equal(1060, listaRacunaPremaPretrazivanju[1].Racun_ID);
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
