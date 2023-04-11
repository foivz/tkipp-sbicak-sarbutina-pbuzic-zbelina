using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Security.Cryptography;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;
using System.Linq;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        [Given(@"Radnik se nalazi na formi prijave")]
        public void GivenRadnikSeNalaziNaFormiPrijave()
        {
            var driver = GuiDriverv3.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmLogin") != null;
            Assert.IsTrue(isOpen);
        }

        [When(@"Radnik unosi podatke: Korisnicko ime=""([^""]*)"", Lozinka=""([^""]*)""")]
        public void WhenRadnikUnosiPodatkeKorisnickoImeLozinka(string korime, string lozinka)
        {
            var driver = GuiDriverv3.GetDriver();
            var txtNaziv = driver.FindElementByAccessibilityId("txtKorIme");
            var txtLozinka = driver.FindElementByAccessibilityId("txtLozinka");

            txtNaziv.SendKeys(korime);
            txtLozinka.SendKeys(lozinka);
        }

        [When(@"Radnik klikne gumb Prijava")]
        public void WhenRadnikKlikneGumbPrijava()
        {
            var driver = GuiDriverv3.GetDriver();
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();
        }

        [Then(@"Radnik je prebačen s forme za prijavu na glavni izbornik")]
        public void ThenRadnikJePrebacenSFormeZaPrijavuNaGlavniIzbornik()
        {
            var driver = GuiDriverv3.GetDriver();

            if (driver.WindowHandles.Count > 1)
            {
                driver.SwitchTo().Window(driver.WindowHandles.Last());
            }

            var isOpened = driver.FindElementByName("FrmPocetna");


            Assert.IsNotNull(isOpened);

        }

        [Then(@"prikazuje se poruka ""([^""]*)""")]
        public void ThenPrikazujeSePoruka()
        {
            var driver = GuiDriverv3.GetDriver();
            var lblErrorMessage = driver.FindElementByAccessibilityId("lblErrorMessage");
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
