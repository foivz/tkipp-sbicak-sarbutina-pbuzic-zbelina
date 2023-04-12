using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using ZMGDesktopTests.Support;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using OpenQA.Selenium.Interactions;
using System.Windows.Forms;

namespace ZMGDesktopTests.StepDefinitions
{
    [Binding]
    public class PrimkaStepDefinitions
    {
        int kolAluminija = 0;

        [Then(@"Korisnik se nalazi na formi za upravljanje katalogom")]
        public void ThenKorisnikSeNalaziNaFormiZaUpravljanjeKatalogom()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var dgvMat = driver.FindElementByAccessibilityId("dgvMaterijali");
            string kolAluminijaString = dgvMat.FindElementByName("Kolicina Row 5, Not sorted.").Text;
            int kolAluminija = int.Parse(kolAluminijaString);



            bool isOpen = driver.FindElementByAccessibilityId("FrmKatalog") != null;
            Assert.IsTrue(isOpen);
        }



        [Then(@"Korisnik simulira uspješno skeniranje QR koda materijala tako što klikne na gumb za isprobavanje uspješnog skeniranja")]
        public void ThenKorisnikJeSimuliraUspjesnoSkeniranjeQRKodaMaterijalaTakoStoKlikneNaGumbZaIsprobavanjeUspjesnogSkeniranja()
        {
            var driver = GuiDriver.GetDriver();
            var gumb = driver.FindElementByAccessibilityId("btnProba");
            gumb.Click();
        }


        [When(@"Korisnik upiše ""([^""]*)"" u numerički izbornik")]
        public void WhenKorisnikUpiseUNumerickiIzbornik(string broj)
        {
            var driver = GuiDriver.GetDriver();
            var numKolicina = driver.FindElementByAccessibilityId("numKolicina");

            numKolicina.SendKeys(broj);
        }


        [When(@"Korisniku se prikaže forma generirane primke")]
        public void WhenKorisnikuSePrikazeFormaGeneriranePrimke()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            Thread.Sleep(2000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            bool isOpen = driver.FindElementByAccessibilityId("FrmPrimka") != null;
            Assert.IsTrue(isOpen);
        }

        [When(@"Korisnik klikne na gumb Zatvori")]
        public void WhenKorisnikKlikneNaGumbZatvori()
        {
            var driver = GuiDriver.GetDriver();
            var gumb = driver.FindElementByAccessibilityId("btnZatvori");
            gumb.Click();

        }

        [Then(@"Količina skeniranog materijala je promjenjena u sustavu")]
        public void ThenKolicinaSkeniranogMaterijalaJePromjenjenaUSustavu()
        {
            var driver = GuiDriver.GetDriver();
            var dgvMat = driver.FindElementByAccessibilityId("dgvMaterijali");
            string kolAluminijaString = dgvMat.FindElementByName("Kolicina Row 5, Not sorted.").Text;
            int trenutnaKolicina = int.Parse(kolAluminijaString);
            Assert.IsTrue(trenutnaKolicina > kolAluminija);
        }



        [When(@"Korisnik klikne Ažuriraj količinu")]
        public void WhenKorisnikKlikneAzurirajKolicinu()
        {
            var driver = GuiDriver.GetDriver();
            var gumb = driver.FindElementByName("Ažuriraj količinu");
            gumb.Click();
        }

        [Then(@"Korisnik provjeri podudaranje podataka tako da je Količina: ""([^""]*)"", a Naziv: Aluminij")]
        public void ThenKorisnikProvjeriPodudaranjePodatakaTakoDaJeKolicinaADatumDanasnjiDatum(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var kolicina = driver.FindElementByAccessibilityId("txtKolicina").Text;
            var naziv = driver.FindElementByAccessibilityId("txtNaziv").Text;

            Assert.IsTrue(kolicina == "201" && naziv == "Aluminij");

        }

        [When(@"korisnik klikne na gumb Pohrani datoteku lokalno")]
        public void WhenKorisnikKlikneNaGumbPohraniDatotekuLokalno()
        {
            var driver = GuiDriver.GetDriver();
            var gumb = driver.FindElementByAccessibilityId("btnPohrani");
            gumb.Click();
        }


        [When(@"Korisnik odabere mjesto spremanja datoteke na Desktop i nazove datoteku ""([^""]*)""")]
        public void WhenKorisnikOdabereMjestoSpremanjaDatotekeNaDesktopINazoveDatoteku(string primka)
        {
            var driver = GuiDriver.GetDriver();
            var saveDialog = driver.FindElementByName("Save As");
            var desktop = driver.FindElementByName("Desktop");
            desktop.Click();

            var fileName = driver.FindElementByName("File name:");
            fileName.Click();
            var actions = new Actions(driver);
            actions.SendKeys("primka.pdf");
            actions.Perform();

            var btnSpremi = driver.FindElementByName("Save");
            btnSpremi.Click();

            Thread.Sleep(5000);


        }

        [When(@"Korisnik locira dokument na Desktopu i otvori ga")]
        public void WhenKorisnikLociraDokumentNaDesktopuIOtvoriGa()
        {
            
            var driver = GuiDriver.GetDriver();
            var filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "primka.pdf");
            Assert.IsTrue(File.Exists(filePath));

            var pdfReader = new PdfReader(filePath);
            var pdfText = PdfTextExtractor.GetTextFromPage(pdfReader, 1);
            pdfReader.Close();

            var kolicina = driver.FindElementByAccessibilityId("txtKolicina").Text;
            var naziv = driver.FindElementByAccessibilityId("txtNaziv").Text;
            var jedinica = driver.FindElementByAccessibilityId("txtMjernaJedinica").Text;

            Assert.IsTrue(pdfText.Contains("Naziv: " + naziv));
            Assert.IsTrue(pdfText.Contains("Kolicina: " + kolicina + " " + jedinica));
        }

        [Then(@"Prikazuje se poruka da je dokument uspješno spremljen lokalno s ispravnim podacima")]
        public void ThenPrikazujeSePorukaDaJeDokumentUspjesnoSpremljenLokalnoSIspravnimPodacima()
        {
            var driver = GuiDriver.GetDriver();
            var poruka = driver.FindElementByName("PDF dokument uspješno kreiran i spremljen.").Text;
            Assert.IsTrue(poruka == "PDF dokument uspješno kreiran i spremljen.");
            

        }
    }
}
