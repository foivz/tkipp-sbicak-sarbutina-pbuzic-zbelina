using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class DeleteClientStepDefinitions
    {

        [Given(@"Korisnik je na glavnom izborniku")]
        public void GivenKorisnikJeNaGlavnomIzborniku()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmPocetna") != null;
            bool title = driver.Title == "Glavni izbornik";
            Assert.IsTrue(isOpen);
        }

        [When(@"Korisnik klikne gumb klijenti")]
        public void WhenKorisnikKlikneGumbKlijenti()
        {
            var driver = GuiDriver.GetDriver();
            var btnKlijenti = driver.FindElementByAccessibilityId("btnKlijenti");
            btnKlijenti.Click();
        }

        [Then(@"Korisniku se prikazuje popis svih klijenata")]
        public void ThenKorisnikuSePrikazujePopisSvihKlijenata()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            bool isOpened = driver.FindElementByAccessibilityId("FrmPregledKlijenata") != null;
            //bool title = driver.Title == "Pregled klijenata";
            Assert.IsTrue(isOpened);
        }

        [Then(@"Korisnik odabere klijenta ""([^""]*)""")]
        public void ThenKorisnikOdabereKlijenta(string casa)
        {
            var driver = GuiDriver.GetDriver();
            var value = driver.FindElementByName("Naziv Row 13, Not sorted.");
            value.Click();
        }

        [Then(@"Korisnik klikne na gumb Izbrisi klijenta")]
        public void ThenKorisnikKlikneNaGumbIzbrisiKlijenta()
        {
            var driver = GuiDriver.GetDriver();
            var btnIzbrisi = driver.FindElementByAccessibilityId("btnIzbrisiKlijenta");
            btnIzbrisi.Click();
        }

        [Then(@"Klijent je uspješno obrisan")]
        public void ThenKlijentJeUspjesnoObrisan()
        {
            var driver = GuiDriver.GetDriver();
            bool isOpened = driver.FindElementByAccessibilityId("FrmPregledKlijenata") != null;
            Assert.IsTrue(isOpened);
        }

        [Then(@"Prikazuje se poruka: ""([^""]*)""")]
        public void ThenPrikazujeSePoruka(string p0)
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            var tekst = driver.FindElementByAccessibilityId("65535");
            Assert.IsTrue(tekst.Text == p0);
        }

        [AfterFeature]
        public static void CloseApplication()
        {
            GuiDriver.Dispose();
        }
    }
}
