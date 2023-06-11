using BusinessLogicLayer.Services;
using DataAccessLayer.Iznimke;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;
using Xunit.Sdk;
using ZMGDesktop;
using ZMGDesktop.ValidacijaUnosa;

namespace ZMG.IntegrationTests
{
    public class ZMGIntegrationTests
    {
        private KlijentServices _klijentServices;
        private RadniNalogService RadniNalogService;
        private RadnikServices RadnikServices;
        private RobaService RobaService;
        private PrimkaServices PrimkaService;
        private MaterijalServices MaterijalService;
        private UslugaServices UslugaService;

        private void kreirajServis()
        {
            _klijentServices = new KlijentServices(new KlijentRepository());
            RadniNalogService = new RadniNalogService(new RadniNalogRepository());
            RadnikServices = new RadnikServices(new RadnikRepository());
            RobaService = new RobaService(new RobaRepository());
            PrimkaService = new PrimkaServices(new PrimkaRepository());
            MaterijalService = new MaterijalServices(new MaterijalRepository());
            UslugaService = new UslugaServices(new UslugaRepository());
        }

        private Validacija kreirajValidator()
        {
            return new Validacija();
        }

        [Fact]
        public void UcitajKlijente_PostojeKlijentiUBazi_KlijentiUspjesnoPrikazaniUTablici()
        {
            //Arrange
            kreirajServis();

            //Act
            List<Klijent> klijenti = _klijentServices.DohvatiKlijente();

            //Assert
            Assert.NotNull(klijenti);
            Assert.IsType<List<Klijent>>(klijenti);
        }

        [Fact]
        public void IzbrisiKlijenta_KlijentImaRadniNalog_BacaGresku()
        {
            //Arrange
            kreirajServis();
            var klijenti = _klijentServices.DohvatiKlijente();
            var klijent = klijenti.SingleOrDefault(k => k.Naziv == "Aggreko");

            //Act
            Action act = () => _klijentServices.Remove(klijent);
            //Assert
            Assert.Throws<BrisanjeKlijentaException>(() => act());
        }

        [Fact]
        public void IzbrisiKlijenta_KlijentNemaRadniNalogIliRobuIliRacun_KlijentUspjesnoObrisan()
        {
            //Arrange
            kreirajServis();
            var klijenti = _klijentServices.DohvatiKlijente();
            var klijent = klijenti.SingleOrDefault(k => k.Naziv == "Toni");
            int brojKlijenata = klijenti.Count;

            //Act
            _klijentServices.Remove(klijent);
            

            //Assert
            Assert.True(brojKlijenata != _klijentServices.DohvatiKlijente().Count);
            _klijentServices.Add(klijent);
        }

        [Fact]
        public void DodajKlijenta_IspunjeniSviPodaci_KlijentDodanUBazu()
        {
            //Arrange
            kreirajServis();
            var klijent = new Klijent
            {
                Naziv = "Toni",
                Adresa = "Erpenja 30",
                Mjesto = "Krapina",
                OIB = "72521592912",
                BrojTelefona = "0962125821",
                Email = "toni@gmail.com",
                IBAN = "HR2817502616521258291"
            };

            //Act
            bool uspjesno = _klijentServices.Add(klijent);

            //Assert
            Assert.True(uspjesno);
            
        }

        [Fact]
        public void DodajKlijenta_VecPostojeciNaziv_BacaGresku()
        {
            //Arrange
            kreirajServis();

            var klijent = new Klijent
            {
                Naziv = "Aggreko",
                Adresa = "Zlatar 30",
                Mjesto = "Zlatar",
                OIB = "21693125721",
                BrojTelefona = "0917261582",
                Email = "preis@gmail.com",
                IBAN = "HR2817502816521258291"
            };

            //Act
            Action act = () => _klijentServices.Add(klijent);

            //Assert
            Assert.Throws<UserException>(() => act());
        }

