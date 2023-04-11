using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
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
            var driver = GuiDriverv2.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmLogin") != null;
            Assert.IsTrue(isOpen);
        }

        [When(@"Radnik unosi podatke: Korisnicko ime=""([^""]*)"", Lozinka=""([^""]*)""")]
        public void WhenRadnikUnosiPodatkeKorisnickoImeLozinka(string korime, string lozinka)
        {
            var driver = GuiDriverv2.GetDriver();
            var txtNaziv = driver.FindElementByAccessibilityId("txtKorIme");
            var txtLozinka = driver.FindElementByAccessibilityId("txtLozinka");

            txtNaziv.SendKeys(korime);
            txtLozinka.SendKeys(lozinka);
        }

        [When(@"Radnik klikne gumb Prijava")]
        public void WhenRadnikKlikneGumbPrijava()
        {
            var driver = GuiDriverv2.GetDriver();
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();
        }

        [Then(@"Radnik je prebačen s forme za prijavu na glavni izbornik")]
        public void ThenRadnikJePrebacenSFormeZaPrijavuNaGlavniIzbornik()
        {
            var driver = GuiDriverv2.GetDriver();

            Thread.Sleep(2000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            

            var isOpened = driver.FindElementByName("Glavni izbornik");


            Assert.IsNotNull(isOpened);

        }

        [When(@"Korisnik unese ""([^""]*)"" u polje za korisničko ime i unese ""([^""]*)"" u polje za lozinku")]
        public void WhenKorisnikUneseUPoljeZaKorisnickoImeIUneseUPoljeZaLozinku(string korime, string lozinka)
        {
            var driver = GuiDriverv2.GetDriver();
            var txtNaziv = driver.FindElementByAccessibilityId("txtKorIme");
            var txtLozinka = driver.FindElementByAccessibilityId("txtLozinka");

            txtNaziv.SendKeys(korime);
            txtLozinka.SendKeys(lozinka);
        }


        [Then(@"prikazuje se poruka ""([^""]*)""")]
        public void ThenPrikazujeSePoruka()
        {
            var driver = GuiDriverv2.GetDriver();
            var lblErrorMessage = driver.FindElementByName("Krivi podaci!");
            var actualMessage = lblErrorMessage.Text;
            Assert.AreEqual(actualMessage, "Krivi podaci!");
        }


        [AfterScenario]
        public void CloseApplication()
        {
            GuiDriver.Dispose();
        }
    }
}
