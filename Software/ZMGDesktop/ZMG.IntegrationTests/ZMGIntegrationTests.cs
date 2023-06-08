﻿using BusinessLogicLayer.Services;
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

        private void kreirajServis()
        {
            _klijentServices = new KlijentServices(new KlijentRepository());
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
            var klijent = klijenti.SingleOrDefault(k => k.Naziv == "Adriatic");
            int brojKlijenata = klijenti.Count;

            //Act
            _klijentServices.Remove(klijent);
            

            //Assert
            Assert.True(brojKlijenata != _klijentServices.DohvatiKlijente().Count);
            _klijentServices.Add(klijent);
        }

        [Fact]

        /*
         Ovaj test nakon što se pokrene, potrebno je pokrenuti aplikaciju i obrisati klijenta "Toni". Ako se to neće napraviti i ako 
        pokušamo ponovno pokrenuti test, on neće proći iz razloga jer taj klijent već postoji u bazi i ne možemo dodati dva ista klijenta.
         */
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
            var radniNalogServis = new RadniNalogService(new RadniNalogRepository());
            var klijenti = _klijentServices.DohvatiKlijente();
            var klijent = klijenti.SingleOrDefault(k => k.Naziv == "Impuls");

            //Act
            int racuni = racunServis.DohvatiRacuneZaKlijenta(klijent).Count;
            int radniNalozi = radniNalogServis.DohvatiRadneNalogeZaKlijenta(klijent).Count;

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
    }
}
