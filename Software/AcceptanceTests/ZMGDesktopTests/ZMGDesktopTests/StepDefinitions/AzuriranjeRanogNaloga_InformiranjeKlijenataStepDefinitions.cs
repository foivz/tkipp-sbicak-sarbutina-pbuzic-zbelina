using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
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

        [Then(@"Korisnik klikne na gumb Detalji")]
        public void ThenKorisnikKlikneNaGumbDetalji()
        {
            var driver = GuiDriver.GetDriver();
            var btnDetalji = driver.FindElementByAccessibilityId("btnDetalji");
            btnDetalji.Click();
        }

        [Then(@"Korisnik klikne na gumb Izmijeni")]
        public void ThenKorisnikKlikneNaGumbIzmijeni()
        {
            var driver = GuiDriver.GetDriver();
            var btnIzmijeni = driver.FindElementByAccessibilityId("btnIzmijeni");
            btnIzmijeni.Click();
        }

        [Then(@"Korisnik unosi u polje Količina = ""([^""]*)"", polje Opis = ""([^""]*)"", status ostaje isti te dodaje čelik u tablicu materijala")]
        public void ThenKorisnikUnosiUPoljeKolicinaPoljeOpisStatusOstajeIstiTeDodajeCelikUTablicuMaterijala(string kolicina, string opis)
        {
            var driver = GuiDriver.GetDriver();
            var txtKolicina = driver.FindElementByAccessibilityId("txtKolicina");
            var txtOpis = driver.FindElementByAccessibilityId("txtOpis");
            var btnDodajMaterijal = driver.FindElementByAccessibilityId("btnDodajMaterijal");

            txtKolicina.SendKeys(kolicina);
            txtOpis.SendKeys(opis);
            btnDodajMaterijal.Click();
        }

        [Then(@"Korisnik klikće na gumb Spremi")]
        public void ThenKorisnikKlikceNaGumbSpremi()
        {
            var driver = GuiDriver.GetDriver();
            var btnSpremi = driver.FindElementByAccessibilityId("btnSpremi");
            btnSpremi.Click();
        }

        [Then(@"Podaci radnog naloga su izmijenjeni i spremljeni")]
        public void ThenPodaciRadnogNalogaSuIzmijenjeniISpremljeni()
        {
            var driver = GuiDriver.GetDriver();
            var dgvPopisRadnihNaloga = driver.FindElementByAccessibilityId("dgvPopisRadnihNaloga");
            bool isOpened = driver.FindElementByAccessibilityId("FrmPopisRadnihNaloga") != null;
            Assert.IsTrue(isOpened);
        }

        [Then(@"Korisnik ostavlja polje Količina prazno pa unosi Opis = ""([^""]*)""")]
        public void ThenKorisnikOstavljaPoljeKolicinaPraznoPaUnosiOpis(string opis)
        {
            var driver = GuiDriver.GetDriver();
            var txtOpis = driver.FindElementByAccessibilityId("txtOpis");
            var txtKolicina = driver.FindElementByAccessibilityId("txtKolicina");

            for (int i=0; i < 2; i++)
            {
                txtKolicina.SendKeys(Keys.Backspace);
            }
            txtOpis.SendKeys(opis);
        }

        [Then(@"Prikaz poruke ""([^""]*)""")]
        public void ThenPrikazPoruke(string poruka)
        {
            var driver = GuiDriver.GetDriver();

            var messageBox = driver.FindElementByAccessibilityId("65535");
            Assert.IsTrue(poruka == messageBox.Text);
        }

        [Then(@"Podaci radnog naloga su izmijenjeni i spremljeni te Email poruka o promjeni nije poslana")]
        public void ThenPodaciRadnogNalogaSuIzmijenjeniISpremljeniTeEmailPorukaOPromjeniNijePoslana()
        {
            var driver = GuiDriver.GetDriver();
            var dgvPopisRadnihNaloga = driver.FindElementByAccessibilityId("dgvPopisRadnihNaloga");
            bool isOpened = driver.FindElementByAccessibilityId("FrmPopisRadnihNaloga") != null;
            Assert.IsTrue(isOpened);
        }

        [Then(@"Korisnik odabire radni nalog s popisa sa statusom Napravljen")]
        public void ThenKorisnikOdabireRadniNalogSPopisaSaStatusomNapravljen()
        {
            var driver = GuiDriver.GetDriver();
            var dgvPopisRadnihNaloga = driver.FindElementByAccessibilityId("dgvPopisRadnihNaloga");
            dgvPopisRadnihNaloga.Click();
        }

        [Then(@"Korisnik mijenja status radnog naloga iz padajuće liste iz Napravljen u U obradi")]
        public void ThenKorisnikMijenjaStatusRadnogNalogaIzPadajuceListeIzNapravljenUUObradi()
        {
            var driver = GuiDriver.GetDriver();
            var cmbStatus = driver.FindElementByAccessibilityId("cmbStatus");

            cmbStatus.Click();
            cmbStatus.SendKeys(Keys.Down);
            cmbStatus.SendKeys(Keys.Enter);
        }

        [Then(@"Korisnik dobiva E-mail poruku o promjeni statusa radnog naloga")]
        public void ThenKorisnikDobivaE_MailPorukuOPromjeniStatusaRadnogNaloga()
        {
            var driver = GuiDriver.GetDriver();
            var dgvPopisRadnihNaloga = driver.FindElementByAccessibilityId("dgvPopisRadnihNaloga");
            bool isOpened = driver.FindElementByAccessibilityId("FrmPopisRadnihNaloga") != null;
            Assert.IsTrue(isOpened);
        }

        [Then(@"Korisnik odabire radni nalog s popisa sa statusom U obradi")]
        public void ThenKorisnikOdabireRadniNalogSPopisaSaStatusomUObradi()
        {
            var driver = GuiDriver.GetDriver();
            var dgvPopisRadnihNaloga = driver.FindElementByAccessibilityId("dgvPopisRadnihNaloga");
            dgvPopisRadnihNaloga.Click();
            dgvPopisRadnihNaloga.SendKeys(Keys.Down);
            dgvPopisRadnihNaloga.SendKeys(Keys.Down);
        }

        [Then(@"Korisnik mijenja status radnog naloga iz padajuće liste iz U obradi u Dovršen")]
        public void ThenKorisnikMijenjaStatusRadnogNalogaIzPadajuceListeIzUObradiUDovrsen()
        {
            var driver = GuiDriver.GetDriver();
            var cmbStatus = driver.FindElementByAccessibilityId("cmbStatus");

            cmbStatus.Click();
            cmbStatus.SendKeys(Keys.Down);
            cmbStatus.SendKeys(Keys.Enter);
        }

        [AfterScenario]
        public void CloseApplication()
        {
            GuiDriver.Dispose();
        }
    }
}
