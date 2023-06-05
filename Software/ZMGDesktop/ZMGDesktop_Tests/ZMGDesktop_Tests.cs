using BusinessLogicLayer.Services;
using DataAccessLayer.Iznimke;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;
using ZMGDesktop;

namespace ZMGDesktop_Tests
{
    public class ZMGDesktop_Tests
    {
        [Fact]
        public void UcitajKlijente_KlijentiPostojeuBazi_KlijentiSePrikazujuUSustavu()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            var ocekivaniKlijenti = new List<Klijent> { new Klijent { Naziv = "Klijent 1" }, new Klijent { Naziv = "Klijent 2" } };
            A.CallTo(() => fakeRepo.GetAll()).Returns(ocekivaniKlijenti.AsQueryable());

            var fakeServis = new KlijentServices(fakeRepo);

            var klijenti = fakeServis.DohvatiKlijente();

            Assert.NotNull(ocekivaniKlijenti);
            Assert.Equal(klijenti, ocekivaniKlijenti);
        }

        [Fact]
        public void Remove_KlijentImaRacun_BacaGresku()
        {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var servis = new KlijentServices(fakeRepo);
            var racun = new Racun { Fakturirao = "Netko" };
            var klijent = new Klijent { Naziv = "Klijent 1" };
            racun.Klijent = klijent;
            klijent.Racun.Add(racun);
            A.CallTo(() => fakeRepo.Remove(klijent, true)).Throws(() => new BrisanjeKlijentaException("Klijent ima račune."));

            //Act
            Action act = () => servis.Remove(klijent);

            //Assert
            Assert.Throws<BrisanjeKlijentaException>(() => act());
        }

        [Fact]
        public void Remove_KlijentNemaRacunRadniNalogIliRobu_KlijentUspjesnoObrisan()
        {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var servis = new KlijentServices(fakeRepo);
            var klijent = new Klijent { Naziv = "Klijent 1" };
            A.CallTo(() => fakeRepo.Remove(klijent, true)).Returns(1);

            //Act
            bool uspjesno = servis.Remove(klijent);

            //Assert
            Assert.True(uspjesno);
            A.CallTo(() => fakeRepo.Remove(klijent, true)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Remove_KlijentImaRadniNalog_BacaGresku()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = new Klijent { Naziv = "Ime" };
            var radniNalog = new RadniNalog { Opis = "Opis" };
            klijent.RadniNalog.Add(radniNalog);
            A.CallTo(() => fakeRepo.Remove(klijent, true)).Throws(new BrisanjeKlijentaException("Istina"));
            var fakeServis = new KlijentServices(fakeRepo);
            //
            Action act = () => fakeRepo.Remove(klijent);

            //Assert
            Assert.Throws<BrisanjeKlijentaException>(() => act());
        }

        [Fact]
        public void Add_IspunjeniSviPodaciZaKlijenta_KlijentUspjesnoDodan()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = new Klijent
            {
                Naziv = "Klijent",
                OIB = "21694361376",
                Adresa = "Lobor 35",
                IBAN = "HR6324020061696361472",
                BrojTelefona = "0976401323",
                Email = "email@gmail.com",
                Mjesto = "Lobor",
            };
            A.CallTo(() => fakeRepo.Add(klijent, true)).Returns(1);
            var servis = new KlijentServices(fakeRepo);

            //Act
            bool ubacen = servis.Add(klijent);

            //Assert
            Assert.True(ubacen);
            A.CallTo(() => fakeRepo.Add(klijent, true)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Add_KlijentSPostojecimImenom_BacaGresku()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = new Klijent
            {
                Naziv = "Aggreko",
                OIB = "21694361376",
                Adresa = "Lobor 35",
                IBAN = "HR6324020061696361472",
                BrojTelefona = "0976401323",
                Email = "email@gmail.com",
                Mjesto = "Lobor"
            };
            var servis = new KlijentServices(fakeRepo);

            A.CallTo(() => fakeRepo.Add(klijent, true)).Throws(new UserException("Postoji korisnik s ovim imenom"));

            //Act
            Action act = () => servis.Add(klijent);

            //Assert
            Assert.Throws<UserException>(() => act());
        }

        [Fact]
        public void Add_KlijentSPostojecimOibom_BacaGresku()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = new Klijent
            {
                Naziv = "Klijent",
                OIB = "21852758212",
                Adresa = "Lobor 35",
                IBAN = "HR6324020061696361472",
                BrojTelefona = "0976401323",
                Email = "email@gmail.com",
                Mjesto = "Lobor"
            };
            var servis = new KlijentServices(fakeRepo);

            A.CallTo(() => fakeRepo.Add(klijent, true)).Throws(new OIBException("Postoji korisnik s ovim OIBOM"));

            //Act
            Action act = () => servis.Add(klijent);

            //Assert
            Assert.Throws<OIBException>(() => act());
        }

