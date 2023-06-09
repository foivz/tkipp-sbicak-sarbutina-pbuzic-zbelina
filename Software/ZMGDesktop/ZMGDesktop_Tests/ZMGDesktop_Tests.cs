using BusinessLogicLayer.Services;
using DataAccessLayer.Iznimke;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using FakeItEasy;
using Microsoft.SqlServer.Server;
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
    public class ZMGDesktop_Tests
    {
        [Fact]
        public void UcitajKlijente_KlijentiPostojeuBazi_KlijentiSePrikazujuUSustavu()
        {
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

        private Klijent kreirajKlijenta(string naziv, string oib, string adresa, string iban, string brojTelefona, string email, string mjesto)
        {
            return new Klijent
            {
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
        public void Add_IspunjeniSviPodaciZaKlijenta_KlijentUspjesnoDodan()
        {
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
        public void Add_KlijentSPostojecimImenom_BacaGresku()
        {
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
        public void Add_KlijentSPostojecimOibom_BacaGresku()
        {
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
        public void Add_KlijentSPostojecimEmailom_BacaGresku()
        {
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
        public void Add_KlijentSPostojecimTelefonom_BacaGresku()
        {
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
        public void Add_KlijentSPostojecimIBANRacunom_BacaGresku()
        {
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
        public void DohvatiDesetNajboljih_PostojeDesetNajvecihKlijenata_KaoRezultatVracaDesetNajvecihKlijenata()
        {
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
        public void DohvatiDesetNajboljihKlijenata_NemaDovoljnoPodatkaUBaziZaDesetNajvecihKlijenata_BacaGresku()
        {
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
        public void AzurirajKlijenta_PostaviNazivNaPostojeciUBazi_VratiGreskuDaVecPostojiKlijentSTakvimNazivomUBazi()
        {
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
        public void AzurirajKlijenta_PromijeniOdgovarajuceSvojstvo_UspjesnoAzuriranKlijent()
        {
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
        public void DohvatiRacuneZaKlijenta_PostojeViseRacunaZaKlijenta_VracaPopisRacunaVezanihZaKlijenta()
        {
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
        public void DohvatiRadneNalogeZaKlijenta_PostojeRadniNaloziZaKlijenta_VracaPopisRadnihNalogaVezanihZaKlijenta()
        {
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

        private Validacija kreirajValidaciju()
        {
            return new Validacija();
        }

        [Fact]

        public void ProvjeraOib_OIBNeispravan_VracaFalse()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraOIB("728129529182");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void ProvjeraOib_OIBIspravan_VracaTrue()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraOIB("27125827125");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]

        public void ProvjeraMaila_MailNeispravan_VracaFalse()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraMaila("nesto@122'.com");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void ProvjeraMaila_MailIspravan_VracaTrue()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraMaila("tvrtka@gmail.com");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]

        public void ProvjeraRacuna_RacunNeispravan_VracaFalse()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraRacuna("HR828192591257271927217");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void ProvjeraRacuna_RacunIspravan_VracaTrue()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraRacuna("HR4524020061552417385");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]

        public void provjeraSamoSlova_NeispravanNaziv_VracaFalse()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraSamoSlova("Ivan123");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void provjeraSamoSlova_IspravanNaziv_VracaTrue()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraSamoSlova("Ivan");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]

        public void provjeraMjesta_NeispravnoMjesto_VracaFalse()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraMjesta("Novi '.Vinodolski");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void provjeraMjesta_IspravnoMjesto_VracaTrue()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraMjesta("Novi Vinodolski");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]

        public void provjeraUlica_NeispravnaUlica_VracaFalse()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraUlica("Apple# Street");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void provjeraUlica_IspravnaUlica_VracaTrue()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraUlica("10th Avenue");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]

        public void provjeraSamoBrojevi_StringSadrziSlova_VracaFalse()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraSamoBrojevi("123cetiri");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void provjeraSamoBrojevi_StringSadrziSamoBrojeve_VracaTrue()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraSamoBrojevi("12456123123");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]

        public void provjeraTelefon_NeispravanBrojTelefona_VracaFalse()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraTelefon("092124281241");
            //Assert
            Assert.False(uspjesno);
        }

        [Fact]

        public void provjeraTelefon_IspravanBrojTelefona_VracaTrue()
        {
            //Arrange
            Validacija valid = kreirajValidaciju();
            //Act
            bool uspjesno = valid.provjeraTelefon("0992718252");
            //Assert
            Assert.True(uspjesno);
        }

        [Fact]
        public void Add_ProslijediKaoParametarKlijentaIFalse_Vraca0()
        {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = kreirajKlijenta("Preis", "82812572912", "Lobor 35", "HR6322120061696361472", "0916401323", "email@gmail.com", "Lobor");
            
            //Act
            int red = fakeRepo.Add(klijent, false);

            //Assert
            Assert.True(red == 0);
        }

        [Fact]
        public void Update_ProslijediKaoParametarKlijentaIFalse_Vraca0()
        {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = kreirajKlijenta("Preis", "82812572912", "Lobor 35", "HR6322120061696361472", "0916401323", "email@gmail.com", "Lobor");

            //Act
            int red = fakeRepo.Update(klijent, false);

            //Assert
            Assert.True(red == 0);
        }

        [Fact]
        public void Remove_ProslijediKaoParametarKlijentaIFalse_Vraca0()
        {
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
        public void DohvatiRadneNaloge_RadniNaloziPostojeUBazi_VracaRadneNaloge()
        {
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
        public void DohvatiRadneNalogePoStatusima_RadniNaloziPostojeUBazi_VracaRadneNalogePoStatusima()
        {
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
        public void DodajRadniNalog_UpisaniSviPodaciIspravno_RadniNalogUspjenoDodanUBazu()
        {
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
        public void ObrisiRadniNalog_RadniNalogPostojiUBazi_RadniNalogUspjenoObrisan()
        {
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
        public void AzurirajRadniNalog_IzmijenjenaKolicinaRadnogNaloga_RadniNalogUspjenoIzmjenjen()
        {
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
        public void DohvatiRobuKlijenta_PostojiKlijentSRobom_VraćenaKlijentovaRoba()
        {
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
        public void DohvatiSvuRobu_PostojiRobaUBazi_VraćenaRoba()
        {
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
        public void Add_UpisaniSviPodaciIspravno_DodanNoviMaterijal()
        {
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

        [Fact]
        public async Task Login_IspravniPodaci_VracaRadnika() {
            // Arrange
            var korime = "sarbutina20";
            var lozinka = "12345";
            var radnik = new Radnik{
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
        public async Task Login_NeispravniPodaci_VracaNull() {
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
    }
}
