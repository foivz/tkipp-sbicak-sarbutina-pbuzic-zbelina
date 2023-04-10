using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Forms;
using System.Xml.Linq;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class Pregledavanje_RacunaStepDefinitions
    {
        [Given(@"Korisnik se nalazi na formi za pregledavanje racuna")]
        public void GivenKorisnikSeNalaziNaFormiZaPregledavanjeRacuna()
        {
            var driver = GuiDriverAppOpen.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmRacuni") != null;
            Assert.IsTrue(isOpen);
        }

        [Then(@"Korisniku se prikazuju svi racuni iz baze podataka")]
        public void ThenKorisnikuSePrikazujuSviRacuniIzBazePodataka()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            bool racuniSePrikazuju = driver.FindElementByAccessibilityId("dgvRacuni") != null;
            Assert.IsTrue(racuniSePrikazuju);
        }

        [When(@"Korisnik klikne na radiogumb Uzlazno")]
        public void WhenKorisnikKlikneNaRadiogumbUzlazno()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var rbtnUzlazno = driver.FindElementByAccessibilityId("rbtnUzlazno");
            rbtnUzlazno.Click();
        }

        [Then(@"Korisnik klikne na gumb Pretrazivanje")]
        public void ThenKorisnikKlikneNaGumbPretrazivanje()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var btnPretraznivanje = driver.FindElementByAccessibilityId("btnPretrazivanje");
            btnPretraznivanje.Click();
        }

        [Then(@"Korisniku se prikazuju svi racuni klijenta uzlazno prema identifikatorima")]
        public void ThenKorisnikuSePrikazujuSviRacuniKlijentaUzlaznoPremaIdentifikatorima()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var dgvRacuni = driver.FindElementByAccessibilityId("dgvRacuni");
            var value = dgvRacuni.FindElementByName("Racun_ID Row 0, Not sorted.").Text;
            Assert.IsTrue(value == "158");
        }

        [When(@"Korisnik klikne na radiogumb Silazno")]
        public void WhenKorisnikKlikneNaRadiogumbSilazno()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var rbtnSilazno = driver.FindElementByAccessibilityId("rbtnSilazno");
            rbtnSilazno.Click();
        }

        [Then(@"Korisniku se prikazuju svi racuni klijenta silazno prema identifikatorima")]
        public void ThenKorisnikuSePrikazujuSviRacuniKlijentaSilaznoPremaIdentifikatorima()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var dgvRacuni = driver.FindElementByAccessibilityId("dgvRacuni");
            var value = dgvRacuni.FindElementByName("Racun_ID Row 0, Not sorted.").Text;
            Assert.IsTrue(value == "1061");
        }

        [Then(@"Korisnik klikne na radiogumb Ukupni iznos racuna")]
        public void ThenKorisnikKlikneNaRadiogumbUkupniIznosRacuna()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var rbtnUkupniIznos = driver.FindElementByAccessibilityId("rbtnUkupniIznos");
            rbtnUkupniIznos.Click();
        }

        [Then(@"Korisniku se prikazuju svi racuni klijenta uzlazno prema ukupnoj cijeni")]
        public void ThenKorisnikuSePrikazujuSviRacuniKlijentaUzlaznoPremaUkupnojCijeni()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var dgvRacuni = driver.FindElementByAccessibilityId("dgvRacuni");
            var value = dgvRacuni.FindElementByName("UkupnaCijena Row 0, Not sorted.").Text;
            Assert.IsTrue(value == "49.5");
        }

        [Then(@"Korisniku se prikazuju svi racuni klijenta silazno prema ukupnoj cijeni")]
        public void ThenKorisnikuSePrikazujuSviRacuniKlijentaSilaznoPremaUkupnojCijeni()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var dgvRacuni = driver.FindElementByAccessibilityId("dgvRacuni");
            var value = dgvRacuni.FindElementByName("UkupnaCijena Row 0, Not sorted.").Text;
            Assert.IsTrue(value == "6750");
        }

        [When(@"Korisnik klikne na gumb Pretrazivanje")]
        public void WhenKorisnikKlikneNaGumbPretrazivanje()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var btnPretraznivanje = driver.FindElementByAccessibilityId("btnPretrazivanje");
            btnPretraznivanje.Click();
        }

        [Then(@"Korisnik klikne na gumb Ocisti")]
        public void ThenKorisnikKlikneNaGumbOcisti()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var btnOcisti = driver.FindElementByAccessibilityId("btnOcisti");
            btnOcisti.Click();
        }

        [Then(@"Korisniku se prikazuju svi racuni klijenta")]
        public void ThenKorisnikuSePrikazujuSviRacuniKlijenta()
        {
            var driver = GuiDriverAppOpen.GetDriver();
            var dgvRacuni = driver.FindElementByAccessibilityId("dgvRacuni");
            var value = dgvRacuni.FindElementByName("Klijent Row 0, Not sorted.").Text;
            Assert.IsTrue(value == "Aggreko");
        }


        [AfterScenario]
        public void CloseApplication()
        {
            GuiDriver.Dispose();
        }
    }
}
