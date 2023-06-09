using BusinessLogicLayer.Services;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ZMG.IntegrationTests.sbicak20_Integration
{
    public class PoslodavacRepository_Integration_Tests
    {
        private PoslodavacServices PoslodavacServices;

        private void kreirajServis()
        {
            PoslodavacServices = new PoslodavacServices(new PoslodavacRepository());
        }

        [Fact]
        public void DohvatiPoslodavca_PoslodavacPostojiUBazi_PoslodavacJeDohvacen()
        {
            //arrange
            kreirajServis();
            //act
            Poslodavac poslodavac = PoslodavacServices.GetPoslodavac();
            //assert
            Assert.NotNull(poslodavac);
            Assert.Equal(1, poslodavac.Poslodavac_ID);
            Assert.Equal("Republika Hrvatska", poslodavac.Drzava);
        }

        [Fact]

        public void Update_NijeImpelementiranaFunkcija_BacaSeGreska()
        {
            //arrange
            var poslodavac = new Poslodavac();  
            var poslodavacRepo = new PoslodavacRepository();
            //act
            Action act = () => poslodavacRepo.Update(poslodavac);
            //assert
            Assert.Throws<NotImplementedException>(() => act());
        }

    }
}