        [Fact]
        public void DodajKlijenta_VecPostojeciOib_BacaGresku()
        {
            //Arrange
            kreirajServis();

            var klijent = new Klijent
            {
                Naziv = "Ceha",
                Adresa = "Zlatar 30",
                Mjesto = "Zlatar",
                OIB = "21852758212",
                BrojTelefona = "0917261582",
                Email = "preis@gmail.com",
                IBAN = "HR2817502816521258291"
            };

            //Act
            Action act = () => _klijentServices.Add(klijent);

            //Assert
            Assert.Throws<OIBException>(() => act());
        }

        [Fact]
        public void DodajKlijenta_VecPostojeciIBAN_BacaGresku()
        {
            //Arrange
            kreirajServis();

            var klijent = new Klijent
            {
                Naziv = "Ceha",
                Adresa = "Zlatar 30",
                Mjesto = "Zlatar",
                OIB = "27125281257",
                BrojTelefona = "0917261582",
                Email = "preis@gmail.com",
                IBAN = "HR2817528125291258271"
            };

            //Act
            Action act = () => _klijentServices.Add(klijent);

            //Assert
            Assert.Throws<IBANException>(() => act());
        }

        [Fact]
        public void DodajKlijenta_VecPostojeciTelefon_BacaGresku()
        {
            //Arrange
            kreirajServis();

            var klijent = new Klijent
            {
                Naziv = "Ostrc",
                Adresa = "Belec  30",
                Mjesto = "Belec",
                OIB = "62712592012",
                BrojTelefona = "0913955196",
                Email = "ostrc@gmail.com",
                IBAN = "HR2817521722391258271"
            };

            //Act
            Action act = () => _klijentServices.Add(klijent);

            //Assert
            Assert.Throws<TelefonException>(() => act());
        }

        [Fact]
        public void DodajKlijenta_VecPostojeciEmail_BacaGresku()
        {
            //Arrange
            kreirajServis();

            var klijent = new Klijent
            {
                Naziv = "Ceha",
                Adresa = "Zlatar 30",
                Mjesto = "Zlatar",
                OIB = "27125281257",
                BrojTelefona = "0972612852",
                Email = "aggreko@gmail.com",
                IBAN = "HR2817521225291258271"
            };

            //Act
            Action act = () => _klijentServices.Add(klijent);

            //Assert
            Assert.Throws<EmailException>(() => act());
        }

        [Fact]  
        public void AzurirajKlijenta_SviPodaciIspravni_UspjesnoAzuriranKlijent()
        {
            //Arrange
            kreirajServis();

            var klijenti = _klijentServices.DohvatiKlijente();
            var azuriranKlijent = klijenti.SingleOrDefault(k => k.Naziv == "Adriatic");
            azuriranKlijent.Mjesto = "Zapresic";

            //Act
            _klijentServices.Update(azuriranKlijent);

            //Assert
            klijenti = _klijentServices.DohvatiKlijente();
            Assert.True(klijenti.SingleOrDefault(k => k.Naziv == "Adriatic").Mjesto == "Zapresic");
        }

        [Fact]
        public void UcitajRadneNalogeUcitajRacune_PostojeRacuniIRadniNaloziZaKlijenta_PrikazujuSeRadniNaloziIRacuniZaKlijenta()
        {
            //Arrange
            kreirajServis();
            var racunServis = new RacunService(new RacunRepository());
            var klijenti = _klijentServices.DohvatiKlijente();
            var klijent = klijenti.SingleOrDefault(k => k.Naziv == "Impuls");

            //Act
            int racuni = racunServis.DohvatiRacuneZaKlijenta(klijent).Count;
            int radniNalozi = RadniNalogService.DohvatiRadneNalogeZaKlijenta(klijent).Count;

            //Assert
            Assert.True(racuni == 2 && radniNalozi == 1);
        }

        [Fact]
        public void KreirajIzvjestaj_ImaDovoljnoPodataka_KreiranIzvjestajDesetNajvecihKlijenata()
        {
            //Arrange
            kreirajServis();

            //Act
            _klijentServices.DohvatiDesetNajboljih();

            //Assert
            Assert.Equal(10, _klijentServices.DohvatiDesetNajboljih().Count);
        }

