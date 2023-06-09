using BusinessLogicLayer.LogikaZaRacune;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ZMGDesktop_Tests.sbicak20
{
    public class RacunanjeAPI_Tests
    {
        //BusinessLogicLayer.LogikaZaRacune.RacunanjeAPI
        [Fact]
        public void RacunanjeUkupnog_ProsljedenaNullStavka_IznosjeNula()
        {
            //arrange
            RacunanjeAPI racunanjeAPI = new RacunanjeAPI();
            List<StavkaRacun> lista = null;

            //act
            var ukupno = racunanjeAPI.RacunanjeUkupnog(lista);

            //assert
            Assert.Equal(0, ukupno);
        }

        [Fact]
        public void RacunanjeUkupnog_ProsljedenaListaSDvijeStavke_IznosJeIspravan()
        {
            //arrange
            RacunanjeAPI racunanje = new RacunanjeAPI();
            List<StavkaRacun> lista = new List<StavkaRacun>
            {
                new StavkaRacun
                {
                    UkupnaCijenaStavke = 300
                },
                new StavkaRacun
                {
                    UkupnaCijenaStavke = 300
                }

            };
            //act
            double ukupno = racunanje.RacunanjeUkupnog(lista);
            //assert
            Assert.True(ukupno == 600);
        }

        [Fact]
        public void RacunanjePDV_ProslijedimoBroj4_IznosJe1()
        {
            //arrange
            RacunanjeAPI racunanjeAPI = new RacunanjeAPI();

            ////act
            var ukupno = racunanjeAPI.RacunanjePDV(4);

            //assert
            Assert.True(ukupno == 1);
        }

        [Fact]
        public void RacunanjeUkupnogPDV_ProslijedimoBroj4i1_IznosJe5()
        {
            //arrange
            RacunanjeAPI racunanjeAPI = new RacunanjeAPI();

            //act
            var ukupno = racunanjeAPI.RacunanjeUkupnogPDV(4, 1);

            //assert
            Assert.True(ukupno == 5);
        }
    }
}
