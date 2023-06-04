using BusinessLogicLayer.Services;
using DataAccessLayer.Iznimke;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZMGDesktop;

namespace ZMGDesktop_Tests
{
    public class ZMGDesktop_Tests
    {
        [Fact]
        public void UcitajKlijente_KlijentiPostojeuBazi_KlijentiSePrikazujuUSustavu()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            var ocekivaniKlijenti = new List<Klijent> { new Klijent { Naziv = "Klijent 1" }, new Klijent { Naziv = "Klijent 2" } };
            A.CallTo(() => fakeRepo.GetAll()).Returns(ocekivaniKlijenti.AsQueryable());

            var fakeServis = new KlijentServices(fakeRepo);

            var klijenti = fakeServis.DohvatiKlijente();

            Assert.NotNull(ocekivaniKlijenti);
            Assert.Equal(klijenti, ocekivaniKlijenti);
        }

        [Fact]
        public void Remove_KlijentNijeSelektiran_VracaFalse()
        {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var fakeServis = new KlijentServices(fakeRepo);
            A.CallTo(() => fakeRepo.Remove(null, true)).Returns(0);

            //Act
            var dohvacen = fakeServis.Remove(null);

            //Assert
            Assert.False(dohvacen);
        }

        [Fact]
        public void Remove_KlijentImaRacun_VracaFalse()
        {
            //Arrange
            var fakeRepo = A.Fake<IKlijentRepository>();
            var servis = new KlijentServices(fakeRepo);
            var racun = new Racun { Fakturirao = "Netko" };
            var klijent = new Klijent { Naziv = "Klijent 1" };
            racun.Klijent = klijent;
            klijent.Racun.Add(racun);
            A.CallTo(() => fakeRepo.provjeri(klijent)).Returns(false);

            //Act
            bool istina = servis.Remove(klijent);

            //Assert
            Assert.False(istina);
        }

        [Fact]
        public void Remove_KlijentImaRadniNalog_BacaGresku()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = new Klijent { Naziv = "Ime" };
            A.CallTo(() => fakeRepo.provjeri(klijent)).Returns(false);
            var fakeServis = new KlijentServices(fakeRepo);
            //
            Action act = () => fakeServis.Remove(klijent);

            //Assert
            Assert.Throws<BrisanjeKlijentaException>(() => act());
        }

        [Fact]
        public void Add_IspunjeniSviPodaciZaKlijenta_KlijentUspjesnoDodan()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = new Klijent
            {
                Naziv = "Klijent",
                OIB = "21694361376",
                Adresa = "Lobor 35",
                IBAN = "HR6324020061696361472",
                BrojTelefona = "0976401323",
                Email = "email@gmail.com",
                Mjesto = "Lobor",
            };
            var servis = new KlijentServices(fakeRepo);
            A.CallTo(() => fakeRepo.Add(klijent, true)).Returns(1);

            //Act
            bool ubacen = servis.Add(klijent);

            //Assert
            Assert.True(ubacen);
            A.CallTo(() => fakeRepo.Add(klijent, true)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Add_KlijentSNeispravnimImenom_BacaGresku()
        {
            var fakeRepo = A.Fake<IKlijentRepository>();
            var klijent = new Klijent
            {
                Naziv = "Klijent",
                OIB = "21694361376",
                Adresa = "Lobor 35",
                IBAN = "HR6324020061696361472",
                BrojTelefona = "0976401323",
                Email = "email@gmail.com",
                Mjesto = "Lobor"
            };
            var servis = new KlijentServices(fakeRepo);


            A.CallTo(() => fakeRepo.Add(klijent, true)).Returns(0);

            //Act
            Action act = () => servis.Add(klijent);

            //Assert
            Assert.Throws<UserException>(() => act());
        }

    }
}
