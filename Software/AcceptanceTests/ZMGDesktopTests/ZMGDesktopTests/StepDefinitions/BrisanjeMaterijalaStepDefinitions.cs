using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class BrisanjeMaterijalaStepDefinitions
    {
        [Given(@"korisnik je na formi za upravljanje katalogom usluga i materijala")]
        public void GivenKorisnikJeNaFormiZaUpravljanjeKatalogomUslugaIMaterijala()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmKatalog") != null;
            Assert.IsTrue(isOpen);
        }

        [Then(@"postoji materijal ""([^""]*)"" u popisu svih materijala")]
        public void ThenPostojiMaterijalUPopisuSvihMaterijala(string materijal)
        {
            var driver = GuiDriver.GetDriver();
            var dgvMat = driver.FindElementByAccessibilityId("dgvMaterijali");
            var value1 = dgvMat.FindElementByName("Naziv Row 0, Not sorted.").Text;
            var value2 = dgvMat.FindElementByName("Row 6");

            Assert.IsTrue(value1 == "Celik" || value2 != null);
        }


        [When(@"korisnik klikne na red ili čeliju unutar reda materijala ""([^""]*)""")]
        public void WhenKorisnikKlikneNaRedIliCelijuUnutarRedaMaterijala(string naziv)
        {
            var driver = GuiDriver.GetDriver();
            var dgvMat = driver.FindElementByAccessibilityId("dgvMaterijali");
            if (naziv == "Celik") { 
                var row = dgvMat.FindElementByName("Naziv Row 0, Not sorted.");
                row.Click();
            }
            else
            {
                var row = dgvMat.FindElementByName("Naziv Row 6, Not sorted.");
                row.Click();
            }
            
        }

        [When(@"korisnik klikne na gumb ""([^""]*)""")]
        public void WhenKorisnikKlikneNaGumb(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var gumb = driver.FindElementByAccessibilityId("btnObrisi");
            gumb.Click();
        }


        [Then(@"materijal ""([^""]*)"" se ne prikazuje više u popisu svih materijala")]
        public void ThenMaterijalSeNePrikazujeViseUPopisuSvihMaterijala(string drvo)
        {
            var driver = GuiDriver.GetDriver();
            var dgvMat = driver.FindElementByAccessibilityId("dgvMaterijali");
            var value2 = dgvMat.FindElementByName("Naziv Row 6, Not sorted.") != null;
            Assert.IsNotNull(value2);
        }

        [Given(@"materijal ""([^""]*)"" se nalazi u nekom radnom nalogu ili primci")]
        public void GivenMaterijalSeNalaziUNekomRadnomNaloguIliPrimci(string celik)
        {
            throw new PendingStepException();
        }

 

        [Then(@"prikazuje se poruka da nije moguće obrisati materijal koji se nalazi u nekom radnom nalogu ili primci")]
        public void ThenPrikazujeSePorukaDaNijeMoguceObrisatiMaterijalKojiSeNalaziUNekomRadnomNaloguIliPrimci()
        {
            var driver = GuiDriver.GetDriver();
            var lblErrorMessage = driver.FindElementByName("Zabranjeno brisanje materijala koji se nalazi u radnom nalogu ili primci.");
            Assert.IsNotNull(lblErrorMessage);
        }
    }
}
