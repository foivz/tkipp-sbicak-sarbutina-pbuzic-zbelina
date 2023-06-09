using Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MailKit;
using BusinessLogicLayer.Services;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using BusinessLogicLayer.PDF;

namespace ZMGDesktop_Tests.sbicak20
{
    public class EmailAPI_Integration_Tests
    {
        private RacunService RacunService;
        private StavkaRacunService StavkaRacunService;

        private void kreirajServis()
        {
            RacunService = new RacunService(new RacunRepository());
            StavkaRacunService = new StavkaRacunService(new StavkaRepository());
        }
        [Fact]
        public void NapraviEmail_ProslijediSeStringFromToSubjectText_KosturEmailaJeNapravljen()
        {
            //arrange
            string from = "zastitametalnegalanterije@gmail.com";
            string to = "sebastijan.bicak@gmail.com";
            string subject = "Automatski test";
            string text = "Ovo je automatski generirani test.";
            //act
            int rezultat = EmailAPI.NapraviEmail(from, to, subject, text);
            //assert
            Assert.Equal(1, rezultat);
        }

        [Fact]
        public void DodajPrilog_ProslijediSePrilog_PrilogJeDodanUMail()
        {
            kreirajServis();
            //arrange
            string from = "zastitametalnegalanterije@gmail.com";
            string to = "sebastijan.bicak@gmail.com";
            string subject = "Automatski test";
            string text = "Ovo je automatski generirani test.";
            EmailAPI.NapraviEmail(from, to, subject, text);
            Racun racun = RacunService.DohvatiZadnjiRacun();
            List<StavkaRacun> listaStavki = StavkaRacunService.DohvatiStavkeRacuna(racun.Racun_ID);
            GeneriranjePDF.SacuvajPDF(racun, listaStavki);
            //act
            int rezultat = EmailAPI.DodajPrilog(GeneriranjePDF.nazivDatoteke);

            //assert
            Assert.Equal(1, rezultat);
        }

        [Fact]
        public void Posalji_SviPodaciSuIspravni_MailJePoslan()
        {
            kreirajServis();
            //arrange
            string from = "zastitametalnegalanterije@gmail.com";
            string to = "sebastijan.bicak@gmail.com";
            string subject = "Automatski test";
            string text = "Ovo je automatski generirani test.";
            EmailAPI.NapraviEmail(from, to, subject, text);
            Racun racun = RacunService.DohvatiZadnjiRacun();
            List<StavkaRacun> listaStavki = StavkaRacunService.DohvatiStavkeRacuna(racun.Racun_ID);
            GeneriranjePDF.SacuvajPDF(racun, listaStavki);
            EmailAPI.DodajPrilog(GeneriranjePDF.nazivDatoteke);
            //act
            int rezultat = EmailAPI.Posalji();

            //assert
            Assert.Equal(1, rezultat);
        }
    }
}