        [Fact]
        public void Add_KlijentSPostojecimEmailom_BacaGresku()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = new Klijent
            {
                Naziv = "Klijent",
                OIB = "28152726125",
                Adresa = "Lobor 35",
                IBAN = "HR2817528125291258271",
                BrojTelefona = "0976401323",
                Email = "email@gmail.com",
                Mjesto = "Lobor"
            };
            var servis = new KlijentServices(fakeRepo);

            A.CallTo(() => fakeRepo.Add(klijent, true)).Throws(new EmailException("Postoji korisnik s ovim mailom"));

            //Act
            Action act = () => servis.Add(klijent);

            //Assert
            Assert.Throws<EmailException>(() => act());
        }

        [Fact]
        public void Add_KlijentSPostojecimTelefonom_BacaGresku()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = new Klijent
            {
                Naziv = "Klijent",
                OIB = "85721527125",
                Adresa = "Lobor 35",
                IBAN = "HR2817528915291258271",
                BrojTelefona = "0913955196",
                Email = "email@gmail.com",
                Mjesto = "Lobor"
            };
            var servis = new KlijentServices(fakeRepo);

            A.CallTo(() => fakeRepo.Add(klijent, true)).Throws(new TelefonException("Postoji korisnik s ovim brojem telefona"));

            //Act
            Action act = () => servis.Add(klijent);

            //Assert
            Assert.Throws<TelefonException>(() => act());
        }

        [Fact]
        public void Add_KlijentSPostojecimIBANRacunom_BacaGresku()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = new Klijent
            {
                Naziv = "Belina",
                OIB = "0962124281",
                Adresa = "Lobor 21",
                IBAN = "HR2817521258682125921",
                BrojTelefona = "0992712642",
                Email = "email@gmail.com",
                Mjesto = "Lobor"
            };
            var servis = new KlijentServices(fakeRepo);

            A.CallTo(() => fakeRepo.Add(klijent, true)).Throws(new IBANException("Postoji korisnik s ovim brojem IBAN računom."));

            //Act
            Action act = () => servis.Add(klijent);

            //Assert
            Assert.Throws<IB>(() => act());
        }

        [Fact]
        public void DohvatiDesetNajboljih_PostojeDesetNajvecihKlijenata_KaoRezultatVracaDesetNajvecihKlijenata()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            List<Klijent> desetNajvecih = new List<Klijent>()
            {
                new Klijent {Naziv="Klijent1"},
                new Klijent {Naziv="Klijent2"},
                new Klijent {Naziv="Klijent3"},
                new Klijent {Naziv="Klijent4"},
                new Klijent {Naziv="Klijent5"},
                new Klijent {Naziv="Klijent6"},
                new Klijent {Naziv="Klijent7"},
                new Klijent {Naziv="Klijent8"},
                new Klijent {Naziv="Klijent9"},
                new Klijent {Naziv="Klijent10"},
            };
            var fakeServis = new KlijentServices(fakeRepo);
            A.CallTo(() => fakeRepo.DohvatiDesetNajboljih()).Returns(desetNajvecih.AsQueryable());

            //Act
            var desetNajboljih = fakeServis.DohvatiDesetNajboljih();

            //Assert
            Assert.Equal(desetNajvecih, desetNajboljih);
        }

        [Fact]
        public void DohvatiDesetNajboljihKlijenata_NemaDovoljnoPodatkaUBaziZaDesetNajvecihKlijenata_BacaGresku()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            A.CallTo(() => fakeRepo.DohvatiDesetNajboljih()).Throws(new Exception("Nema dovoljno podatka za prikaz deset najvecih klijenata"));
            var fakeServis = new KlijentServices(fakeRepo);

            //Assert
            Action act = () => fakeServis.DohvatiDesetNajboljih();

            //Assert
            Assert.Throws<Exception>(() => act());
        }

        [Fact]
        public void AzurirajKlijenta_PostaviNazivNaPostojeciUBazi_VratiGreskuDaVecPostojiKlijentSTakvimNazivomUBazi()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = new Klijent
            {
                Naziv = "Klijent",
                OIB = "21694361376",
                Adresa = "Lobor 35",
                IBAN = "HR6324020061696361472",
                BrojTelefona = "0976401323",
                Email = "email@gmail.com",
                Mjesto = "Lobor"
            };
            A.CallTo(() => fakeRepo.Update(klijent, true)).Throws(new UserException("Postoji vec klijent s ovim nazivom"));
            var fakeServis = new KlijentServices(fakeRepo);

            //Act
            Action act = () => fakeServis.Update(klijent);

            //Assert
            Assert.Throws<UserException>(() => act());
        }

        [Fact]
        public void AzurirajKlijenta_PromijeniOdgovarajuceSvojstvo_UspjesnoAzuriranKlijent()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = new Klijent
            {
                Naziv = "Karlosag",
                OIB = "82812572912",
                Adresa = "Lobor 35",
                IBAN = "HR6322120061696361472",
                BrojTelefona = "0916401323",
                Email = "email@gmail.com",
                Mjesto = "Lobor"
            };
            A.CallTo(() => fakeRepo.Update(klijent, true)).Returns(1);
            var fakeServis = new KlijentServices(fakeRepo);

            //Act
            bool uspjesno = fakeServis.Update(klijent);

            //Assert
            Assert.True(uspjesno);
        }

        [Fact]
        public void DohvatiRacuneZaKlijenta_PostojeViseRacunaZaKlijenta_VracaPopisRacunaVezanihZaKlijenta()
        {
            //Arrange
            var fakeRepo = A.Fake<IRacunRepository>();
            var klijent = new Klijent
            {
                Naziv = "Preis",
                OIB = "82812572912",
                Adresa = "Lobor 35",
                IBAN = "HR6322120061696361472",
                BrojTelefona = "0916401323",
                Email = "email@gmail.com",
                Mjesto = "Lobor"
            };
            var racuni = new List<Racun>()
            {
                new Racun { Fakturirao = "Ivan", Opis = "Odlicno", UkupnaCijena = 20000 },
                new Racun { Fakturirao = "Karlo", Opis = "Ne bas najbolje", UkupnaCijena = 10000 }
            };
            klijent.Racun = racuni;
            A.CallTo(() => fakeRepo.DohvatiRacuneZaKlijenta(klijent)).Returns(racuni.AsQueryable());
            var fakeServis = new RacunService(fakeRepo);

            //Act
            var racuniKlijenta = fakeServis.DohvatiRacuneZaKlijenta(klijent);

            //Assert
            Assert.Equal(racuni, racuniKlijenta);
        }

        [Fact]
        public void DohvatiRadneNalogeZaKlijenta_PostojeRadniNaloziZaKlijenta_VracaPopisRadnihNalogaVezanihZaKlijenta()
        {
            var fakeRepo = A.Fake<IRadniNalogRepository>();
            var klijent = new Klijent
            {
                Naziv = "Preis",
                OIB = "82812572912",
                Adresa = "Lobor 35",
                IBAN = "HR6322120061696361472",
                BrojTelefona = "0916401323",
                Email = "email@gmail.com",
                Mjesto = "Lobor"
            };
            var radniNalozi = new List<RadniNalog>()
            {
                new RadniNalog { Opis = "Sve uredu", Kolicina = 20 },
                new RadniNalog { Opis = "Previse materijala", Kolicina = 50 }
            };
            klijent.RadniNalog = radniNalozi;
            A.CallTo(() => fakeRepo.DohvatiRadneNalogeZaKlijenta(klijent)).Returns(radniNalozi.AsQueryable());
            var fakeServis = new RadniNalogService(fakeRepo);

            //Act
            var klijentRadniNalozi = fakeServis.DohvatiRadneNalogeZaKlijenta(klijent);

            //Assert
            Assert.Equal(klijentRadniNalozi, radniNalozi);
        }

    }
}
