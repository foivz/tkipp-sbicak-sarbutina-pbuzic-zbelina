﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class DodavanjeRadnogNalogaStepDefinitions
    {
        [Given(@"Korisnik se nalazi na početnom izborniku")]
        public void GivenKorisnikSeNalaziNaPocetnomIzborniku()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmPocetna") != null;
            bool title = driver.Title == "Glavni izbornik";
            Assert.IsTrue(isOpen);
        }

        [When(@"Korisnik klikne na gumb radni nalozi")]
        public void WhenKorisnikKlikneNaGumbRadniNalozi()
        {
            var driver = GuiDriver.GetDriver();
            var btnRadniNalozi = driver.FindElementByAccessibilityId("btnRadniNalozi");
            btnRadniNalozi.Click();
        }

        [Then(@"Korisniku se otvara forma za prikaz svih radnih naloga")]
        public void ThenKorisnikuSeOtvaraFormaZaPrikazSvihRadnihNaloga()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isOpened = driver.FindElementByAccessibilityId("FrmPopisRadnihNaloga") != null;
            Assert.IsTrue(isOpened);
        }

        [Then(@"Korisnik klikne na gumb novi radni nalog")]
        public void ThenKorisnikKlikneNaGumbNoviRadniNalog()
        {
            var driver = GuiDriver.GetDriver();
            var btnNoviRadniNalog = driver.FindElementByAccessibilityId("btnNoviRadniNalog");
            btnNoviRadniNalog.Click();
        }

        [Then(@"Korisniku se otvara forma za dodavanje novog radnog naloga")]
        public void ThenKorisnikuSeOtvaraFormaZaDodavanjeNovogRadnogNaloga()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isOpened = driver.FindElementByAccessibilityId("FrmNoviRadniNalog") != null;
            Assert.IsTrue(isOpened);
        }

        [Then(@"Korisnik redom unosi podatke za radni nalog: Količina = ""([^""]*)"", Opis = ""([^""]*)"", odabire Status = ""([^""]*)"", odabire Datum stvaranja = ""([^""]*)"", odabire Klijent = ""([^""]*)"", odabire Materijal = ""([^""]*)"" i dodaje ga u tablicu, odabire robu ""([^""]*)"" iz tablice klijentove robe te ju dodaje u tablicu robe radnog naloga, klikće na gumb generiraj QR kod")]
        public void ThenKorisnikRedomUnosiPodatkeZaRadniNalogKolicinaOpisOdabireStatusOdabireDatumStvaranjaOdabireKlijentOdabireMaterijalIDodajeGaUTablicuOdabireRobuIzTabliceKlijentoveRobeTeJuDodajeUTablicuRobeRadnogNalogaKlikceNaGumbGenerirajQRKod(string kolicina, string opis, string status, string datum_stvaranja, string klijent, string materijal, string roba)
        {
            var driver = GuiDriver.GetDriver();
            var txtKolicina = driver.FindElementByAccessibilityId("txtKolicina");
            var txtOpis = driver.FindElementByAccessibilityId("txtOpis");
            var cmbStatus = driver.FindElementByAccessibilityId("cmbStatus");
            var cmbKlijent = driver.FindElementByAccessibilityId("cmbKlijent");
            var dtpDatumStvaranja = driver.FindElementByAccessibilityId("dtpDatumStvaranja");
            var roba1 = driver.FindElementByAccessibilityId("4233047401");
            var roba2 = driver.FindElementByAccessibilityId("4224437140");

            var btnDodajMaterijal = driver.FindElementByAccessibilityId("btnDodajMaterijal");
            var btnDodajRobuNaRadniNalog = driver.FindElementByAccessibilityId("btnDodajRobuNaRadniNalog");
            var btnGenerirajQRKod = driver.FindElementByAccessibilityId("btnGenerirajQRKod");

            txtKolicina.SendKeys(kolicina);
            txtOpis.SendKeys(opis);
            cmbStatus.Click();
            var statusNapravljen = cmbStatus.FindElementByName(status);
            statusNapravljen.Click();

            dtpDatumStvaranja.SendKeys(DateTime.Parse(datum_stvaranja).ToString());
            cmbKlijent.SendKeys(klijent);
            btnDodajMaterijal.Click();
            roba1.Click();
            btnDodajRobuNaRadniNalog.Click();
            roba2.Click();
            btnDodajRobuNaRadniNalog.Click();
            btnGenerirajQRKod.Click();
        }

        [Then(@"Korisnik klikne na gumb Podnesi")]
        public void ThenKorisnikKlikneNaGumbPodnesi()
        {
            var driver = GuiDriver.GetDriver();
            var btnPodnesi = driver.FindElementByAccessibilityId("btnPodnesi");
            btnPodnesi.Click();
        }

        [Then(@"Radni nalog uspješno dodan u bazu podataka")]
        public void ThenRadniNalogUspjesnoDodanUBazuPodataka()
        {
            var driver = GuiDriver.GetDriver();
            var dgvKlijenti = driver.FindElementByAccessibilityId("dgvPopisRadnihNaloga");
            bool isOpened = driver.FindElementByAccessibilityId("FrmPopisRadnihNaloga") != null;
            Assert.IsTrue(isOpened);
        }

        [Then(@"Korisnik redom unosi podatke za radni nalog: Opis = ""([^""]*)"", odabire Status = ""([^""]*)"", odabire Datum stvaranja = ""([^""]*)"", odabire Klijent = ""([^""]*)"", odabire Materijal = ""([^""]*)"" i dodaje ga u tablicu, odabire robu ""([^""]*)"" iz tablice klijentove robe te ju dodaje u tablicu robe radnog naloga, klikće na gumb generiraj QR kod")]
        public void ThenKorisnikRedomUnosiPodatkeZaRadniNalogOpisOdabireStatusOdabireDatumStvaranjaOdabireKlijentOdabireMaterijalIDodajeGaUTablicuOdabireRobuIzTabliceKlijentoveRobeTeJuDodajeUTablicuRobeRadnogNalogaKlikceNaGumbGenerirajQRKod(string opis, string status, string datum_stvaranja, string klijent, string materijal, string roba)
        {
            var driver = GuiDriver.GetDriver();
            var txtOpis = driver.FindElementByAccessibilityId("txtOpis");
            var cmbStatus = driver.FindElementByAccessibilityId("cmbStatus");
            var cmbKlijent = driver.FindElementByAccessibilityId("cmbKlijent");
            var dtpDatumStvaranja = driver.FindElementByAccessibilityId("dtpDatumStvaranja");
            var cmbMaterijal = driver.FindElementByAccessibilityId("cmbMaterijal");
            var roba1 = driver.FindElementByAccessibilityId("4233047401");
            var roba2 = driver.FindElementByAccessibilityId("4224437140");

            var btnDodajMaterijal = driver.FindElementByAccessibilityId("btnDodajMaterijal");
            var btnDodajRobuNaRadniNalog = driver.FindElementByAccessibilityId("btnDodajRobuNaRadniNalog");
            var btnGenerirajQRKod = driver.FindElementByAccessibilityId("btnGenerirajQRKod");

            txtOpis.SendKeys(opis);
            cmbStatus.SendKeys(status);
            dtpDatumStvaranja.SendKeys(DateTime.Parse(datum_stvaranja).ToString());
            cmbKlijent.SendKeys(klijent);
            cmbMaterijal.SendKeys(materijal);
            btnDodajMaterijal.Click();
            roba1.Click();
            btnDodajRobuNaRadniNalog.Click();
            roba2.Click();
            btnDodajRobuNaRadniNalog.Click();
            btnGenerirajQRKod.Click();
        }

        [Then(@"Korisnik redom unosi podatke za radni nalog: Količina = ""([^""]*)"", Opis = ""([^""]*)"", odabire Datum stvaranja = ""([^""]*)"", odabire Klijent = ""([^""]*)"", odabire Materijal = ""([^""]*)"" i dodaje ga u tablicu, odabire robu ""([^""]*)"" iz tablice klijentove robe te ju dodaje u tablicu robe radnog naloga, klikće na gumb generiraj QR kod")]
        public void ThenKorisnikRedomUnosiPodatkeZaRadniNalogKolicinaOpisOdabireDatumStvaranjaOdabireKlijentOdabireMaterijalIDodajeGaUTablicuOdabireRobuIzTabliceKlijentoveRobeTeJuDodajeUTablicuRobeRadnogNalogaKlikceNaGumbGenerirajQRKod(string kolicina, string opis, string datum_stvaranja, string klijent, string materijal, string roba)
        {
            var driver = GuiDriver.GetDriver();
            var txtKolicina = driver.FindElementByAccessibilityId("txtKolicina");
            var txtOpis = driver.FindElementByAccessibilityId("txtOpis");
            var cmbKlijent = driver.FindElementByAccessibilityId("cmbKlijent");
            var dtpDatumStvaranja = driver.FindElementByAccessibilityId("dtpDatumStvaranja");
            var cmbMaterijal = driver.FindElementByAccessibilityId("cmbMaterijal");
            var roba1 = driver.FindElementByAccessibilityId("4233047401");
            var roba2 = driver.FindElementByAccessibilityId("4224437140");

            var btnDodajMaterijal = driver.FindElementByAccessibilityId("btnDodajMaterijal");
            var btnDodajRobuNaRadniNalog = driver.FindElementByAccessibilityId("btnDodajRobuNaRadniNalog");
            var btnGenerirajQRKod = driver.FindElementByAccessibilityId("btnGenerirajQRKod");

            txtKolicina.SendKeys(kolicina);
            txtOpis.SendKeys(opis);
            dtpDatumStvaranja.SendKeys(DateTime.Parse(datum_stvaranja).ToString());
            cmbKlijent.SendKeys(klijent);
            cmbMaterijal.SendKeys(materijal);
            btnDodajMaterijal.Click();
            roba1.Click();
            btnDodajRobuNaRadniNalog.Click();
            roba2.Click();
            btnDodajRobuNaRadniNalog.Click();
            btnGenerirajQRKod.Click();
        }

        [Then(@"Prikazivanje poruke ""([^""]*)""")]
        public void ThenPrikazivanjePoruke(string poruka)
        {
            var driver = GuiDriver.GetDriver();

            var messageBox = driver.FindElementByAccessibilityId("65535");
            Assert.IsTrue(poruka == messageBox.Text);
        }

        [Then(@"Korisnik redom unosi podatke za radni nalog: Količina = ""([^""]*)"", Opis = ""([^""]*)"", odabire Status = ""([^""]*)"", odabire Datum stvaranja = ""([^""]*)"", odabire Klijent = ""([^""]*)"", odabire robu ""([^""]*)"" iz tablice klijentove robe te ju dodaje u tablicu robe radnog naloga, klikće na gumb generiraj QR kod")]
        public void ThenKorisnikRedomUnosiPodatkeZaRadniNalogKolicinaOpisOdabireStatusOdabireDatumStvaranjaOdabireKlijentOdabireRobuIzTabliceKlijentoveRobeTeJuDodajeUTablicuRobeRadnogNalogaKlikceNaGumbGenerirajQRKod(string kolicina, string opis, string status, string datum_stvaranja, string klijent, string roba)
        {
            var driver = GuiDriver.GetDriver();
            var txtKolicina = driver.FindElementByAccessibilityId("txtKolicina");
            var txtOpis = driver.FindElementByAccessibilityId("txtOpis");
            var cmbStatus = driver.FindElementByAccessibilityId("cmbStatus");
            var cmbKlijent = driver.FindElementByAccessibilityId("cmbKlijent");
            var dtpDatumStvaranja = driver.FindElementByAccessibilityId("dtpDatumStvaranja");
            var roba1 = driver.FindElementByAccessibilityId("4233047401");
            var roba2 = driver.FindElementByAccessibilityId("4224437140");

            var btnDodajRobuNaRadniNalog = driver.FindElementByAccessibilityId("btnDodajRobuNaRadniNalog");
            var btnGenerirajQRKod = driver.FindElementByAccessibilityId("btnGenerirajQRKod");

            txtKolicina.SendKeys(kolicina);
            txtOpis.SendKeys(opis);
            cmbStatus.SendKeys(status);
            dtpDatumStvaranja.SendKeys(DateTime.Parse(datum_stvaranja).ToString());
            cmbKlijent.SendKeys(klijent);
            roba1.Click();
            btnDodajRobuNaRadniNalog.Click();
            roba2.Click();
            btnDodajRobuNaRadniNalog.Click();
            btnGenerirajQRKod.Click();
        }

        [Then(@"Korisnik redom unosi podatke za radni nalog: Količina = ""([^""]*)"", Opis = ""([^""]*)"", odabire Status = ""([^""]*)"", odabire Datum stvaranja = ""([^""]*)"", odabire Klijent = ""([^""]*)"", odabire Materijal = ""([^""]*)"" i dodaje ga u tablicu, klikće na gumb generiraj QR kod")]
        public void ThenKorisnikRedomUnosiPodatkeZaRadniNalogKolicinaOpisOdabireStatusOdabireDatumStvaranjaOdabireKlijentOdabireMaterijalIDodajeGaUTablicuKlikceNaGumbGenerirajQRKod(string kolicina, string opis, string status, string datum_stvaranja, string klijent, string materijal)
        {
            var driver = GuiDriver.GetDriver();
            var txtKolicina = driver.FindElementByAccessibilityId("txtKolicina");
            var txtOpis = driver.FindElementByAccessibilityId("txtOpis");
            var cmbStatus = driver.FindElementByAccessibilityId("cmbStatus");
            var cmbKlijent = driver.FindElementByAccessibilityId("cmbKlijent");
            var dtpDatumStvaranja = driver.FindElementByAccessibilityId("dtpDatumStvaranja");
            var cmbMaterijal = driver.FindElementByAccessibilityId("cmbMaterijal");

            var btnDodajMaterijal = driver.FindElementByAccessibilityId("btnDodajMaterijal");
            var btnGenerirajQRKod = driver.FindElementByAccessibilityId("btnGenerirajQRKod");

            txtKolicina.SendKeys(kolicina);
            txtOpis.SendKeys(opis);
            cmbStatus.SendKeys(status);
            dtpDatumStvaranja.SendKeys(DateTime.Parse(datum_stvaranja).ToString());
            cmbKlijent.SendKeys(klijent);
            cmbMaterijal.SendKeys(materijal);
            btnDodajMaterijal.Click();
            btnGenerirajQRKod.Click();
        }

        [Then(@"Korisnik klikne na gumb dodaj novu robu bez upisivanja informacija o robi")]
        public void ThenKorisnikKlikneNaGumbDodajNovuRobuBezUpisivanjaInformacijaORobi()
        {
            var driver = GuiDriver.GetDriver();
            var btnDodajRobuNaRadniNalog = driver.FindElementByAccessibilityId("btnDodajRobuNaRadniNalog");
            btnDodajRobuNaRadniNalog.Click();
        }

        [AfterScenario]
        public void CloseApplication()
        {
            GuiDriver.Dispose();
        }
    }
}
