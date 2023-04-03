using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;
using OpenQA.Selenium.Appium;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class ImportClientsFromXMLFileStepDefinitions
    {
        [Given(@"Korisniku se otvara glavni izbornik")]
        public void GivenKorisnikuSeOtvaraGlavniIzbornik()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmPocetna") != null;
            bool title = driver.Title == "Glavni izbornik";
            Assert.IsTrue(isOpen);
        }

        [When(@"Korisnik izabere gumb klijenti")]
        public void WhenKorisnikIzabereGumbKlijenti()
        {
            var driver = GuiDriver.GetDriver();
            var btnKlijenti = driver.FindElementByAccessibilityId("btnKlijenti");
            btnKlijenti.Click();
        }

        [Then(@"Korisniku se otvara forma za popis svih klijenata")]
        public void ThenKorisnikuSeOtvaraFormaZaPopisSvihKlijenata()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isOpened = driver.FindElementByAccessibilityId("FrmPregledKlijenata") != null;
            //bool title = driver.Title == "Pregled klijenata";
            Assert.IsTrue(isOpened);
        }

        [Then(@"Korisnik klikne na gumb Uvezi klijenta \(XML\)")]
        public void ThenKorisnikKlikneNaGumbUveziKlijentaXML()
        {
            var driver = GuiDriver.GetDriver();
            var btnUveziKlijenta = driver.FindElementByAccessibilityId("btnUveziKlijenta");
            btnUveziKlijenta.Click();
        }

        [Then(@"Korisniku se otvara forma za odabir datoteke za uvoz klijenta")]
        public void ThenKorisnikuSeOtvaraFormaZaOdabirDatotekeZaUvozKlijenta()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            bool isOpened = driver.FindElementByAccessibilityId("FrmXML") != null;
            Assert.IsTrue(isOpened);
        }

        [Then(@"Korisnik klikne na gumb Odaberi")]
        public void ThenKorisnikKlikneNaGumbOdaberi()
        {
            var driver = GuiDriver.GetDriver();
            var btnOdaberi = driver.FindElementByAccessibilityId("btnOdaberi");
            btnOdaberi.Click();
        }

        [Then(@"Korisniku se otvara forma gdje odabire datoteku pod nazivom ""([^""]*)""")]
        public void ThenKorisnikuSeOtvaraFormaGdjeOdabireDatotekuPodNazivom(string naziv)
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            bool isOpened = driver.FindElementByClassName("#32770") != null;
            Assert.IsTrue(isOpened);
            var txtfileName = driver.FindElementByAccessibilityId("1148");
            txtfileName.SendKeys(naziv);
            var btnOpen = driver.FindElementByAccessibilityId("1");
            btnOpen.Click();
        }


        [Then(@"Korisniku se otvara prozor koji ispisuje putanju do datoteke gdje se klikne na gumb OK")]
        public void ThenKorisnikuSeOtvaraProzorKojiIspisujePutanjuDoDatotekeGdjeSeKlikneNaGumbOK()
        {
            var driver = GuiDriver.GetDriver();
            bool isOpened = driver.FindElementByClassName("#32770") != null;
            Assert.IsTrue(isOpened);
            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();
        }

        [Then(@"Korisnik klikne na gumb Unesi")]
        public void ThenKorisnikKlikneNaGumbUnesi()
        {
            var driver = GuiDriver.GetDriver();
            var btnUnesi = driver.FindElementByAccessibilityId("btnUnesi");
            btnUnesi.Click();
        }

        [Then(@"Prikazuje se poruka ""([^""]*)"" te su klijenti vidljivi na popisu svih klijenata")]
        public void ThenPrikazujeSePorukaTeSuKlijentiVidljiviNaPopisuSvihKlijenata(string poruka)
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            var dialogBox = driver.FindElementByAccessibilityId("65535");
            Assert.IsTrue(dialogBox.Text == poruka);
            var btnOk = driver.FindElementByAccessibilityId("2");
            Thread.Sleep(5000);
            btnOk.Click();
        }

        [Then(@"Korisniku se prikazuje greška")]
        public void ThenKorisnikuSePrikazujeGreska()
        {
            var driver = GuiDriver.GetDriver();
            var dialogBox = driver.FindElementByAccessibilityId("65535");
            Assert.IsTrue(dialogBox.Text != null);
            var btnOk = driver.FindElementByAccessibilityId("2");
            Thread.Sleep(2000);
            btnOk.Click();
        }


        [Then(@"Korisniku se prikazuje poruka ""([^""]*)""")]
        public void ThenKorisnikuSePrikazujePoruka(string poruka)
        {
            var driver = GuiDriver.GetDriver();
            var btnOk = driver.FindElementByAccessibilityId("2");
            var dialogText = driver.FindElementByAccessibilityId("65535");
            Assert.IsTrue(dialogText.Text == poruka);
            Thread.Sleep(4000);
            btnOk.Click();
        }

        [AfterScenario]
        public void CloseApplication()
        {
            GuiDriver.Dispose();
        }
    }
}
