using BusinessLogicLayer.Services;
using DataAccessLayer.Iznimke;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using FakeItEasy;
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
        public void Pretrazi_KaoParametarNemojProslijeditiNista_VracaPopisSvihKlijenata()
        {
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

            //Act
            var pretrazeniKlijenti = fakeServis.Pretrazi("");

            //Assert
            Assert.NotNull(pretrazeniKlijenti);
            Assert.Equal(klijenti, pretrazeniKlijenti);
        }

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
