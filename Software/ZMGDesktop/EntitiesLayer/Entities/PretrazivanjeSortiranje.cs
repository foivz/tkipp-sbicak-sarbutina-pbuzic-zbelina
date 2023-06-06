using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities
{
    public class PretrazivanjeSortiranje
    {
        private int Pretrazivanje;
        private int Sortiranje;

        public PretrazivanjeSortiranje()
        {
            Reset();
        }

        public void PostaviPretrazivanje(int vrijednost)
        {
            Pretrazivanje = vrijednost;
        }

        public int DohvatiPretrazivanje()
        {
            return Pretrazivanje;
        }
        public void PostaviSortiranje(int vrijednost)
        {
            Sortiranje = vrijednost;
        }

        public int DohvatiSortiranje()
        {
            return Sortiranje;
        }

        public void Reset()
        {
            Pretrazivanje = 0;
            Sortiranje = 0;
        }
    }
}
