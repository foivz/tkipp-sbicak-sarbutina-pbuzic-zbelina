using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FakeItEasy;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using BusinessLogicLayer.Services;

namespace ZMGDesktop_Tests.sbicak20
{
    public class PoslodavacServices_Tests
    {
        [Fact]
        public void GetPoslodavac()
        {
            //arrange
            var fakeRepo = A.Fake<IPoslodavacRepository>();
            var fakePoslodavac = new List<Poslodavac>
            {
                new Poslodavac
                {
                    Poslodavac_ID = 1,
                    OIB = "123",
                    Naziv = "ZMG"
                }
                
            };

            var poslodavacService = new PoslodavacServices(fakeRepo);
            A.CallTo(() => fakeRepo.DohvatiPoslodavca(1)).Returns(fakePoslodavac.AsQueryable());
            //act
            var poslodavac = poslodavacService.GetPoslodavac();
            //assert
            Assert.NotNull(poslodavac);
            Assert.IsType<Poslodavac>(poslodavac);
            Assert.Equal("123", poslodavac.OIB);
            Assert.Equal("ZMG", poslodavac.Naziv);
            Assert.Equal(1, poslodavac.Poslodavac_ID);
        }
    }
}
