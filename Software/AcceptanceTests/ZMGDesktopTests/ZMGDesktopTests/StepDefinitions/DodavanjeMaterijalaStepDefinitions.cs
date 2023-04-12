using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using System.Windows;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class DodavanjeMaterijalaStepDefinitions
    {
        [When(@"Korisnik klikne na gumb za dodavanje materijala")]
        public void WhenKorisnikKlikneNaGumbZaDodavanjeMaterijala()
        {
            var driver = GuiDriver.GetDriver();
            var gumb = driver.FindElementByAccessibilityId("btnDodajMaterijal");
            gumb.Click();
        }

        


        [Given(@"Korisnik se nalazi na formi glavnog izbornika")]
        public void GivenKorisnikSeNalaziNaFormiGlavnogIzbornika()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmPocetna") != null;
            bool title = driver.Title == "Glavni izbornik";
            Assert.IsTrue(isOpen);
        }

        [Then(@"Korisnik se nalazi na formi za upravljanje katalogom usluga i materijala")]
        public void ThenKorisnikSeNalaziNaFormiZaUpravljanjeKatalogomUslugaIMaterijala()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmKatalog") != null;
            Assert.IsTrue(isOpen);
        }


       

        [When(@"Korisnik klikne na gumb '([^']*)'")]
        public void WhenKorisnikKlikneNaGumb(string prosljGumb)
        {
            var driver = GuiDriver.GetDriver();
            var gumb = driver.FindElementByName(prosljGumb);
            gumb.Click();
            
        }

        [When(@"Otvara se forma za dodavanje novog materijala")]
        public void WhenOtvaraSeFormaZaDodavanjeNovogMaterijala()
        {
            var driver = GuiDriver.GetDriver();

            //Thread.Sleep(2000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());


            var isOpened = driver.FindElementByAccessibilityId("FrmDodajMaterijal");


            Assert.IsNotNull(isOpened);
        }

        [Then(@"Prikazana je poruka ""([^""]*)""")]
        public void ThenPrikazanaJePoruka(string poruka)
        {
            var driver = GuiDriver.GetDriver();
            
            var lblErrorMessage = driver.FindElementByName(poruka);
            var actualMessage = lblErrorMessage.Text;
            Assert.AreEqual(actualMessage, poruka);
        }




        [When(@"Radnik unosi ""([^""]*)"" u polje za naziv")]
        public void WhenRadnikUnosiUPoljeZaNaziv(string naziv)
        {
            var driver = GuiDriver.GetDriver();
            var txtNaziv = driver.FindElementByAccessibilityId("txtNaziv");

            txtNaziv.SendKeys(naziv);
        }

        [When(@"unosi ""([^""]*)"" u polje za količinu")]
        public void WhenUnosiUPoljeZaKolicinu(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var numKol = driver.FindElementByAccessibilityId("txtKolicina");

            numKol.SendKeys(p0);
        }

        [When(@"odabire ""([^""]*)"" u padajućem izborniku za mjernu jedinicu")]
        public void WhenOdabireUPadajucemIzbornikuZaMjernuJedinicu(string kg)
        {
            var driver = GuiDriver.GetDriver();
            var cmbJedinica = driver.FindElementByAccessibilityId("cmbMjernaJedinica");
            cmbJedinica.Click();
            cmbJedinica.FindElementByName("kg").Click();
        }

        [When(@"unosi ""([^""]*)"" u polje za cijenu po jedinici")]
        public void WhenUnosiUPoljeZaCijenuPoJedinici(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var numCijena = driver.FindElementByAccessibilityId("txtCijena");

            numCijena.SendKeys(p0);
        }

        [When(@"unosi ""([^""]*)"" u polje za opis")]
        public void WhenUnosiUPoljeZaOpis(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var txtOpis = driver.FindElementByAccessibilityId("txtOpis");

            txtOpis.SendKeys(p0);
        }

        [When(@"označava ""([^""]*)"" u polju za opasnost po život")]
        public void WhenOznacavaUPoljuZaOpasnostPoZivot(string @false)
        {
            var driver = GuiDriver.GetDriver();
            var txtOpasno = driver.FindElementByAccessibilityId("txtOpasno");

            txtOpasno.SendKeys(@false);
        }

        

        [Then(@"Materijal ""([^""]*)"" se prikazuje na popisu materijala")]
        public void ThenMaterijalSePrikazujeNaPopisuMaterijala(string guma)
        {
            var driver = GuiDriver.GetDriver();
            var dgvRacuni = driver.FindElementByAccessibilityId("dgvMaterijali");
            var value1 = dgvRacuni.FindElementByName("Naziv Row 6, Not sorted.").Text;

            Assert.IsTrue(value1 == "Guma");
        }

        [Then(@"postoji materijal ""([^""]*)"" u katalogu")]
        public void ThenPostojiMaterijalUKatalogu(string celik)
        {
            var driver = GuiDriver.GetDriver();
            var dgvMaterijali = driver.FindElementByAccessibilityId("dgvMaterijali");
            var value1 = dgvMaterijali.FindElementByName("Naziv Row 0, Not sorted.").Text;

            Assert.IsTrue(value1 == "Celik");
        }



        [Given(@"postoji materijal ""([^""]*)"" u katalogu")]
        public void GivenPostojiMaterijalUKatalogu(string celik)
        {
            throw new PendingStepException();
        }

        

        [When(@"Korisnik unosi ""([^""]*)"" u polje za mjernu jedinicu")]
        public void WhenKorisnikUnosiUPoljeZaMjernuJedinicu(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var cmbJedinica = driver.FindElementByAccessibilityId("cmbMjernaJedinica");

            cmbJedinica.SendKeys("Rucna promjena");
        }

        [Then(@"Prikazuje se poruka da treba ispuniti sva polja")]
        public void ThenPrikazujeSePorukaDaTrebaIspunitiSvaPolja()
        {
            var driver = GuiDriver.GetDriver();
            

            var lblErrorMessage = driver.FindElementByName("Potrebno je ispuniti sva polja");
            var actualMessage = lblErrorMessage.Text;
            Assert.AreEqual(actualMessage, "Potrebno je ispuniti sva polja");
        }


        [Then(@"Korisnik bi trebao vidjeti novi materijal s nazivom '([^']*)' na popisu svih materijala")]
        public void ThenKorisnikBiTrebaoVidjetiNoviMaterijalSNazivomNaPopisuSvihMaterijala(string đćčšž)
        {
            var driver = GuiDriver.GetDriver();
 
            var dgvMat = driver.FindElementByAccessibilityId("dgvMaterijali");
            var value1 = dgvMat.FindElementByName("Naziv Row 8, Not sorted.").Text;

            Assert.IsTrue(value1 == "Ðccšž");
        }
    }
}
