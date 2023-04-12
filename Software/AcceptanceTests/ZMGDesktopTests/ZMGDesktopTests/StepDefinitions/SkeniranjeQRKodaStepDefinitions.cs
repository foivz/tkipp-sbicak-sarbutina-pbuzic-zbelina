using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class SkeniranjeQRKodaStepDefinitions
    {
        [Given(@"Korisnik je na formi za upravljanje katalogom usluga i materijala")]
        public void GivenKorisnikJeNaFormiZaUpravljanjeKatalogomUslugaIMaterijala()
        {
            throw new PendingStepException();
        }

        [When(@"Korisnik klikne na gumb ""([^""]*)""")]
        public void WhenKorisnikKlikneNaGumb(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var gumb = driver.FindElementByAccessibilityId("btnZaprimi");
            gumb.Click();
        }

        [When(@"Korisnik odabire kameru ""([^""]*)"" na padajućem izborniku")]
        public void WhenKorisnikOdabireKameruNaPadajucemIzborniku(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var cmbJedinica = driver.FindElementByAccessibilityId("cboDevice");
            cmbJedinica.Click();
        }

        [When(@"Korisnik klikne gumb za početak skeniranja")]
        public void WhenKorisnikKlikneGumbZaPocetakSkeniranja()
        {
            var driver = GuiDriver.GetDriver();
            var gumb = driver.FindElementByAccessibilityId("btnKreni");
            gumb.Click();
        }


        [When(@"Korisnik klikne gumb ""([^""]*)""")]
        public void WhenKorisnikKlikneGumb(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var gumb = driver.FindElementByAccessibilityId("btnZaprimi");
            gumb.Click();
        }



        [Then(@"Prikaz kamere je vidljiv na ekranu")]
        public void ThenPrikazKamereJeVidljivNaEkranu()
        {
            var driver = GuiDriver.GetDriver();
            var picQR = driver.FindElementByAccessibilityId("picQR");
            Assert.IsTrue(picQR.Displayed);
        }



        [Then(@"Skener prepoznaje QR kod i korisniku se daje mogućnost određivanja količine materijala")]
        public void ThenSkenerPrepoznajeQRKodIKorisnikuSeDajeMogucnostOdredivanjaKolicineMaterijala()
        {
            var driver = GuiDriver.GetDriver();
            var azuriraj = driver.FindElementByAccessibilityId("btnZaprimi");
            Assert.IsTrue(azuriraj.Enabled);
        }


        [Then(@"Skener prepoznaje neispravan QR kod i prikazuje se poruka da nije ispravan QR kod")]
        public void ThenSkenerPrepoznajeQRKodIPrikazujeSePorukaDaNijeIspravanQRKod()
        {
            var driver = GuiDriver.GetDriver();
            var error = driver.FindElementByName("lblError");
            Assert.IsNotNull(error);
        }

    }
}
