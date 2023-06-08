using BusinessLogicLayer.PDF;
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
        private UslugaServices UslugaServices;
        private PoslodavacServices PoslodavacServices;
        private RacunService RacunService;
        private StavkaRacunService StavkaRacunService;

        private void kreirajServis()
        {
            _klijentServices = new KlijentServices(new KlijentRepository());
            RadniNalogService = new RadniNalogService(new RadniNalogRepository());
            RadnikServices = new RadnikServices(new RadnikRepository());
            RobaService = new RobaService(new RobaRepository());
            UslugaServices = new UslugaServices(new UslugaRepository());
            PoslodavacServices = new PoslodavacServices(new PoslodavacRepository());
            RacunService = new RacunService(new RacunRepository());
            StavkaRacunService = new StavkaRacunService(new StavkaRepository());

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

            var azuriraniRadniNalog = new RadniNalog {
                RadniNalog_ID = 2060,
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

        //sbicak20
        [Fact]
        public void SacuvajPDF_ProsljedenRacunIJednaStavkaUListi_GeneriraniPDF()
        {
            //arrage
            kreirajServis();

            var radnik = RadnikServices.DohvatiSveRadnike().FirstOrDefault(r => r.Radnik_ID == 24);
            var poslodavac = PoslodavacServices.GetPoslodavac();
            var klijent = _klijentServices.DohvatiKlijente().FirstOrDefault(k => k.Klijent_ID == 150);

            List<StavkaRacun> lista = new List<StavkaRacun>
            {
                new StavkaRacun
                {
                    KolikoRobePoJedinici = 123,
                    KolicinaRobe = 123,
                    DatumIzrade = DateTime.Now,
                    JedinicaMjere = "kg",
                    JedinicnaCijena = 123,
                    UkupnaCijenaStavke = 123,
                    Usluga = UslugaServices.DohvatiUsluguPoNazivu("Cincanje"),
                    Roba = RobaService.DohvatiSvuRobu().FirstOrDefault()
                }
            };
            Racun racun = new Racun
            {
                Klijent = klijent,
                Poslodavac = poslodavac,
                Radnik = radnik,
                Fakturirao = "asddasf",
                Opis = "asddasf",
                NacinPlacanja = "asddasf",
                UkupnaCijena = 1.2,
                PDV = 1.2,
                UkupnoStavke = 3.4,
                DatumIzdavanja = DateTime.Now,
                StavkaRacun = lista,
                RokPlacanja = "asddasf"
            };


            //act
            int rezultat = GeneriranjePDF.SacuvajPDF(racun, lista);

            //assert
            Assert.Equal(1, rezultat);
        }
    }
}
