using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class ClientReportStepDefinitions
    {
        [Given(@"Korisnik je na glavnom izborniku\.")]
        public void GivenKorisnikJeNaGlavnomIzborniku_()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmPocetna") != null;
            bool title = driver.Title == "Glavni izbornik";
            Assert.IsTrue(isOpen);
        }

        [When(@"Korisnik klikne na gumb Izvjestaji")]
        public void WhenKorisnikKlikneNaGumbIzvjestaji()
        {
            var driver = GuiDriver.GetDriver();
            var btnIzvjestaji = driver.FindElementByAccessibilityId("btnIzvjestaji");
            btnIzvjestaji.Click();
        }

        [Then(@"Korisniku se otvara forma za izvjestaje")]
        public void ThenKorisnikuSeOtvaraFormaZaIzvjestaje()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool frmIzvjestaji = driver.FindElementByAccessibilityId("FrmPopisIzvjestaja") != null;
            Assert.IsTrue(frmIzvjestaji);
        }

        [Then(@"Korisniku se ispise poruka ""([^""]*)""")]
        public void ThenKorisnikuSeIspisePoruka(string poruka)
        {
            var driver = GuiDriver.GetDriver();
            var messageBox = driver.FindElementByAccessibilityId("65535");
            Assert.IsTrue(poruka == messageBox.Text);
            var btnOk = driver.FindElementByAccessibilityId("2");
            Thread.Sleep(3500);
            btnOk.Click();
        }


        [Then(@"Korisnik klikne na gumb Izrada klijentskih izvještaja")]
        public void ThenKorisnikKlikneNaGumbIzradaKlijentskihIzvjestaja()
        {
            var driver = GuiDriver.GetDriver();
            var btnIzvjestajKlijenata = driver.FindElementByAccessibilityId("btnIzvjestajKlijenata");
            btnIzvjestajKlijenata.Click();

        }

        [Then(@"Korisniku se otvara forma na kojoj je izvještaj o (.*) najvećih klijenata \(stupčasti graf i graf pita\)")]
        public void ThenKorisnikuSeOtvaraFormaNaKojojJeIzvjestajONajvecihKlijenataStupcastiGrafIGrafPita(int p0)
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isOpened = driver.FindElementByName("Report") != null;
            Assert.IsTrue(isOpened);
        }

        [AfterScenario]
        public void CloseApplication()
        {
            GuiDriver.Dispose();
        }
    }
}
