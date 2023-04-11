using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Data;
using System.Linq;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class UpravljanjeStavkamaStepDefinitions
    {
        [Given(@"Korisniku se otvara forma za upravljanje stavkama")]
        public void ThenKorisnikuSeOtvaraFormaZaUpravljanjeStavkama()
        {
            var driver = GuiDriverAppOpen.GetOrCreateDriver();
            bool isOpened = driver.FindElementByAccessibilityId("FrmDodajStavke") != null;
            Assert.IsTrue(isOpened);
        }

        [When(@"Korisnik klikne na gumb Dodaj")]
        public void WhenKorisnikKlikneNaGumbDodaj()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var btnUpravljanjeStavkama = driver.FindElementByAccessibilityId("btnDodaj");
            btnUpravljanjeStavkama.Click();
        }

        [When(@"Korisnik unese krive podatke")]
        public void WhenKorisnikUneseOdredeneKrivePodatke()
        {
            var podatak1 = "13";
            var podatak2 = "0.93";
            var podatak3 = "5";

            var driver = GuiDriverAppOpen.GetDriver();
            var txtKolikoRobePoJedinici = driver.FindElementByAccessibilityId("txtKolikoRobePoJedinici");
            var txtKolicina = driver.FindElementByAccessibilityId("txtKolicina");
            var txtJedinicnaCijena = driver.FindElementByAccessibilityId("txtJedinicnaCijena");

            txtKolikoRobePoJedinici.SendKeys(podatak1);
            txtKolicina.SendKeys(podatak2);
            txtJedinicnaCijena.SendKeys(podatak3);
        }

        [When(@"Korisnik unese tocne podatke")]
        public void WhenKorisnikUneseTocnePodatke()
        {
            var podatak1 = "13";
            var podatak2 = "10";
            var podatak3 = "5";

            var driver = GuiDriverAppOpen.GetDriver();
            var txtKolikoRobePoJedinici = driver.FindElementByAccessibilityId("txtKolikoRobePoJedinici");
            var txtKolicina = driver.FindElementByAccessibilityId("txtKolicina");
            var txtJedinicnaCijena = driver.FindElementByAccessibilityId("txtJedinicnaCijena");

            txtKolikoRobePoJedinici.Clear();
            txtKolicina.Clear();
            txtJedinicnaCijena.Clear();

            txtKolikoRobePoJedinici.ClearCache();
            txtKolicina.ClearCache();
            txtJedinicnaCijena.ClearCache();

            txtKolikoRobePoJedinici.SendKeys(podatak1);
            txtKolicina.SendKeys(podatak2);
            txtJedinicnaCijena.SendKeys(podatak3);
        }

        [When(@"Korinsik ponovno unese tocne podatke")]

        public void WhenKorisnikPonovnoUneseTocnePodatke()
        {
            var podatak1 = "13";
            var podatak2 = "10";
            var podatak3 = "5";

            var driver = GuiDriverAppOpen.GetDriver();
            var txtKolikoRobePoJedinici = driver.FindElementByAccessibilityId("txtKolikoRobePoJedinici");
            var txtKolicina = driver.FindElementByAccessibilityId("txtKolicina");
            var txtJedinicnaCijena = driver.FindElementByAccessibilityId("txtJedinicnaCijena");

            txtKolikoRobePoJedinici.Clear();
            txtKolicina.Clear();
            txtJedinicnaCijena.Clear();

            txtKolikoRobePoJedinici.ClearCache();
            txtKolicina.ClearCache();
            txtJedinicnaCijena.ClearCache();

            txtKolikoRobePoJedinici.SendKeys(podatak1);
            txtKolicina.SendKeys(podatak2);
            txtJedinicnaCijena.SendKeys(podatak3);
        }

        [When(@"Korisnik oznaci stavku")]
        public void WhenKorisnikZnaciStavku()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var dgvStavkeDodaj = driver.FindElementByAccessibilityId("dgvStavkeDodaj");
            dgvStavkeDodaj.Click();
            dgvStavkeDodaj.SendKeys(Keys.Up);
        }

        [Then(@"Korisniku se izbacuje poruka sustava za unos stavke")]
        public void ThenKorisnikuSeIzbacujePorukaSustavaZaUnosStavke()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var greska = driver.FindElementByClassName("#32770") != null;
            Assert.IsTrue(greska);
        }

        [Then(@"Korisnik klikne na gumb Ok")]
        public void ThenKorisnikKlikneNaGumbOk()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var btnGreskaOK = driver.FindElementByAccessibilityId("2");
            btnGreskaOK.Click();
        }

        [Then(@"Korisnik klikne na gumb Dodaj")]
        public void ThenKorisnikKlikneNaGumbDodaj()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var btnUpravljanjeStavkama = driver.FindElementByAccessibilityId("btnDodaj");
            btnUpravljanjeStavkama.Click();
        }

        [Then(@"Korisnik klikne na gumb Obrisi")]

        public void ThenKorisnikKlikneNaGumbObrisi()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var btnObrisi = driver.FindElementByAccessibilityId("btnObrisi");
            btnObrisi.Click();
        }

        [Then(@"Korisnik mijenja uslugu")]
        public void ThenKorisnikMijenjaUslugu()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var cmbUsluge = driver.FindElementByAccessibilityId("cmbUsluge");
            cmbUsluge.Click();
            cmbUsluge.SendKeys(Keys.Down);
        }

        [AfterScenario]
        public void CloseApplication()
        {
            GuiDriver.Dispose();
        }
    }
}