        [Fact]
        public void Add_ProslijediKaoParametarKlijentaIFalse_Vraca0()
        {
            //Arrange
            var repo = new KlijentRepository();
            var klijent = new Klijent
            {
                Naziv = "Zasjades",
                Adresa = "Marusevec 20",
                Mjesto = "Varazdin",
                OIB = "82717572612",
                BrojTelefona = "0912748261",
                Email = "buzic@gmail.com",
                IBAN = "HR2817520125291258271"
            };

            //Act
            int red = repo.Add(klijent, false);

            //Assert
            Assert.Equal(red, 0);
        }

        [Fact]
        public void Update_ProslijediKaoParametarKlijentaIFalse_Vraca0()
        {
            //Arrange
            var repo = new KlijentRepository();
            var klijenti = repo.GetAll();
            var klijent = klijenti.SingleOrDefault(k => k.Naziv == "Aggreko");
            klijent.Adresa = "Krapina 42";
            klijent.Mjesto = "Krapina";

            //Act
            int red = repo.Update(klijent, false);

            //Assert
            Assert.Equal(red, 0);
        }

        [Fact]
        public void Remove_ProslijediKaoParametarKlijentaIFalse_Vraca0()
        {
            //Arrange
            var repo = new KlijentRepository();
            var klijent = new Klijent
            {
                Naziv = "Zasjades",
                Adresa = "Marusevec 20",
                Mjesto = "Varazdin",
                OIB = "82717572612",
                BrojTelefona = "0912748261",
                Email = "buzic@gmail.com",
                IBAN = "HR2817520125291258271"
            };

            //Act
            int red = repo.Remove(klijent, false);

            //Assert
            Assert.Equal(red, 0);
        }

        // INTEGRACIJSKI TESTOVI RADNIH NALOGA
        [Fact]
        public void DohvatiSveRadneNaloge_PostojeRadniNaloziUBazi_RadniNaloziUspjenoPrikazani()
        {
            //Arrange
            kreirajServis();

            //Act
            List<RadniNalog> radniNalozi = RadniNalogService.DohvatiRadneNaloge();

            //Assert
            Assert.NotNull(radniNalozi);
            Assert.IsType<List<RadniNalog>>(radniNalozi);
        }

        [Fact]
        public void DohvatiRadneNalogePoStatusima_PostojeRadniNaloziUBazi_RadniNaloziPoStatusimaUspjenoPrikazani()
        {
            //Arrange
            kreirajServis();

            //Act
            List<RadniNalog> radniNalozi = RadniNalogService.DohvatiRadneNalogePoStatusima();

            //Assert
            Assert.NotNull(radniNalozi);
            Assert.IsType<List<RadniNalog>>(radniNalozi);
        }

        [Fact]
        public void Add_IspunjeniSviPodaci_UspješnoDodanRadniNalogUBazu()
        {
            //Arrange
            kreirajServis();

            List<Materijal> materijal = new List<Materijal>();
            List<Roba> roba = new List<Roba>();

            var klijenti = _klijentServices.DohvatiKlijente();
            var klijent = klijenti.FirstOrDefault(k => k.Klijent_ID == 152);
            var radnici = RadnikServices.DohvatiSveRadnike();
            var radnik = radnici.FirstOrDefault(r => r.Radnik_ID == 26);

            materijal.Add(new Materijal { Materijal_ID = 40, Naziv = "Celik", CijenaMaterijala = 20, JedinicaMjere = "ppm", Kolicina = 1, Opis = "Materijal integracijskih testova", OpasnoPoZivot = false, QR_kod = "E463XXDVTR0J94Y6LE6H" });
            roba.Add(new Roba { Roba_ID = 181, Naziv = "Šipka e2", Kolicina = "5", Klijent_ID = 149 });

            var radniNalog = new RadniNalog {
                Kolicina = 15,
                Opis = "Treba integracijski testirat",
                QR_kod = "NEK1QR123KOD12345678",
                DatumStvaranja = DateTime.Now,
                Status = "Napravljen",
                Radnik_ID = radnik.Radnik_ID,
                Klijent_ID = klijent.Klijent_ID,
                Klijent = klijent,
                Radnik = radnik,
                Materijal = materijal,
                Roba = roba
            };

            //Act
            bool uspjesno = RadniNalogService.DodajRadniNalog(radniNalog);

            //Assert
            Assert.True(uspjesno);
        }

