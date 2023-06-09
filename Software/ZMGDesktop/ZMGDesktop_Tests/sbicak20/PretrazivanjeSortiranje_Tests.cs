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
    public class PretrazivanjeSortiranje_Tests
    {
        [Fact]
        public void PostaviPretrazivanje_PostavitiAtributPretrazivanjeNaBroj100_IspravnoPostavljenaVrijednost()
        {
            //arrange
            PretrazivanjeSortiranje searchSort = new PretrazivanjeSortiranje();
            //act
            searchSort.PostaviPretrazivanje(100);
            //assert
            Assert.Equal(100, searchSort.DohvatiPretrazivanje());
        }

        [Fact]
        public void PostaviSortiranje_PostavitiAtributSortiranjeNaBroj200_IspravnoPostavljenaVrijednost()
        {
            //arrange
            PretrazivanjeSortiranje searchSort = new PretrazivanjeSortiranje();
            //act
            searchSort.PostaviSortiranje(200);
            //assert
            Assert.Equal(200, searchSort.DohvatiSortiranje());
        }

        [Fact]
        public void Reset_AtributiCeSePostavitiNaNulu_AtributiSuIspravnoPostavljeni()
        {
            //arrange
            PretrazivanjeSortiranje searchSort = new PretrazivanjeSortiranje();
            searchSort.PostaviPretrazivanje(2);
            searchSort.PostaviSortiranje(213);
            int rezultat1 = searchSort.DohvatiPretrazivanje();
            int rezultat2 = searchSort.DohvatiSortiranje();
            //act
            searchSort.Reset();
            //assert
            Assert.Equal(2, rezultat1);
            Assert.Equal(213, rezultat2);
        }

        [Fact]
        public void DohvatiPretrazivanje_AtributPretrazivanjeJePostavljenNaBroj2_IspravnoDohvacanjeAtributaPretrazivanje()
        {
            //arrange
            PretrazivanjeSortiranje searchSort = new PretrazivanjeSortiranje();
            searchSort.PostaviPretrazivanje(2);
            //act
            int rezultat = searchSort.DohvatiPretrazivanje();
            //assert
            Assert.Equal(2, rezultat);
        }

        [Fact]
        public void DohvatiSortiranje_AtributSortiranjeJePostavljenNaBroj1_IspravnoDohvacanjeAtributaSortiranje()
        {
            //arrange
            PretrazivanjeSortiranje searchSort = new PretrazivanjeSortiranje();
            searchSort.PostaviSortiranje(1);
            //act
            int rezultat = searchSort.DohvatiSortiranje();
            //assert
            Assert.Equal(1, rezultat);
        }

        [Fact]
        public void PretrazivanjeSortiranje_PostavljeneVrijednostiUKontsruktoru_AtributiSuPostavljeniNaNula()
        {
            //arrange

            //act
            PretrazivanjeSortiranje searchSort = new PretrazivanjeSortiranje();
            //assert
            Assert.NotNull(searchSort);
            Assert.Equal(0, searchSort.DohvatiPretrazivanje());
            Assert.Equal(0, searchSort.DohvatiSortiranje());
        }

    }
}
