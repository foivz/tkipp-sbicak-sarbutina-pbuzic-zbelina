using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class DodavanjeMaterijalaStepDefinitions
    {
        [Given(@"Korisnik se nalazi na formi za upravljanje katalogom usluga i materijala")]
        public void GivenKorisnikSeNalaziNaFormiZaUpravljanjeKatalogomUslugaIMaterijala()
        {
            var driver = GuiDriverv2.GetOrCreateDriver();
            bool isOpen = driver.FindElementByAccessibilityId("FrmKatalog") != null;
            Assert.IsTrue(isOpen);
        }

        [When(@"Korisnik klikne na gumb '([^']*)'")]
        public void WhenKorisnikKlikneNaGumb(string p0)
        {
            var driver = GuiDriverv2.GetDriver();
            var btnLogin = driver.FindElementByAccessibilityId("btnDodaj");
            btnLogin.Click();
        }

        [When(@"otvara se forma za dodavanje novog materijala")]
        public void WhenOtvaraSeFormaZaDodavanjeNovogMaterijala()
        {
            throw new PendingStepException();
        }


        [When(@"Radnik unosi ""([^""]*)"" u polje za naziv")]
        public void WhenRadnikUnosiUPoljeZaNaziv(string guma)
        {
            throw new PendingStepException();
        }

        [When(@"unosi ""([^""]*)"" u polje za količinu")]
        public void WhenUnosiUPoljeZaKolicinu(string p0)
        {
            throw new PendingStepException();
        }

        [When(@"odabire ""([^""]*)"" u padajućem izborniku za mjernu jedinicu")]
        public void WhenOdabireUPadajucemIzbornikuZaMjernuJedinicu(string kg)
        {
            throw new PendingStepException();
        }

        [When(@"unosi ""([^""]*)"" u polje za cijenu po jedinici")]
        public void WhenUnosiUPoljeZaCijenuPoJedinici(string p0)
        {
            throw new PendingStepException();
        }

        [When(@"unosi ""([^""]*)"" u polje za opis")]
        public void WhenUnosiUPoljeZaOpis(string p0)
        {
            throw new PendingStepException();
        }

        [When(@"označava ""([^""]*)"" u polju za opasnost po život")]
        public void WhenOznacavaUPoljuZaOpasnostPoZivot(string @false)
        {
            throw new PendingStepException();
        }


        [Then(@"Materijal ""([^""]*)"" se prikazuje na popisu materijala")]
        public void ThenMaterijalSePrikazujeNaPopisuMaterijala(string guma)
        {
            throw new PendingStepException();
        }




        [Given(@"postoji materijal ""([^""]*)"" u katalogu")]
        public void GivenPostojiMaterijalUKatalogu(string celik)
        {
            throw new PendingStepException();
        }


        [When(@"Korisnik unosi ""([^""]*)"" u polje za naziv")]
        public void WhenKorisnikUnosiUPoljeZaNaziv(string celik)
        {
            throw new PendingStepException();
        }

        [When(@"Korisnik unosi ""([^""]*)"" u polje za mjernu jedinicu")]
        public void WhenKorisnikUnosiUPoljeZaMjernuJedinicu(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"Prikazuje se poruka da treba ispuniti sva polja")]
        public void ThenPrikazujeSePorukaDaTrebaIspunitiSvaPolja()
        {
            throw new PendingStepException();
        }

        [When(@"Korisnik unosi naziv '([^']*)'")]
        public void WhenKorisnikUnosiNaziv(string đćčšž)
        {
            throw new PendingStepException();
        }

        [Then(@"Korisnik bi trebao vidjeti novi materijal s nazivom '([^']*)' na popisu svih materijala")]
        public void ThenKorisnikBiTrebaoVidjetiNoviMaterijalSNazivomNaPopisuSvihMaterijala(string đćčšž)
        {
            throw new PendingStepException();
        }
    }
}
