using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;
using System.Linq;
using System.Threading;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        [Given(@"Radnik se nalazi na formi prijave")]
        public void GivenRadnikSeNalaziNaFormiPrijave()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmLogin") != null;
            Assert.IsTrue(isOpen);
        }

        [When(@"Radnik unosi podatke: Korisnicko ime=""([^""]*)"", Lozinka=""([^""]*)""")]
        public void WhenRadnikUnosiPodatkeKorisnickoImeLozinka(string korime, string lozinka)
        {
            var driver = GuiDriver.GetDriver();
            var txtNaziv = driver.FindElementByAccessibilityId("txtKorIme");
            var txtLozinka = driver.FindElementByAccessibilityId("txtLozinka");

            txtNaziv.SendKeys(korime);
            txtLozinka.SendKeys(lozinka);
        }

        [When(@"Radnik klikne gumb Prijava")]
        public void WhenRadnikKlikneGumbPrijava()
        {
            var driver = GuiDriver.GetDriver();
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();
        }

        [Then(@"Radnik je prebačen s forme za prijavu na glavni izbornik")]
        public void ThenRadnikJePrebacenSFormeZaPrijavuNaGlavniIzbornik()
        {
            var driver = GuiDriverv2.GetDriver();
            Thread.Sleep(2000);

            driver.SwitchTo().Window(driver.WindowHandles.First());


            var isOpened = driver.FindElementByName("Glavni izbornik");


            Assert.IsNotNull(isOpened);

        }

        [When(@"Radnik unese ""([^""]*)"" u polje za korisničko ime i unese ""([^""]*)"" u polje za lozinku")]
        public void WhenRadnikUneseUPoljeZaKorisnickoImeIUneseUPoljeZaLozinku(string korime, string lozinka)
        {
            var driver = GuiDriverv2.GetDriver();
            var txtNaziv = driver.FindElementByAccessibilityId("txtKorIme");
            var txtLozinka = driver.FindElementByAccessibilityId("txtLozinka");

            txtNaziv.SendKeys(korime);
            txtLozinka.SendKeys(lozinka);
        }

        [Then(@"Radniku se prikazuje poruka ""([^""]*)""")]
        public void ThenRadnikuSePrikazujePoruka(string poruka)
        {
            var driver = GuiDriverv2.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            var error = driver.FindElementByName("Krivi podaci!") != null;
            Assert.IsTrue(error);
        }




        [AfterScenario]
        public void CloseApplication()
        {
            GuiDriver.Dispose();
        }
    }
}
