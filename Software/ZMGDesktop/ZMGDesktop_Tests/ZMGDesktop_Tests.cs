﻿using BusinessLogicLayer.Services;
using DataAccessLayer.Iznimke;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using FakeItEasy;
using Microsoft.SqlServer.Server;
using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;
using ZMGDesktop;
using ZMGDesktop.ValidacijaUnosa;

namespace ZMGDesktop_Tests
{
    public class ZMGDesktop_Tests {
        [Fact]
        public void UcitajKlijente_KlijentiPostojeuBazi_KlijentiSePrikazujuUSustavu() {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var ocekivaniKlijenti = new List<Klijent> { new Klijent { Naziv = "Klijent 1" }, new Klijent { Naziv = "Klijent 2" } };
            A.CallTo(() => fakeRepo.GetAll()).Returns(ocekivaniKlijenti.AsQueryable());
            var fakeServis = new KlijentServices(fakeRepo);

            //Act
            var klijenti = fakeServis.DohvatiKlijente();

            //Assert
            Assert.NotNull(ocekivaniKlijenti);
            Assert.Equal(klijenti, ocekivaniKlijenti);
        }

        [Fact]
        public void Remove_KlijentImaRacun_BacaGresku() {
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
        public void Remove_KlijentNemaRacunRadniNalogIliRobu_KlijentUspjesnoObrisan() {
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
        public void Remove_KlijentImaRadniNalog_BacaGresku() {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = new Klijent { Naziv = "Ime" };
            var radniNalog = new RadniNalog { Opis = "Opis" };
            klijent.RadniNalog.Add(radniNalog);
            A.CallTo(() => fakeRepo.Remove(klijent, true)).Throws(new BrisanjeKlijentaException("Istina"));
            var fakeServis = new KlijentServices(fakeRepo);

            //Act
            Action act = () => fakeRepo.Remove(klijent);

            //Assert
            Assert.Throws<BrisanjeKlijentaException>(() => act());
        }

        private Klijent kreirajKlijenta(string naziv, string oib, string adresa, string iban, string brojTelefona, string email, string mjesto) {
            return new Klijent {
                Naziv = naziv,
                OIB = oib,
                Adresa = adresa,
                IBAN = iban,
                BrojTelefona = brojTelefona,
                Email = email,
                Mjesto = mjesto
            };
        }

        [Fact]
        public void Add_IspunjeniSviPodaciZaKlijenta_KlijentUspjesnoDodan() {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();

            var klijent = kreirajKlijenta("Klijent", "21694361376", "Lobor 35", "HR6324020061696361472", "0976401323", "email@gmail.com", "Lobor");
            A.CallTo(() => fakeRepo.Add(klijent, true)).Returns(1);
            var servis = new KlijentServices(fakeRepo);

            //Act
            bool ubacen = servis.Add(klijent);

            //Assert
            Assert.True(ubacen);
            A.CallTo(() => fakeRepo.Add(klijent, true)).MustHaveHappenedOnceExactly();
        }



        [Fact]
        public void Add_KlijentSPostojecimImenom_BacaGresku() {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = kreirajKlijenta("Aggreko", "21694361376", "Lobor 35", "HR6324020061696361472", "0976401323", "email@gmail.com", "Lobor");
            var servis = new KlijentServices(fakeRepo);

            A.CallTo(() => fakeRepo.Add(klijent, true)).Throws(new UserException("Postoji korisnik s ovim imenom"));

            //Act
            Action act = () => servis.Add(klijent);

            //Assert
            Assert.Throws<UserException>(() => act());
        }

        [Fact]
        public void Add_KlijentSPostojecimOibom_BacaGresku() {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = kreirajKlijenta("Klijent", "21852758212", "Lobor 35", "HR6324020061696361472", "0976401323", "email@gmail.com", "Lobor");

            var servis = new KlijentServices(fakeRepo);

            A.CallTo(() => fakeRepo.Add(klijent, true)).Throws(new OIBException("Postoji korisnik s ovim OIBOM"));

            //Act
            Action act = () => servis.Add(klijent);

            //Assert
            Assert.Throws<OIBException>(() => act());
        }

        [Fact]
        public void Add_KlijentSPostojecimEmailom_BacaGresku() {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = kreirajKlijenta("Klijent", "28152726125", "Lobor 35", "HR2817528125291258271", "0976401323", "email@gmail.com", "Lobor");

            var servis = new KlijentServices(fakeRepo);

            A.CallTo(() => fakeRepo.Add(klijent, true)).Throws(new EmailException("Postoji korisnik s ovim mailom"));

            //Act
            Action act = () => servis.Add(klijent);

            //Assert
            Assert.Throws<EmailException>(() => act());
        }

        [Fact]
        public void Add_KlijentSPostojecimTelefonom_BacaGresku() {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = kreirajKlijenta("Klijent", "85721527125", "Lobor 35", "HR2817528915291258271", "0913955196", "email@gmail.com", "Lobor");
            var servis = new KlijentServices(fakeRepo);

            A.CallTo(() => fakeRepo.Add(klijent, true)).Throws(new TelefonException("Postoji korisnik s ovim brojem telefona"));

            //Act
            Action act = () => servis.Add(klijent);

            //Assert
            Assert.Throws<TelefonException>(() => act());
        }

        [Fact]
        public void Add_KlijentSPostojecimIBANRacunom_BacaGresku() {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = kreirajKlijenta("Belina", "0962124281", "Lobor 21", "HR2817521258682125921", "0992712642", "email@gmail.com", "Lobor");
            var servis = new KlijentServices(fakeRepo);

            A.CallTo(() => fakeRepo.Add(klijent, true)).Throws(new IBANException("Postoji korisnik s ovim brojem IBAN računom."));

            //Act
            Action act = () => servis.Add(klijent);

            //Assert
            Assert.Throws<IBANException>(() => act());
        }

        [Fact]
        public void DohvatiDesetNajboljih_PostojeDesetNajvecihKlijenata_KaoRezultatVracaDesetNajvecihKlijenata() {
            //Arrange
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
        public void DohvatiDesetNajboljihKlijenata_NemaDovoljnoPodatkaUBaziZaDesetNajvecihKlijenata_BacaGresku() {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            A.CallTo(() => fakeRepo.DohvatiDesetNajboljih()).Throws(new Exception("Nema dovoljno podatka za prikaz deset najvecih klijenata"));
            var fakeServis = new KlijentServices(fakeRepo);

            //Act
            Action act = () => fakeServis.DohvatiDesetNajboljih();

            //Assert
            Assert.Throws<Exception>(() => act());
        }

        [Fact]
        public void AzurirajKlijenta_PostaviNazivNaPostojeciUBazi_VratiGreskuDaVecPostojiKlijentSTakvimNazivomUBazi() {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = kreirajKlijenta("Klijent", "21694361376", "Lobor 35", "HR6324020061696361472", "0976401323", "email@gmail.com", "Lobor");
            A.CallTo(() => fakeRepo.Update(klijent, true)).Throws(new UserException("Postoji vec klijent s ovim nazivom"));
            var fakeServis = new KlijentServices(fakeRepo);

            //Act
            Action act = () => fakeServis.Update(klijent);

            //Assert
            Assert.Throws<UserException>(() => act());
        }

        [Fact]
        public void AzurirajKlijenta_PromijeniOdgovarajuceSvojstvo_UspjesnoAzuriranKlijent() {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = kreirajKlijenta("Karlobag", "82812572912", "Lobor 35", "HR6322120061696361472", "0916401323", "email@gmail.com", "Lobor");
            A.CallTo(() => fakeRepo.Update(klijent, true)).Returns(1);
            var fakeServis = new KlijentServices(fakeRepo);

            //Act
            bool uspjesno = fakeServis.Update(klijent);

            //Assert
            Assert.True(uspjesno);
        }

        [Fact]
        public void DohvatiRacuneZaKlijenta_PostojeViseRacunaZaKlijenta_VracaPopisRacunaVezanihZaKlijenta() {
            //Arrange
            var fakeRepo = A.Fake<IRacunRepository>();
            var klijent = kreirajKlijenta("Preis", "82812572912", "Lobor 35", "HR6322120061696361472", "0916401323", "email@gmail.com", "Lobor");
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
        public void DohvatiRadneNalogeZaKlijenta_PostojeRadniNaloziZaKlijenta_VracaPopisRadnihNalogaVezanihZaKlijenta() {
            //Arrange
            var fakeRepo = A.Fake<IRadniNalogRepository>();
            var klijent = kreirajKlijenta("Preis", "82812572912", "Lobor 35", "HR6322120061696361472", "0916401323", "email@gmail.com", "Lobor");
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

        private Validacija kreirajValidaciju() {
            return new Validacija();
        }

        [Fact]

        public void ProvjeraOib_OIBNeispravan_VracaFalse() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraOIB("728129529182");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void ProvjeraOib_OIBIspravan_VracaTrue() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraOIB("27125827125");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]

        public void ProvjeraMaila_MailNeispravan_VracaFalse() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraMaila("nesto@122'.com");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void ProvjeraMaila_MailIspravan_VracaTrue() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraMaila("tvrtka@gmail.com");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]

        public void ProvjeraRacuna_RacunNeispravan_VracaFalse() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraRacuna("HR828192591257271927217");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void ProvjeraRacuna_RacunIspravan_VracaTrue() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraRacuna("HR4524020061552417385");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]

        public void provjeraSamoSlova_NeispravanNaziv_VracaFalse() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraSamoSlova("Ivan123");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void provjeraSamoSlova_IspravanNaziv_VracaTrue() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraSamoSlova("Ivan");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]

        public void provjeraMjesta_NeispravnoMjesto_VracaFalse() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraMjesta("Novi '.Vinodolski");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void provjeraMjesta_IspravnoMjesto_VracaTrue() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraMjesta("Novi Vinodolski");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]

