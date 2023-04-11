using System;
using TechTalk.SpecFlow;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class BrisanjeMaterijalaStepDefinitions
    {
        [Given(@"korisnik je na formi za upravljanje katalogom usluga i materijala")]
        public void GivenKorisnikJeNaFormiZaUpravljanjeKatalogomUslugaIMaterijala()
        {
            throw new PendingStepException();
        }

        [Given(@"postoji materijal ""([^""]*)"" u popisu svih materijala")]
        public void GivenPostojiMaterijalUPopisuSvihMaterijala(string drvo)
        {
            throw new PendingStepException();
        }

        [When(@"korisnik klikne na red ili čeliju unutar reda materijala ""([^""]*)""")]
        public void WhenKorisnikKlikneNaRedIliCelijuUnutarRedaMaterijala(string drvo)
        {
            throw new PendingStepException();
        }

        [When(@"korisnik klikne na gumb Obriši materijal")]
        public void WhenKorisnikKlikneNaGumbObrisiMaterijal()
        {
            throw new PendingStepException();
        }

        [Then(@"materijal ""([^""]*)"" se ne prikazuje više u popisu svih materijala")]
        public void ThenMaterijalSeNePrikazujeViseUPopisuSvihMaterijala(string drvo)
        {
            throw new PendingStepException();
        }

        [Given(@"materijal ""([^""]*)"" se nalazi u nekom radnom nalogu ili primci")]
        public void GivenMaterijalSeNalaziUNekomRadnomNaloguIliPrimci(string celik)
        {
            throw new PendingStepException();
        }

        [When(@"korisnik klikne na red ili čeliju unutar reda materijala ""([^""]*)""")]
        public void WhenKorisnikKlikneNaRedIliCelijuUnutarRedaMaterijala(string celik)
        {
            throw new PendingStepException();
        }

        [Then(@"prikazuje se poruka da nije moguće obrisati materijal koji se nalazi u nekom radnom nalogu ili primci")]
        public void ThenPrikazujeSePorukaDaNijeMoguceObrisatiMaterijalKojiSeNalaziUNekomRadnomNaloguIliPrimci()
        {
            throw new PendingStepException();
        }
    }
}
