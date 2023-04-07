using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class IzvjestajiRadnihNalogaStepDefinitions
    {
        [Given(@"Korisnik se nalazi na početnom izborniku")]
        public void GivenKorisnikSeNalaziNaPocetnomIzborniku()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmPocetna") != null;
            bool title = driver.Title == "Glavni izbornik";
            Assert.IsTrue(isOpen);
        }

        [When(@"Korisnik klikne na gumb izvještaji")]
        public void WhenKorisnikKlikneNaGumbIzvjestaji()
        {
            var driver = GuiDriver.GetDriver();
            var btnIzvjestaji = driver.FindElementByAccessibilityId("btnIzvjestaji");
            btnIzvjestaji.Click();
        }

        [Then(@"Korisniku se otvara forma za izvještaje")]
        public void ThenKorisnikuSeOtvaraFormaZaIzvjestaje()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isOpened = driver.FindElementByAccessibilityId("FrmPopisIzvjestaja") != null;
            Assert.IsTrue(isOpened);
        }

        [Then(@"Korisnik klikne na gumb za Izrada izvještaja radnih naloga")]
        public void ThenKorisnikKlikneNaGumbZaIzradaIzvjestajaRadnihNaloga()
        {
            var driver = GuiDriver.GetDriver();
            var btnIzvjestajRadnihNaloga = driver.FindElementByAccessibilityId("btnIzvjestajRadnihNaloga");
            btnIzvjestajRadnihNaloga.Click();
        }

        [Then(@"Korisniku se otvara forma u kojoj se nalazi izvještaj o svim radnim nalozima po statusima na stupčastom i grafu pita")]
        public void ThenKorisnikuSeOtvaraFormaUKojojSeNalaziIzvjestajOSvimRadnimNalozimaPoStatusimaNaStupcastomIGrafuPita()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isOpened = driver.FindElementByAccessibilityId("FrmKreirajIzvjestajRadnihNaloga") != null;
            Assert.IsTrue(isOpened);
        }
    }
}
