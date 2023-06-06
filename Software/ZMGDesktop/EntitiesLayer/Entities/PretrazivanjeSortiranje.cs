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
            Pretrazivanje = 0;
            Sortiranje = 0;
        }

        public void PostaviPretrazivanje(int vrijednost)
        {
            Pretrazivanje = vrijednost;
        }
        public void PostaviSortiranje(int vrijednost)
        {
            Sortiranje = vrijednost;
        }
    }
}
