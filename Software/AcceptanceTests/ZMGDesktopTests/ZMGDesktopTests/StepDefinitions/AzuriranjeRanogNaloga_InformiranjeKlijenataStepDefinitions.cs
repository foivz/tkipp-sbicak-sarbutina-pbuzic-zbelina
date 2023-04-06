using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class AzuriranjeRanogNaloga_InformiranjeKlijenataStepDefinitions
    {
        [Given(@"Korisnik se nalazi na početnom ekranu")]
        public void GivenKorisnikSeNalaziNaPocetnomEkranu()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmPocetna") != null;
            bool title = driver.Title == "Glavni izbornik";
            Assert.IsTrue(isOpen);
        }

        [When(@"Korisnik klikne na gumb Radni nalozi")]
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

        [Given(@"Korisnik odabire radni nalog s bilo kojim statusom")]
        public void GivenKorisnikOdabireRadniNalogSBiloKojimStatusom()
        {
            var driver = GuiDriver.GetDriver();
            var radniNalog = driver.FindElementByName("Status Row 0, Not sorted.");

            radniNalog.Click();
        }

        [Given(@"Korisnik klikne na gumb Detalji")]
        public void GivenKorisnikKlikneNaGumbDetalji()
        {
            var driver = GuiDriver.GetDriver();
            var btnDetalji = driver.FindElementByAccessibilityId("btnDetalji");
            btnDetalji.Click();
        }

        [Given(@"Korisnik klikne na gumb Izmijeni")]
        public void GivenKorisnikKlikneNaGumbIzmijeni()
        {
            var driver = GuiDriver.GetDriver();
            var btnIzmijeni = driver.FindElementByAccessibilityId("btnIzmijeni");
            btnIzmijeni.Click();
        }

        [Given(@"Korisnik unosi u polje Količina = ""([^""]*)"", polje Opis = ""([^""]*)"", status ostaje isti te dodaje čelik u tablicu materijala")]
        public void GivenKorisnikUnosiUPoljeKolicinaPoljeOpisStatusOstajeIstiTeDodajeCelikUTablicuMaterijala(string kolicina, string opis)
        {
            var driver = GuiDriver.GetDriver();
            var txtKolicina = driver.FindElementByAccessibilityId("txtKolicina");
            var txtOpis = driver.FindElementByAccessibilityId("txtOpis");
            var btnDodajMaterijal = driver.FindElementByAccessibilityId("btnDodajMaterijal");

            txtKolicina.SendKeys(kolicina);
            txtOpis.SendKeys(opis);
            btnDodajMaterijal.Click();
        }

        [Given(@"Korisnik klikne na gumb Spremi")]
        public void GivenKorisnikKlikneNaGumbSpremi()
        {
            var driver = GuiDriver.GetDriver();
            var btnSpremi = driver.FindElementByAccessibilityId("btnSpremi");
            btnSpremi.Click();
        }

        [Given(@"Podaci radnog naloga su izmijenjeni i spremljeni")]
        public void GivenPodaciRadnogNalogaSuIzmijenjeniISpremljeni()
        {
            var driver = GuiDriver.GetDriver();
            var dgvPopisRadnihNaloga = driver.FindElementByAccessibilityId("dgvPopisRadnihNaloga");
            bool isOpened = driver.FindElementByAccessibilityId("FrmPopisRadnihNaloga") != null;
            Assert.IsTrue(isOpened);
        }

        [Given(@"Korisnik ostavlja polje Količina prazno pa unosi Opis = ""([^""]*)""")]
        public void GivenKorisnikOstavljaPoljeKolicinaPraznoPaUnosiOpis(string opis)
        {
            var driver = GuiDriver.GetDriver();
            var txtOpis = driver.FindElementByAccessibilityId("txtOpis");

            txtOpis.SendKeys(opis);
        }

        [Given(@"Prikaz poruke ""([^""]*)""")]
        public void GivenPrikazPoruke(string poruka)
        {
            var driver = GuiDriver.GetDriver();
            var messageBox = driver.FindElementByAccessibilityId("65535");
            Assert.IsTrue(poruka == messageBox.Text);
        }

        [Given(@"Korisnik ne mijenja status radnog naloga")]
        public void GivenKorisnikNeMijenjaStatusRadnogNaloga()
        {
            var driver = GuiDriver.GetDriver();
            var btnDodajMaterijal = driver.FindElementByAccessibilityId("btnDodajMaterijal");
            btnDodajMaterijal.Click();
        }

        [Given(@"Podaci radnog naloga su izmijenjeni i spremljeni te Email poruka o promjeni nije poslana")]
        public void GivenPodaciRadnogNalogaSuIzmijenjeniISpremljeniTeEmailPorukaOPromjeniNijePoslana()
        {
            var driver = GuiDriver.GetDriver();
            var dgvPopisRadnihNaloga = driver.FindElementByAccessibilityId("dgvPopisRadnihNaloga");
            bool isOpened = driver.FindElementByAccessibilityId("FrmPopisRadnihNaloga") != null;
            Assert.IsTrue(isOpened);
        }

        [Given(@"Korisnik odabire radni nalog s popisa s sa statusom Napravljen")]
        public void GivenKorisnikOdabireRadniNalogSPopisaSSaStatusomNapravljen()
        {
            var driver = GuiDriver.GetDriver();
            var radniNalog = driver.FindElementByName("Status Row 1, Not sorted");
            radniNalog.Click();
        }

        [Given(@"Korisnik mijenja status radnog naloga iz padajuće liste iz Napravljen u U obradi")]
        public void GivenKorisnikMijenjaStatusRadnogNalogaIzPadajuceListeIzNapravljenUUObradi()
        {
            throw new PendingStepException();
        }

        [Given(@"Korisnik dobiva E-mail poruku o promjeni statusa radnog naloga")]
        public void GivenKorisnikDobivaE_MailPorukuOPromjeniStatusaRadnogNaloga()
        {
            throw new PendingStepException();
        }

        [Given(@"Korisnik odabire radni nalog s popisa s sa statusom U obradi")]
        public void GivenKorisnikOdabireRadniNalogSPopisaSSaStatusomUObradi()
        {
            var driver = GuiDriver.GetDriver();
            var radniNalog = driver.FindElementByName("Status Row 2, Not sorted");
            radniNalog.Click();
        }

        [Given(@"Korisnik mijenja status radnog naloga iz padajuće liste iz U obradi u Dovršen")]
        public void GivenKorisnikMijenjaStatusRadnogNalogaIzPadajuceListeIzUObradiUDovrsen()
        {
            throw new PendingStepException();
        }

        [AfterScenario]
        public void CloseApplication()
        {
            GuiDriver.Dispose();
        }
    }
}
