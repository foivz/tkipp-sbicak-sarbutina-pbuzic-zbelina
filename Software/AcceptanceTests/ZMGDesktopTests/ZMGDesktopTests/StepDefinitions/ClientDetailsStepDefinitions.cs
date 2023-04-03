using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class ClientDetailsStepDefinitions
    {
        [Given(@"Korisnik se nalazi u glavnom izborniku")]
        public void GivenKorisnikSeNalaziUGlavnomIzborniku()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmPocetna") != null;
            bool title = driver.Title == "Glavni izbornik";
            Assert.IsTrue(isOpen);
        }


        [When(@"Korisnik odabere gumb klijenti")]
        public void WhenKorisnikOdabereGumbKlijenti()
        {
            var driver = GuiDriver.GetDriver();
            var btnKlijenti = driver.FindElementByAccessibilityId("btnKlijenti");
            btnKlijenti.Click();
        }

        [Then(@"Otvara se forma za prikaz svih klijenata")]
        public void ThenOtvaraSeFormaZaPrikazSvihKlijenata()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isOpened = driver.FindElementByAccessibilityId("FrmPregledKlijenata") != null;
            //bool title = driver.Title == "Pregled klijenata";
            Assert.IsTrue(isOpened);
        }

        [Then(@"Korisnik selektira klijenta ""([^""]*)""")]
        public void ThenKorisnikSelektiraKlijenta(string smartShop)
        {
            var driver = GuiDriver.GetDriver();
            var value = driver.FindElementByName("Row 5");
            value.Click();
        }

        [Then(@"Korisnik klikne na gumb Detalji klijenta")]
        public void ThenKorisnikKlikneNaGumbDetaljiKlijenta()
        {
            var driver = GuiDriver.GetDriver();
            var btnDetaljiKlijenta = driver.FindElementByAccessibilityId("btnDetaljiKlijenta");
            btnDetaljiKlijenta.Click();
        }

        [Then(@"Korisniku se otvara forma na kojoj se prikazuju računi i radni nalozi za klijenta ""([^""]*)""")]
        public void ThenKorisnikuSeOtvaraFormaNaKojojSePrikazujuRacuniIRadniNaloziZaKlijenta(string smartShop)
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isOpened = driver.FindElementByAccessibilityId("FrmDetaljniPrikazKlijenta") != null;
            var lblKlijent = driver.FindElementByAccessibilityId("labelImePrezime");
            Assert.IsTrue(lblKlijent.Text == smartShop && isOpened == true);
        }

        [AfterScenario]
        public void CloseApplication()
        {
            GuiDriver.Dispose();
        }

    }
}
