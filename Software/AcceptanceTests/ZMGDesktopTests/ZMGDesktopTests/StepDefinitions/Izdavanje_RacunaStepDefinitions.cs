using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class Izdavanje_RacunaStepDefinitions
    {
        [Given(@"Korisnik se nalazi na formi za izdavanje racuna")]
        public void GivenKorisnikSeNalaziNaFormiZaIzdavanjeRacuna()
        {
            var driver = GuiDriverAppOpen.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmIzdajNoviRacun") != null;
            Assert.IsTrue(isOpen);
        }

        [When(@"Korisnik klikne na gumb Izdaj racun")]
        public void WhenKorisnikKlikneNaGumbIzdajRacun()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var btnIzdajRacun = driver.FindElementByAccessibilityId("btnIzdajRacun");
            btnIzdajRacun.Click();
        }

        [Then(@"Korisniku se izbacuje poruka sustava za nemogucnost izdavanja racuna")]
        public void ThenKorisnikuSeIzbacujePorukaSustavaZaNemogucnostIzdavanjaRacuna()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            var greska = driver.FindElementByAccessibilityId("65535") != null;
            Assert.IsTrue(greska);
        }

        [Then(@"Korisnik klikne na gumb Ok1")]
        public void ThenKorisnikKlikneNaGumbOk()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var btnGreskaOK = driver.FindElementByAccessibilityId("2");
            btnGreskaOK.Click();
            driver.SwitchTo().Window(driver.WindowHandles.First());
        }

        [Then(@"Korisnik klikne na gumb Natrag")]
        public void ThenKorisnikKlikneNaGumbNatrag()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var FrmUpravljanjeStavkama = driver.FindElementByAccessibilityId("FrmDodajStavke");
            var btnNatrag = FrmUpravljanjeStavkama.FindElementByAccessibilityId("btnNatrag");
            btnNatrag.Click();
        }

        [Then(@"Nakon unosa dvije stavke provjeriti iznos racuna")]
        public void ThenNakonUnosaDvijeStavkeProvjeritiIznosRacuna()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var txtUkupniIznos = driver.FindElementByAccessibilityId("txtUkupniIznos").Text;
            Assert.IsTrue(txtUkupniIznos == "162.5");
        }

        [Then(@"Korisnik klikne na gumb Dodaj1")]
        public void ThenKorisnikKlikneNaGumbDodaj()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var btnDodaj = driver.FindElementByAccessibilityId("btnDodaj");
            btnDodaj.Click();
        }

        [Then(@"Korisniku se otvara forma za upravljanje stavkama")]
        public void ThenKorisnikuSeOtvaraFormaZaUpravljanjeStavkama()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmDodajStavke") != null;
            Assert.IsTrue(isOpen);
        }

        [Then(@"Korisnik promijeni robu")]
        public void ThenKorisnikPromijeniRobu()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var cmbRoba = driver.FindElementByAccessibilityId("cmbRoba");
            cmbRoba.Click();
            cmbRoba.SendKeys(Keys.Down);
        }

        [When(@"Korisnik klikne na gumb Upravljanje stavkama")]
        public void WhenKorisnikKlikneNaGumbUpravljanjeStavkama()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var btnDodajStavke = driver.FindElementByAccessibilityId("btnDodajStavke");
            btnDodajStavke.Click();
        }

        [Then(@"Korisnik unese potrebne podatke")]
        public void ThenKorisnikUnesePodatke()
        {
            var podatak1 = "13";
            var podatak2 = "10";
            var podatak3 = "5";

            var driver = GuiDriverAppOpen.GetDriver();
            var txtKolikoRobePoJedinici = driver.FindElementByAccessibilityId("txtKolikoRobePoJedinici");
            var txtKolicina = driver.FindElementByAccessibilityId("txtKolicina");
            var txtJedinicnaCijena = driver.FindElementByAccessibilityId("txtJedinicnaCijena");

            txtKolikoRobePoJedinici.SendKeys(podatak1);
            txtKolicina.SendKeys(podatak2);
            txtJedinicnaCijena.SendKeys(podatak3);
        }
    }
}