        [Fact]
        public void Add_IspunjeniSviPodaciOsimMaterijalaIRobe_BacanjeIznimke()
        {
            //Arrange
            kreirajServis();

            List<Materijal> materijal = new List<Materijal>();
            List<Roba> roba = new List<Roba>();

            var klijenti = _klijentServices.DohvatiKlijente();
            var klijent = klijenti.FirstOrDefault(k => k.Klijent_ID == 152);
            var radnici = RadnikServices.DohvatiSveRadnike();
            var radnik = radnici.FirstOrDefault(r => r.Radnik_ID == 26);

            var radniNalog = new RadniNalog {
                Kolicina = 15,
                Opis = "Treba integracijski testirat",
                QR_kod = "NEK1QR123KOD12345678",
                DatumStvaranja = DateTime.Now,
                Status = "Napravljen",
                Radnik_ID = radnik.Radnik_ID,
                Klijent_ID = klijent.Klijent_ID,
                Klijent = klijent,
                Radnik = radnik,
                Materijal = materijal,
                Roba = roba
            };

            //Act
            Action act = () => RadniNalogService.DodajRadniNalog(radniNalog);

            //Assert
            Assert.Throws<MaterijalRobaException>(() => act());
        }

        [Fact]
        public void Update_IspunjeniSviPodaci_UspješnoAžuriranRadniNalog()
        {
            //Arrange
            kreirajServis();

            List<Materijal> materijal = new List<Materijal>();
            List<Roba> roba = new List<Roba>();

            var klijenti = _klijentServices.DohvatiKlijente();
            var klijent = klijenti.FirstOrDefault(k => k.Klijent_ID == 152);
            var radnici = RadnikServices.DohvatiSveRadnike();
            var radnik = radnici.FirstOrDefault(r => r.Radnik_ID == 26);
            var radniNalozi = RadniNalogService.DohvatiRadneNaloge();
            var radniNalog = radniNalozi.LastOrDefault();

            var azuriraniRadniNalog = new RadniNalog {
                RadniNalog_ID = radniNalog.RadniNalog_ID,
                Kolicina = 20,
                Opis = "Stvarno treba integracijski testirat",
                QR_kod = "NEK1QR123KOD12345678",
                DatumStvaranja = DateTime.Now,
                Status = "U obradi",
                Radnik_ID = radnik.Radnik_ID,
                Klijent_ID = klijent.Klijent_ID,
                Klijent = klijent,
                Radnik = radnik
            };

            //Act
            bool uspjesno = RadniNalogService.AzurirajRadniNalog(azuriraniRadniNalog);

            //Assert
            Assert.True(uspjesno);
        }

        [Fact]
        public void Remove_BrisanjeRadnogNaloga_UspješnoObrisanRadniNalog()
        {
            //Arrange
            kreirajServis();
            var radniNalozi = RadniNalogService.DohvatiRadneNaloge();
            var radniNalog = radniNalozi.LastOrDefault();

            //Act
            RadniNalogService.ObrisiRadniNalog(radniNalog);

            //Assert
            Assert.Equal(radniNalog, radniNalozi.LastOrDefault());
        }

        // INTEGRACIJSKI TESTOVI ROBE
        [Fact]
        public void Add_IspunjeniSviPodaci_UspješnoDodanaRobaUBazu()
        {
            //Arrange
            kreirajServis();
            var roba = new Roba {
                Naziv = "Testna roba",
                Kolicina = "1",
                Klijent_ID = 152
            };

            //Act
            bool uspjesno = RobaService.Add(roba);

            //Assert
            Assert.True(uspjesno);
        }

        [Fact]
        public void DohvatiRobuKlijenta_PostojiKlijentSRobom_VraćenaKlijentovaRoba()
        {
            //Arrange
            kreirajServis();
            var klijenti = _klijentServices.DohvatiKlijente();
            var klijent = klijenti.FirstOrDefault(k => k.Klijent_ID == 152);

            //Act
            List<Roba> roba = RobaService.DohvatiRobuKlijenta(klijent.Klijent_ID);

            //Assert
            Assert.NotNull(roba);
        }