        public void provjeraUlica_NeispravnaUlica_VracaFalse() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraUlica("Apple# Street");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void provjeraUlica_IspravnaUlica_VracaTrue() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraUlica("10th Avenue");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]

        public void provjeraSamoBrojevi_StringSadrziSlova_VracaFalse() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraSamoBrojevi("123cetiri");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void provjeraSamoBrojevi_StringSadrziSamoBrojeve_VracaTrue() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraSamoBrojevi("12456123123");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]

        public void provjeraTelefon_NeispravanBrojTelefona_VracaFalse() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraTelefon("092124281241");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void provjeraTelefon_IspravanBrojTelefona_VracaTrue() {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraTelefon("0992718252");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]
        public void Add_ProslijediKaoParametarKlijentaIFalse_Vraca0() {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = kreirajKlijenta("Preis", "82812572912", "Lobor 35", "HR6322120061696361472", "0916401323", "email@gmail.com", "Lobor");

            //Act
            int red = fakeRepo.Add(klijent, false);

            //Assert
            Assert.True(red == 0);
        }

        [Fact]
        public void Update_ProslijediKaoParametarKlijentaIFalse_Vraca0() {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = kreirajKlijenta("Preis", "82812572912", "Lobor 35", "HR6322120061696361472", "0916401323", "email@gmail.com", "Lobor");

            //Act
            int red = fakeRepo.Update(klijent, false);

            //Assert
            Assert.True(red == 0);
        }

        [Fact]
        public void Remove_ProslijediKaoParametarKlijentaIFalse_Vraca0() {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = kreirajKlijenta("Preis", "82812572912", "Lobor 35", "HR6322120061696361472", "0916401323", "email@gmail.com", "Lobor");

            //Act
            int red = fakeRepo.Remove(klijent, false);

            //Assert
            Assert.True(red == 0);
        }

        //TESTOVI SERVISA RADNIH NALOGA

        [Fact]
        public void DohvatiRadneNaloge_RadniNaloziPostojeUBazi_VracaRadneNaloge() {
            //Arrange
            var fakeRepo = A.Fake<IRadniNalogRepository>();
            var ocekivaniRadniNalozi = new List<RadniNalog>
            {
                new RadniNalog { Kolicina = 1},
                new RadniNalog { Kolicina = 2}
            };
            A.CallTo(() => fakeRepo.DohvatiSveRadneNaloge()).Returns(ocekivaniRadniNalozi.AsQueryable());

            var fakeServis = new RadniNalogService(fakeRepo);

            //Act
            var dobiveniRadniNalozi = fakeServis.DohvatiRadneNaloge();

            //Assert
            Assert.Equal(ocekivaniRadniNalozi, dobiveniRadniNalozi);
        }

        [Fact]
        public void DohvatiRadneNalogePoStatusima_RadniNaloziPostojeUBazi_VracaRadneNalogePoStatusima() {
            //Arrange
            var fakeRepo = A.Fake<IRadniNalogRepository>();
            var ocekivaniRadniNalozi = new List<RadniNalog>
            {
                new RadniNalog { Status = "Napravljen" },
                new RadniNalog { Status = "U obradi" },
                new RadniNalog { Status = "Dovršen" }
            };
            A.CallTo(() => fakeRepo.DohvatiRadneNalogePoStatusima()).Returns(ocekivaniRadniNalozi.AsQueryable());

            var fakeServis = new RadniNalogService(fakeRepo);

            //Act
            var dobiveniRadniNalozi = fakeServis.DohvatiRadneNalogePoStatusima();

            //Assert
            Assert.Equal(ocekivaniRadniNalozi, dobiveniRadniNalozi);
        }

        [Fact]
        public void DodajRadniNalog_UpisaniSviPodaciIspravno_RadniNalogUspjenoDodanUBazu() {
            //Arrange
            var fakeRepo = A.Fake<IRadniNalogRepository>();
            var fakeKlijent = new Klijent { Naziv = "FakeKlijent" };
            var fakeRadnik = new Radnik { Ime = "FakeRadnik" };
            var fakeMaterijal = new List<Materijal> { new Materijal { Naziv = "FakeMaterijal" } };
            var fakeRoba = new List<Roba> { new Roba { Naziv = "FakeRoba" } };
            var fakeRadniNalog = new RadniNalog {
                Kolicina = 15,
                Opis = "Treba poniklati robu klijenta",
                QR_kod = "NEK1QR123KOD12345678",
                DatumStvaranja = DateTime.Now,
                Status = "Napravljen",
                Radnik_ID = 2,
                Klijent_ID = 3,
                Klijent = fakeKlijent,
                Radnik = fakeRadnik,
                Materijal = fakeMaterijal,
                Roba = fakeRoba
            };

            A.CallTo(() => fakeRepo.Add(fakeRadniNalog, true)).Returns(1);

            var fakeServis = new RadniNalogService(fakeRepo);

            //Act
            var dodaniRadniNalog = fakeServis.DodajRadniNalog(fakeRadniNalog);

            //Assert
            Assert.True(dodaniRadniNalog);
        }

        [Fact]
        public void ObrisiRadniNalog_RadniNalogPostojiUBazi_RadniNalogUspjenoObrisan() {
            //Arrange
            var fakeRepo = A.Fake<IRadniNalogRepository>();
            var fakeKlijent = new Klijent { Naziv = "FakeKlijent" };
            var fakeRadnik = new Radnik { Ime = "FakeRadnik" };
            var fakeMaterijal = new List<Materijal> { new Materijal { Naziv = "FakeMaterijal" } };
            var fakeRoba = new List<Roba> { new Roba { Naziv = "FakeRoba" } };
            var fakeRadniNalog = new RadniNalog {
                Kolicina = 15,
                Opis = "Treba poniklati robu klijenta",
                QR_kod = "NEK1QR123KOD12345678",
                DatumStvaranja = DateTime.Now,
                Status = "Napravljen",
                Radnik_ID = 2,
                Klijent_ID = 3,
                Klijent = fakeKlijent,
                Radnik = fakeRadnik,
                Materijal = fakeMaterijal,
                Roba = fakeRoba
            };

            A.CallTo(() => fakeRepo.Remove(fakeRadniNalog, true)).Returns(1);

            var fakeServis = new RadniNalogService(fakeRepo);

            //Act
            var obrisaniRadniNalog = fakeServis.ObrisiRadniNalog(fakeRadniNalog);

            //Assert
            Assert.True(obrisaniRadniNalog);
        }

        [Fact]
        public void AzurirajRadniNalog_IzmijenjenaKolicinaRadnogNaloga_RadniNalogUspjenoIzmjenjen() {
            //Arrange
            var fakeRepo = A.Fake<IRadniNalogRepository>();
            var fakeKlijent = new Klijent { Naziv = "FakeKlijent" };
            var fakeRadnik = new Radnik { Ime = "FakeRadnik" };
            var fakeMaterijal = new List<Materijal> { new Materijal { Naziv = "FakeMaterijal" } };
            var fakeRoba = new List<Roba> { new Roba { Naziv = "FakeRoba" } };
            var fakeRadniNalog = new RadniNalog {
                Kolicina = 20,
                Opis = "Treba poniklati robu klijenta",
                QR_kod = "NEK1QR123KOD12345678",
                DatumStvaranja = DateTime.Now,
                Status = "Napravljen",
                Radnik_ID = 2,
                Klijent_ID = 3,
                Klijent = fakeKlijent,
                Radnik = fakeRadnik,
                Materijal = fakeMaterijal,
                Roba = fakeRoba
            };

            A.CallTo(() => fakeRepo.Update(fakeRadniNalog, true)).Returns(1);

            var fakeServis = new RadniNalogService(fakeRepo);

            //Act
            var azuriraniRadniNalog = fakeServis.AzurirajRadniNalog(fakeRadniNalog);

            //Assert
            Assert.True(azuriraniRadniNalog);
        }

        // TESOVI SERVISA ROBE

        [Fact]
        public void DohvatiRobuKlijenta_PostojiKlijentSRobom_VraćenaKlijentovaRoba() {
            //Arrange
            var fakeRepo = A.Fake<IRobaRepository>();
            var ocekivanaRoba = new List<Roba>{
                new Roba { Naziv = "fakeRoba" },
                new Roba { Naziv = "fakeRoba2" }
            };

            A.CallTo(() => fakeRepo.DohvatiRobuKlijenta(1)).Returns(ocekivanaRoba.AsQueryable());

            var fakeServis = new RobaService(fakeRepo);

            //Act
            var vracenaRoba = fakeServis.DohvatiRobuKlijenta(1);

            //Assert
            Assert.NotNull(vracenaRoba);
        }

        [Fact]
        public void DohvatiSvuRobu_PostojiRobaUBazi_VraćenaRoba() {
            //Arrange
            var fakeRepo = A.Fake<IRobaRepository>();
            var ocekivanaRoba = new List<Roba>{
                new Roba { Naziv = "fakeRoba" },
                new Roba { Naziv = "fakeRoba2" }
            };

            A.CallTo(() => fakeRepo.DohvatiSvuRobu());

            var fakeServis = new RobaService(fakeRepo);

            //Act
            var vracenaRoba = fakeServis.DohvatiSvuRobu();

            //Assert
            Assert.NotNull(vracenaRoba);
        }

        // DRUGI TESTOVI

        [Fact]
        public void Add_UpisaniSviPodaciIspravno_DodanaNovaRoba() {
            //Arrange
            var fakeRepo = A.Fake<IRobaRepository>();
            var fakeRoba = new Roba { Naziv = "fakeRoba" };

            A.CallTo(() => fakeRepo.Add(fakeRoba, true)).Returns(1);

            var fakeServis = new RobaService(fakeRepo);

            //Act
            var dodanaRoba = fakeServis.Add(fakeRoba);

            //Assert
            Assert.True(dodanaRoba);
        }


        // TESTOVI ZA SERVIS RADNIKA


        [Fact]
        public async Task ProvjeriRadnikaAsync_IspravniPodaci_VracaRadnika() {
            // Arrange
            var korime = "sarbutina20";
            var lozinka = "12345";
            var radnik = new Radnik {
                Korime = korime,
                Lozinka = lozinka
            };

            var fakeRepo = A.Fake<IRadnikRepository>();
            A.CallTo(() => fakeRepo.DohvatiRadnikaAsync(korime, lozinka)).Returns(Task.FromResult(radnik));

            // Act
            var fakeServis = new RadnikServices(fakeRepo);
            var provjereniRadnik = await fakeServis.ProvjeriRadnikaAsync(korime, lozinka);

            // Assert
            Assert.Equal(provjereniRadnik, radnik);

        }

        [Fact]
        public async Task ProvjeriRadnikaAsync_NeispravniPodaci_VracaNull() {
            // Arrange
            var korime = "sarbutina";
            var lozinka = "123";
            Radnik radnik = null;

            var fakeRepo = A.Fake<IRadnikRepository>();
            A.CallTo(() => fakeRepo.DohvatiRadnikaAsync(korime, lozinka)).Returns(Task.FromResult(radnik));

            // Act
            var fakeServis = new RadnikServices(fakeRepo);
            var provjereniRadnik = await fakeServis.ProvjeriRadnikaAsync(korime, lozinka);

            // Assert
            Assert.Null(provjereniRadnik);
        }

        [Fact]
        public void DohvatiSveRadnike_IspravnoPozvanaMetoda_VracaListuRadnika() {
            // Arrange
            IQueryable<Radnik> listaRadnika = new List<Radnik>() {
        new Radnik {Korime = "sarbutina20", Lozinka = "12345"},
        new Radnik {Korime = "sbicak20", Lozinka = "12345"}
    }.AsQueryable();

            

            var fakeRepo = A.Fake<IRadnikRepository>();
            A.CallTo(() => fakeRepo.DohvatiSveRadnike()).Returns(listaRadnika);

            // Act
            var fakeServis = new RadnikServices(fakeRepo);
            var rezultat = fakeServis.DohvatiSveRadnike();

            // Assert
            Assert.Equal(listaRadnika, rezultat);
        }

        // TESTOVI ZA SERVIS MATERIJALA
        [Fact]
        public void DohvatiMaterijale_IspravnoPozvanaMetoda_VracaListuMaterijala() {
            // Arrange
            IQueryable<Materijal> listaMaterijala = new List<Materijal>() {
        new Materijal {Naziv = "Celik"},
        new Materijal {Naziv = "Cink"}
    }.AsQueryable();


            
            var fakeRepo = A.Fake<IMaterijalRepository>();
            A.CallTo(() => fakeRepo.GetAll()).Returns(listaMaterijala);

            // Act
            var fakeServis = new MaterijalServices(fakeRepo);
            var rezultat = fakeServis.DohvatiMaterijale();

            // Assert
            Assert.Equal(listaMaterijala, rezultat);
        }

        [Fact]
        public void ProvjeriQR_IspravanQRKod_VracaTrue() {
            // Arrange
            bool postoji = true;
            string qr = "AR33EDGHUDDW2SVESA22RF";
            var fakeRepo = A.Fake<IMaterijalRepository>();
            A.CallTo(() => fakeRepo.ProvjeriQR(qr)).Returns(postoji);

            // Act
            var fakeServis = new MaterijalServices(fakeRepo);
            var rezultat = fakeServis.ProvjeriQR(qr);

            // Assert
            Assert.Equal(postoji, rezultat);
        }

        [Fact]
        public void ProvjeriQR_NeispravanQRKod_VracaFalse() {
            // Arrange
            bool postoji = false;
            string qr = "AR33EDGHUDDW2SVESA22RF";
            var fakeRepo = A.Fake<IMaterijalRepository>();
            A.CallTo(() => fakeRepo.ProvjeriQR(qr)).Returns(postoji);

            // Act
            var fakeServis = new MaterijalServices(fakeRepo);
            var rezultat = fakeServis.ProvjeriQR(qr);

            // Assert
            Assert.Equal(postoji, rezultat);
        }

        [Fact]
        public void AzurirajMaterijal_IspravanQRiKolicina_VracaMaterijal() {
            // Arrange
            Materijal materijal = new Materijal();
            string qr = "AR33EDGHUDDW2SVESA22RF";
            int kolicina = 10;
            var fakeRepo = A.Fake<IMaterijalRepository>();
            A.CallTo(() => fakeRepo.Azuriraj(qr, kolicina)).Returns(materijal);

            // Act
            var fakeServis = new MaterijalServices(fakeRepo);
            var rezultat = fakeServis.AzurirajMaterijal(qr, kolicina);

            // Assert
            Assert.Equal(materijal, rezultat);
        }

        [Fact]
        public void ObrisiMaterijal_IspravanMaterijal_VracaTrue() {
            // Arrange
            Materijal materijal = new Materijal();
            int uspjeh = 1;
            
            var fakeRepo = A.Fake<IMaterijalRepository>();
            A.CallTo(() => fakeRepo.Remove(materijal, true)).Returns(uspjeh);
            

            // Act
            var fakeServis = new MaterijalServices(fakeRepo);
            var rezultat = fakeServis.ObrisiMaterijal(materijal);
            var rezInt = Convert.ToInt32(rezultat);

            // Assert
            Assert.Equivalent(uspjeh, rezInt);
        }

        [Fact]
        public void DodajMaterijal_IspravanMaterijal_VracaTrue() {
            // Arrange
            Materijal materijal = new Materijal {
                Naziv = "Nepoznat materijal",
                Kolicina = 156,
                QR_kod= "AR33EDGHUDDW2SVESA22RF",
                Opis=" ",
                OpasnoPoZivot=false,
                CijenaMaterijala = 157.4,
                JedinicaMjere="kg"
            };
            int uspjeh = 1;

            var fakeRepo = A.Fake<IMaterijalRepository>();
            A.CallTo(() => fakeRepo.Add(materijal, true)).Returns(uspjeh);

            // Act
            var fakeServis = new MaterijalServices(fakeRepo);
            var rezultat = fakeServis.DodajMaterijal(materijal);
            var rezInt = Convert.ToInt32(rezultat);

            // Assert
            Assert.Equivalent(uspjeh, rezInt);
        }

        [Fact]
        public void DodajMaterijal_NeispravanMaterijal_VracaIznimku() {
            // Arrange
            Materijal materijal = new Materijal { Naziv = "Celik" };
            int uspjeh = 0;

            var fakeRepo = A.Fake<IMaterijalRepository>();

            A.CallTo(() => fakeRepo.Add(materijal, true)).Throws(new InvalidOperationException("Greška prilikom dodavanja materijala:"));

            // Act
            var fakeServis = new MaterijalServices(fakeRepo);

            // Assert
            Assert.Throws<InvalidOperationException>(() => fakeServis.DodajMaterijal(materijal));
        }

        // TESTOVI ZA SERVIS USLUGA
        [Fact]
        public void DohvatiUsluge_IspravnoPozvanaMetoda_VracaListuUsluga() {
            // Arrange
            IQueryable<Usluga> listaUsluga = new List<Usluga>() {
        new Usluga {Naziv = "Cincanje"},
        new Usluga {Naziv = "Niklanje"}
    }.AsQueryable();

            var fakeRepo = A.Fake<IUslugaRepository>();
            A.CallTo(() => fakeRepo.GetAll()).Returns(listaUsluga);

            // Act
            var fakeServis = new UslugaServices(fakeRepo);
            var rezultat = fakeServis.DohvatiUsluge();

            // Assert
            Assert.Equal(rezultat, listaUsluga);
        }

        [Fact]
        public void DohvatiUslugeDistinct_IspravnoPozvanaMetoda_VracaListuUsluga() {
            // Arrange
            IQueryable<string> listaUsluga = new List<string>() {
                "usluga1",
                "usluga2"
    }.AsQueryable();

            var fakeRepo = A.Fake<IUslugaRepository>();
            A.CallTo(() => fakeRepo.DohvatiUslugeDistinct()).Returns(listaUsluga);

            // Act
            var fakeServis = new UslugaServices(fakeRepo);
            var rezultat = fakeServis.DohvatiUslugeDistinct();

            // Assert
            Assert.Equal(rezultat, listaUsluga);
        }
        [Fact]
        public void DohvatiUsluguPoNazivu_IspravnoPozvanaMetoda_VracaListuUsluga() {
            // Arrange
            List<Usluga> usluge = new List<Usluga>() {
               new Usluga {Naziv="Celik"}
            };

            string naziv = "Celik";

            var fakeRepo = A.Fake<IUslugaRepository>();
            A.CallTo(() => fakeRepo.DohvatiUsluguPoNazivu(naziv)).Returns(usluge.AsQueryable());

            // Act
            var fakeServis = new UslugaServices(fakeRepo);
            var rezultat = fakeServis.DohvatiUsluguPoNazivu(naziv);

            // Assert
            Assert.Equal(rezultat, usluge.FirstOrDefault());
        }

        // TESTOVI ZA SERVIS PRIMKE

        [Fact]
        public void DodajPrimku_IspravnaPrimka_VracaTrue() {
            // Arrange
            Primka primka = new Primka {
                Naziv_Materijal = "Celik",
                Kolicina=146,
                Datum=DateTime.Now
            };
            int uspjeh = 1;

            var fakeRepo = A.Fake<IPrimkaRepository>();
            A.CallTo(() => fakeRepo.Add(primka, true)).Returns(uspjeh);

            // Act
            var fakeServis = new PrimkaServices(fakeRepo);
            var rezultat = fakeServis.DodajPrimku(primka);
            var rezInt = Convert.ToInt32(rezultat);

            // Assert
            Assert.Equivalent(uspjeh, rezInt);
        }

        // TESTOVI ZA NOVU FUNKCIONALNOST IZVOZA PODATAKA O MATERIJALIMA
        [Fact]
        public void IzvozMaterijala_NemaMaterijala_VracaPrazanString() {
            List<Materijal> lista = new List<Materijal>();
            
            var fakeRepo = A.Fake<IMaterijalRepository>();
            A.CallTo(() => fakeRepo.GetAll()).Returns(lista.AsQueryable());

            var fakeServis = new MaterijalServices(fakeRepo);

            // Act
            var rezultat = fakeServis.IzvozMaterijala();

                // Assert
           Assert.Empty(rezultat);
            

        }

        [Fact]
        public void IzvozMaterijala_IspravnaLista_VracaIspunjenString() {
            var materijal1 = new Materijal { Naziv = "Materijal 1", Kolicina = 10, CijenaMaterijala=46, JedinicaMjere= "kg", OpasnoPoZivot=false };
            var materijal2 = new Materijal { Naziv = "Materijal 2", Kolicina = 5, CijenaMaterijala = 4, JedinicaMjere = "kg", OpasnoPoZivot = false };
            var materijal3 = new Materijal { Naziv = "Materijal 3", Kolicina = 8, CijenaMaterijala = 16, JedinicaMjere = "kg", OpasnoPoZivot = false };

            var lista = new List<Materijal> { materijal1, materijal2, materijal3 };

            var fakeRepo = A.Fake<IMaterijalRepository>();
            A.CallTo(() => fakeRepo.GetAll()).Returns(lista.AsQueryable());

            var fakeServis = new MaterijalServices(fakeRepo);

            // Act
            var rezultat = fakeServis.IzvozMaterijala();

            // Assert
            Assert.NotEmpty(rezultat);
        }

        [Fact]
        public void GeneracijaCSV_ListaMaterijalaJePrazna_VracaPrazanString() {

            List<Materijal> lista = new List<Materijal>();
            var fakeRepo = A.Fake<IMaterijalRepository>();
            A.CallTo(() => fakeRepo.GetAll()).Returns(lista.AsQueryable());

            var fakeServis = new MaterijalServices(fakeRepo);
            
            // Act
            var rezultat = fakeServis.GeneracijaCSV(lista);

            // Assert
            Assert.Empty(rezultat);
        }

        [Fact]
        public void Pretrazi_PretraziKlijentaPoNazivu_VracaKlijenta()
        {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijenti = new List<Klijent>()
            {
                new Klijent { Naziv = "Preis", OIB = "82812572912", Adresa = "Lobor 35", IBAN = "HR6322120061696361472", BrojTelefona = "0916401323", Email = "email@gmail.com", Mjesto = "Lobor"},
                new Klijent { Naziv = "Prgomet", OIB = "28125727125", Adresa = "Krapina 35", IBAN = "HR6327120061696361472", BrojTelefona = "0917285721", Email = "maxos@gmail.com", Mjesto = "Krapina"},
            };
            A.CallTo(() => fakeRepo.Pretrazi("Pr")).Returns(klijenti.AsQueryable());
            var fakeServis = new KlijentServices(fakeRepo);

            //Act
            var pretrazeniKlijenti = fakeServis.Pretrazi("Pr");

            //Assert
            Assert.NotNull(pretrazeniKlijenti);
            Assert.Equal(pretrazeniKlijenti, klijenti);
        }

        [Fact]
        public void Pretrazi_KaoParametarNemojProslijeditiNista_VracaPopisSvihKlijenata() {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijenti = new List<Klijent>()
            {
                new Klijent { Naziv = "Preis", OIB = "82812572912", Adresa = "Lobor 35", IBAN = "HR6322120061696361472", BrojTelefona = "0916401323", Email = "email@gmail.com", Mjesto = "Lobor"},
                new Klijent {Naziv = "Prgomet", OIB = "28125727125", Adresa = "Krapina 35", IBAN = "HR6327120061696361472", BrojTelefona = "0917285721", Email = "maxos@gmail.com", Mjesto = "Krapina"},
                new Klijent {Naziv = "Muzar", OIB = "72812959212", Adresa = "Durmanec", IBAN = "HR9291258212582716281", BrojTelefona = "09125821252", Email = "muzar@gmail.com", Mjesto = "Durmanec"}
            };
            A.CallTo(() => fakeRepo.Pretrazi("")).Returns(klijenti.AsQueryable());
            var fakeServis = new KlijentServices(fakeRepo);

        }

        [Fact]
        public void GeneracijaCSV_ListaJeIspravna_VracaGeneriraniString() {
            var materijal1 = new Materijal { Naziv = "Materijal 1", Kolicina = 10, CijenaMaterijala = 46, JedinicaMjere = "kg", OpasnoPoZivot = false };
            var materijal2 = new Materijal { Naziv = "Materijal 2", Kolicina = 5, CijenaMaterijala = 4, JedinicaMjere = "kg", OpasnoPoZivot = false };
            var materijal3 = new Materijal { Naziv = "Materijal 3", Kolicina = 8, CijenaMaterijala = 16, JedinicaMjere = "kg", OpasnoPoZivot = false };

            var lista = new List<Materijal> { materijal1, materijal2, materijal3 };

            var fakeRepo = A.Fake<IMaterijalRepository>();
            A.CallTo(() => fakeRepo.GetAll()).Returns(lista.AsQueryable());

            var fakeServis = new MaterijalServices(fakeRepo);
            string ocekivaniCSV = "Materijal_ID,Naziv,CijenaMaterijala,JedinicaMjere,Kolicina,Opis,OpasnoPoZivot,QR_kod,Primka_ID,Usluga_ID,Primka_ID,Naziv_Materijal,Datum,Kolicina,Usluga_ID,Naziv,QR_kod,CijenaUsluge\r\n" +
                       "0,Materijal 1,46,kg,10,,False,,,,0,,,,0,,,0\r\n" +
                       "0,Materijal 2,4,kg,5,,False,,,,0,,,,0,,,0\r\n" +
                       "0,Materijal 3,16,kg,8,,False,,,,0,,,,0,,,0\r\n";



            // Act

            var rezultat = fakeServis.GeneracijaCSV(lista);

            // Assert
            Assert.Equal(rezultat, ocekivaniCSV);
        }

            //Act
            /*var pretrazeniKlijenti = fakeServis.Pretrazi("");

            //Assert
            Assert.NotNull(pretrazeniKlijenti);
            Assert.Equal(klijenti, pretrazeniKlijenti*/

        [Fact]
        public void Pretrazi_KaoParametarProslijediStringSNazivomKojiNePostojiUBazi_Vrati0Klijenata()
        {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            List<Klijent> klijenti = new List<Klijent>();
            A.CallTo(() => fakeRepo.Pretrazi("kszazsdsa")).Returns(klijenti.AsQueryable());
            var fakeServis = new KlijentServices(fakeRepo);

            //Act
            var pretrazeniKlijenti = fakeServis.Pretrazi("kszazsdsa");

            //Assert
            Assert.Empty(klijenti);
        }

        [Fact]
        public void SortirajKlijentePoUkupnomBrojuRacuna_PostojeKlijentiUBazi_VratiSortiraneKlijentePoUkupnomBrojuRacuna()
        {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijenti = new List<Klijent>()
            {
                new Klijent { Naziv = "Preis", OIB = "82812572912", Adresa = "Lobor 35", IBAN = "HR6322120061696361472", BrojTelefona = "0916401323", Email = "email@gmail.com", Mjesto = "Lobor"},
                new Klijent {Naziv = "Prgomet", OIB = "28125727125", Adresa = "Krapina 35", IBAN = "HR6327120061696361472", BrojTelefona = "0917285721", Email = "maxos@gmail.com", Mjesto = "Krapina"},
                new Klijent {Naziv = "Muzar", OIB = "72812959212", Adresa = "Durmanec", IBAN = "HR9291258212582716281", BrojTelefona = "09125821252", Email = "muzar@gmail.com", Mjesto = "Durmanec"}
            };

            var racun1 = new Racun() { Klijent = klijenti[0], Klijent_ID = klijenti[0].Klijent_ID, UkupnaCijena = 100 };
            var racun2 = new Racun() { Klijent = klijenti[0], Klijent_ID = klijenti[0].Klijent_ID, UkupnaCijena = 200 };
            var racun3 = new Racun() { Klijent = klijenti[1], Klijent_ID = klijenti[1].Klijent_ID, UkupnaCijena = 300 };
            A.CallTo(() => fakeRepo.SortirajKlijentePoUkupnomBrojuRacuna()).Returns(klijenti.OrderBy(k => k.ukupniBrojRacuna).AsQueryable());
            var fakeServis = new KlijentServices(fakeRepo);

            //Act
            var sortiraniKlijenti = fakeServis.SortirajKlijentePoUkupnomBrojuRacuna();

            //Assert
            Assert.True(sortiraniKlijenti[0].Naziv == "Preis");
            Assert.True(sortiraniKlijenti[1].Naziv == "Prgomet");
            Assert.True(sortiraniKlijenti[2].Naziv == "Muzar");

        }
    }      
 }
