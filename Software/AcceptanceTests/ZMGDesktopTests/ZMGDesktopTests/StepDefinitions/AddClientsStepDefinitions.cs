using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class AddClientsStepDefinitions
    {


        [Given(@"Korisnik se nalazi na glavnom izborniku")]
        public void GivenKorisnikSeNalaziNaGlavnomIzborniku()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmPocetna") != null;
            bool title = driver.Title == "Glavni izbornik";
            Assert.IsTrue(isOpen);
        }

        [When(@"Korisnik klikne na gumb klijenti")]
        public void WhenKorisnikKlikneNaGumbKlijenti()
        {
            var driver = GuiDriver.GetDriver();
            var btnKlijenti = driver.FindElementByAccessibilityId("btnKlijenti");
            btnKlijenti.Click();
        }

        [Then(@"Korisniku se otvara forma za prikaz svih klijenata")]
        public void ThenKorisnikuSeOtvaraFormaZaPrikazSvihKlijenata()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isOpened = driver.FindElementByAccessibilityId("FrmPregledKlijenata") != null;
            //bool title = driver.Title == "Pregled klijenata";
            Assert.IsTrue(isOpened);
        }

        [Then(@"Korisnik klikne na gumb Dodaj klijenta")]
        public void ThenKorisnikKlikneNaGumbDodajKlijenta()
        {
            var driver = GuiDriver.GetDriver();
            var btnDodajKlijenta = driver.FindElementByAccessibilityId("btnDodajKlijenta");
            btnDodajKlijenta.Click();
        }

        [Then(@"Korisniku se otvara forma za dodavanje klijenta")]
        public void ThenKorisnikuSeOtvaraFormaZaDodavanjeKlijenta()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isOpened = driver.FindElementByAccessibilityId("FrmDodajKlijenta") != null;
            Assert.IsTrue(isOpened);
        }

        [Then(@"Korisnik unosi podatke za klijenta: Naziv = ""([^""]*)"", OIB = ""([^""]*)"", Adresa = ""([^""]*)"", IBAN = ""([^""]*)"", Mjesto =""([^""]*)"", Broj telefona = ""([^""]*)"", Email = ""([^""]*)""")]
        public void ThenKorisnikUnosiPodatkeZaKlijentaNazivOIBAdresaIBANMjestoBrojTelefonaEmail(string naziv, string oib, string adresa, string iban, string mjesto, string telefon, string email)
        {
            var driver = GuiDriver.GetDriver();
            var txtNaziv = driver.FindElementByAccessibilityId("txtNaziv");
            var txtOIB = driver.FindElementByAccessibilityId("txtOIB");
            var txtAdresa = driver.FindElementByAccessibilityId("txtAdresa");
            var txtIBAN = driver.FindElementByAccessibilityId("txtIBAN");
            var txtMjesto = driver.FindElementByAccessibilityId("txtMjesto");
            var txtTelefon = driver.FindElementByAccessibilityId("txtTelefon");
            var txtMail = driver.FindElementByAccessibilityId("txtEmail");
            
            txtNaziv.SendKeys(naziv);
            txtOIB.SendKeys(oib);
            txtAdresa.SendKeys(adresa);
            txtIBAN.SendKeys(iban);
            txtMjesto.SendKeys(mjesto);
            txtTelefon.SendKeys(telefon);
            txtMail.SendKeys(email);
        }

        [Then(@"Korisnik unosi podatke za klijenta: OIB = ""([^""]*)"", Adresa = ""([^""]*)"", IBAN = ""([^""]*)"", Mjesto =""([^""]*)"", Broj telefona = ""([^""]*)"", Email = ""([^""]*)""")]
        public void ThenKorisnikUnosiPodatkeZaKlijentaOIBAdresaIBANMjestoBrojTelefonaEmail(string oib, string adresa, string iban, string mjesto, string telefon, string email)
        {
            var driver = GuiDriver.GetDriver();
            var txtOIB = driver.FindElementByAccessibilityId("txtOIB");
            var txtAdresa = driver.FindElementByAccessibilityId("txtAdresa");
            var txtIBAN = driver.FindElementByAccessibilityId("txtIBAN");
            var txtMjesto = driver.FindElementByAccessibilityId("txtMjesto");
            var txtTelefon = driver.FindElementByAccessibilityId("txtTelefon");
            var txtMail = driver.FindElementByAccessibilityId("txtEmail");

            txtOIB.SendKeys(oib);
            txtAdresa.SendKeys(adresa);
            txtIBAN.SendKeys(iban);
            txtMjesto.SendKeys(mjesto);
            txtTelefon.SendKeys(telefon);
            txtMail.SendKeys(email);
        }


        [Then(@"Korisnik klikne na gumb Spremi")]
        public void ThenKorisnikKlikneNaGumbSpremi()
        {
            var driver = GuiDriver.GetDriver();
            var btnSpremi = driver.FindElementByAccessibilityId("btnSpremi");
            btnSpremi.Click();
        }

        [Then(@"Klijent uspješno dodan u bazu podataka")]
        public void ThenKlijentUspjesnoDodanUBazuPodataka()
        {
            var driver = GuiDriver.GetDriver();
            var dgvKlijenti = driver.FindElementByAccessibilityId("dgvKlijenti");
            bool isOpened = driver.FindElementByAccessibilityId("FrmPregledKlijenata") != null;
            Assert.IsTrue(isOpened);
        }

        [Then(@"Prikazuje se poruka ""([^""]*)""")]
        public void ThenPrikazujeSePoruka(string poruka)
        {
            var driver = GuiDriver.GetDriver();

            var messageBox = driver.FindElementByAccessibilityId("65535");
            Assert.IsTrue(poruka == messageBox.Text);
        }


        [AfterScenario]
        public void CloseApplication()
        {
            GuiDriver.Dispose();
        }
    }
}