        // TDD  - Test za funkcionalnost Pregled robe
        [Fact]
        public void DohvatiSvuRobu_RobaPostojiUBazi_DohvacenaRoba()
        {
            //Arrange
            var robaService = new RobaService(new RobaRepository());

            //Act
            List<Roba> listaRobe = robaService.DohvatiSvuRobu();

            //Assert
            Assert.NotNull(listaRobe);
        }


        // TESTOVI ZA SERVIS I REPOZITORIJ RADNIKA
        [Fact]
        public async Task ProvjeriRadnikaAsync_IspravniPodaci_VracaRadnika() {
            //Arrange
            kreirajServis();
            var radnici = RadnikServices.DohvatiSveRadnike();
            var radnik = radnici.FirstOrDefault(k => k.Korime == "sarbutina20");

            string korime = radnik.Korime;
            string lozinka = "12345"; // MORAO SAM JER BI INAČE MORAO ODHASHIRATI LOZINKU ŠTO JE LOŠIJA OPCIJA OD OVOGA
            //Act
            Radnik provjereniRadnik = await RadnikServices.ProvjeriRadnikaAsync(korime, lozinka);

            //Assert
            Assert.Equal(radnik, provjereniRadnik);
            
        }

        [Fact]
        public async Task ProvjeriRadnikaAsync_NeispravniPodaci_VracaNull() {
            //Arrange
            kreirajServis();
            
            //Act
            Radnik provjereniRadnik = await RadnikServices.ProvjeriRadnikaAsync("", "");

            //Assert
            Assert.Null(provjereniRadnik);

        }

        // TESTOVI ZA SERVIS I REPOZITORIJ MATERIJALA
        public void DohvatiMaterijale_IspravnoPozvanaMetoda_VracaListuMaterijala() {
            // Arrange
            kreirajServis();

            // Act
            List<Materijal> materijali = MaterijalService.DohvatiMaterijale();


            // Assert
            Assert.NotNull(materijali);
        }


        [Fact]
        public void ProvjeriQR_IspravanQRKod_VracaTrue() {
            // Arrange
            kreirajServis();

            var materijali = MaterijalService.DohvatiMaterijale();
            var materijal = materijali.FirstOrDefault(k => k.Naziv == "Celik");


            // Act
            var rezultat = MaterijalService.ProvjeriQR(materijal.QR_kod);

            // Assert
            Assert.True(rezultat);
        }

        [Fact]
        public void ProvjeriQR_NeispravanQRKod_VracaFalse() {
            // Arrange
            kreirajServis();


            // Act
            var rezultat = MaterijalService.ProvjeriQR(null);

            // Assert
            Assert.False(rezultat);
        }

        [Fact]
        public void AzurirajMaterijal_IspravanQRiKolicina_VracaMaterijal() {
            // Arrange
            kreirajServis();

            var materijali = MaterijalService.DohvatiMaterijale();
            var materijal = materijali.FirstOrDefault(k => k.Naziv == "Celik");

            // Act
            var rezultat = MaterijalService.AzurirajMaterijal(materijal.QR_kod, materijal.Kolicina);

            // Assert
            Assert.Equal(materijal, rezultat);
        }

        [Fact]
        public void ObrisiMaterijal_IspravanMaterijal_VracaTrue() {
            // Arrange
            kreirajServis();
            var materijali = MaterijalService.DohvatiMaterijale();
            var materijal = materijali.FirstOrDefault(k => k.QR_kod == "AR33EDGHUDDW2SVESA22RF");

            // Act
            var rezultat = MaterijalService.ObrisiMaterijal(materijal);

            // Assert
            Assert.True(rezultat);
        }

        [Fact]
        public void ObrisiMaterijal_PostojeciMaterijal_VracaIznimku() {
            // Arrange
            kreirajServis();
            var materijali = MaterijalService.DohvatiMaterijale();
            var materijal = materijali.FirstOrDefault(k => k.Naziv == "Celik");

            //Act
            Action act = () => MaterijalService.ObrisiMaterijal(materijal);

            // Assert
            Assert.Throws<BrisanjeMaterijalaException>(() => act());
        }



