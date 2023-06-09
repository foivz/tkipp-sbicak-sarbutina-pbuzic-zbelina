using BusinessLogicLayer.PDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ZMGDesktop_Tests.sbicak20
{
    public class GeneriranjePDF_Tests
    {
        //BusinessLogicLayer.LogikaZaRacune.RacunanjeAPI
        [Fact]
        public void OtvoriPDF_NazivDatotekeJeNull_GeneriraniPDF()
        {
            //arrage
            GeneriranjePDF.nazivDatoteke = null;

            //act
            int rezultat = GeneriranjePDF.OtvoriPDF();

            //assert
            Assert.Equal(0, rezultat);
        }
    }
}