        [Fact]
        public void DodajMaterijal_IspravanMaterijal_VracaTrue() {
            // Arrange
            kreirajServis();
            Materijal materijal = new Materijal {
                Naziv = "Testni materijal",
                Kolicina = 156,
                QR_kod = "AR33EDGHUDDW2SVESA22RF",
                Opis = " ",
                OpasnoPoZivot = false,
                CijenaMaterijala = 157.4,
                JedinicaMjere = "kg"
            };

            // Act
            var rezultat = MaterijalService.DodajMaterijal(materijal);

            // Assert
            Assert.True(rezultat);
        }

        [Fact]
        public void Add_IspravanMaterijal_SaveChangesJeFalse() {
            // Arrange
            kreirajServis();
            Materijal materijal = new Materijal {
                Naziv = "Testni materijal 2",
                Kolicina = 124,
                QR_kod = "AR33EDGHUAAVV2SVESA11RF",
                Opis = " ",
                OpasnoPoZivot = false,
                CijenaMaterijala = 157.4,
                JedinicaMjere = "kg"
            };

            MaterijalRepository repo = new MaterijalRepository();
            // Act
            var rezultat = repo.Add(materijal, false);

            // Assert
            Assert.Equivalent(rezultat, 0);
        }
        /*
        [Fact]
        public void Remove_IspravanMaterijal_SaveChangesJeFalse() {
            // Arrange
            kreirajServis();
            var materijali = MaterijalService.DohvatiMaterijale();
            var materijal = materijali.FirstOrDefault(k => k.Naziv == "Testni materijal 2");

            MaterijalRepository repo = new MaterijalRepository();
            // Act
            var rezultat = repo.Remove(materijal, false);

            // Assert
            Assert.Equivalent(rezultat, 0);
        }*/

        [Fact]
        public void DodajMaterijal_NeispravanMaterijal_VracaIznimku() {
            // Arrange
            kreirajServis();
            Materijal materijal = new Materijal {
                Naziv = "Celik"
            };

            //Act
            Action act = () => MaterijalService.DodajMaterijal(materijal);

            // Assert
            Assert.Throws<InvalidOperationException>(() => act());
        }



        // TESTOVI ZA SERVIS I REPOZITORIJ USLUGA
        [Fact]
        public void DohvatiUsluge_IspravnoPozvanaMetoda_VracaListuUsluga() {
            // Arrange
            kreirajServis();


            // Act
            var rezultat = UslugaService.DohvatiUsluge();

            // Assert
            Assert.NotNull(rezultat);
        }

        [Fact]
        public void DohvatiUslugeDistinct_IspravnoPozvanaMetoda_VracaListuUsluga() {
            // Arrange
            kreirajServis();

            var rezultat = UslugaService.DohvatiUslugeDistinct();

            // Assert
            Assert.NotNull(rezultat);
        }

        [Fact]
        public void DohvatiUsluguPoNazivu_IspravnoPozvanaMetoda_VracaListuUsluga() {
            // Arrange
            kreirajServis();

            string naziv = "Cincanje";

            var rezultat = UslugaService.DohvatiUsluguPoNazivu(naziv);

            // Assert
            Assert.NotNull(rezultat);
        }




        // TESTOVI ZA SERVIS I REPOZITORIJ PRIMKE
        [Fact]
        public void DodajPrimku_IspravnaPrimka_VracaTrue() {
            // Arrange
            kreirajServis();
            Primka primka = new Primka {
                Naziv_Materijal = "Celik",
                Kolicina = 146,
                Datum = DateTime.Now
            };

            var rezultat = PrimkaService.DodajPrimku(primka);

            // Assert
            Assert.True(rezultat);
        }

        [Fact]
        public void Add_ProsljedenSaveChangesFalse_VracaFalse() {
            // Arrange
            kreirajServis();
            Primka primka = new Primka {
                Naziv_Materijal = "Celik",
                Kolicina = 146,
                Datum = DateTime.Now
            };

            PrimkaRepository repo = new PrimkaRepository();
            var rezultat = repo.Add(primka, false);


            // Assert
            Assert.Equivalent(rezultat, 0);
        }

        [Fact]
        public void Add_PostojecaPrimka_VracaIznimku() {
            // Arrange
            kreirajServis();
            PrimkaRepository repo = new PrimkaRepository();
            var primke = repo.GetAll().ToList();
            var primka = primke.LastOrDefault();

            Action act = () => repo.Add(primka, false);

            // Assert
            Assert.Throws<InvalidOperationException>(() => act());
        }



        // NEOSTAVARENE METODE

        [Fact]
        public void PrimkaUpdate_PravilnoPozvano_VracaIznimku() {
            // Arrange
            kreirajServis();
            PrimkaRepository repo = new PrimkaRepository();
            Primka primka = new Primka();
            Action act = () => repo.Update(primka, false);

            // Assert
            Assert.Throws<NotImplementedException>(() => act());
        }

        [Fact]
        public void RadnikUpdate_PravilnoPozvano_VracaIznimku() {
            // Arrange
            kreirajServis();
            Radnik radnik = new Radnik();
            RadnikRepository repo = new RadnikRepository();

            Action act = () => repo.Update(radnik, false);

            // Assert
            Assert.Throws<NotImplementedException>(() => act());
        }

        [Fact]
        public void MaterijalUpdate_PravilnoPozvano_VracaIznimku() {
            // Arrange
            kreirajServis();
            Materijal materijal = new Materijal();
            MaterijalRepository repo = new MaterijalRepository();

            Action act = () => repo.Update(materijal, false);

            // Assert
            Assert.Throws<NotImplementedException>(() => act());
        }

        [Fact]
        public void UslugaUpdate_PravilnoPozvano_VracaIznimku() {
            // Arrange
            kreirajServis();
            Usluga usluga = new Usluga();
            UslugaRepository repo = new UslugaRepository();

            Action act = () => repo.Update(usluga, false);

            // Assert
            Assert.Throws<NotImplementedException>(() => act());
        }


        [Fact]
        public void Pretrazi_PretraziKlijentaPoNazivu_VracaKlijenta()
        {
            //Arrange
            kreirajServis();
            var klijenti = _klijentServices.DohvatiKlijente();
            var klijent = klijenti.SingleOrDefault(k => k.Naziv == "Aggreko");

            //Act
            var pretrazeniKlijenti = _klijentServices.Pretrazi("Aggreko");

            //Assert
            Assert.NotNull(pretrazeniKlijenti);
            Assert.Equal(1, pretrazeniKlijenti.Count);

        }

        [Fact]
        public void Pretrazi_KaoParametarNemojProslijeditiNista_VracaPopisSvihKlijenata()
        {
            //Arrange
            kreirajServis();
            var klijenti = _klijentServices.DohvatiKlijente();

            //Act
            var pretrazeniKlijenti = _klijentServices.Pretrazi("");

            //Assert
            Assert.NotNull(pretrazeniKlijenti);
            Assert.Equal(klijenti, pretrazeniKlijenti);
        }

        [Fact]
        public void Pretrazi_KaoParametarProslijediStringSNazivomKojiNePostojiUBazi_Vrati0Klijenata()
        {
            //Arrange
            kreirajServis();
            var klijenti = _klijentServices.DohvatiKlijente();

            //Act
            var pretrazeniKlijenti = _klijentServices.Pretrazi("sakskkdaskksa");

            //Assert
            Assert.NotNull(pretrazeniKlijenti);
            Assert.Equal(0, pretrazeniKlijenti.Count);
        }

        [Fact]
        public void SortirajKlijentePoUkupnomBrojuRacuna_PostojeKlijentiUBazi_VratiSortiraneKlijentePoUkupnomBrojuRacuna()
        {
            //Arrange
            kreirajServis();

            //Act
            var sortiraniKlijenti = _klijentServices.SortirajKlijentePoUkupnomBrojuRacuna();
            //Assert
            Assert.IsType<List<Klijent>>(sortiraniKlijenti);
            Assert.True(sortiraniKlijenti[0].Naziv == "Aggreko" && sortiraniKlijenti[1].Naziv == "Zvonimir Belina" && sortiraniKlijenti[2].Naziv == "Sebastijan Bicak");
        }
    }
}
