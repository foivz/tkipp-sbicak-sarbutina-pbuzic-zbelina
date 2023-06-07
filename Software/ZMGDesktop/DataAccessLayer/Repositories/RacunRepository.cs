using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class RacunRepository: Repository<Racun>, IRacunRepository
    {
        public RacunRepository(): base(new ZMGBaza())
        {

        }

        public override int Add(Racun entity, bool saveChanges = true)
        {
            var klijent = Context.Klijent.SingleOrDefault(k => k.Klijent_ID == entity.Klijent_ID);
           
            bool[] provjera = new bool[2];
            provjera[0] = false;
            provjera[1] = false;

            Usluga cincanje = null;
            Usluga niklanje = null;

            // provjera za attachanje za bazu podataka kako se ne bi duplicirla usluga i roba
            foreach (var stavka in entity.StavkaRacun)
            {
                if (provjera[0] == false && stavka.Usluga.Naziv == "Cincanje")
                {
                    provjera[0] = true;
                    cincanje = Context.Usluga.Attach(stavka.Usluga);
                }

                if (provjera[1] == false && stavka.Usluga.Naziv == "Niklanje")
                {
                    provjera[1] = true;
                    niklanje = Context.Usluga.Attach(stavka.Usluga);
                }

                if (stavka.Usluga.Naziv == "Cincanje")
                {
                    stavka.Usluga = cincanje;
                }

                if (stavka.Usluga.Naziv == "Niklanje")
                {
                    stavka.Usluga = niklanje;
                }

                Context.Roba.Attach(stavka.Roba);
            }

            var racun = InitRacun(entity, klijent);
            Entities.Add(racun);
            klijent.ukupniBrojRacuna = klijent.Racun.Count;
            return saveChanges ? SaveChanges() : 0;
        }

        private Racun InitRacun(Racun entity, Klijent klijent)
        {
            Racun racun = new Racun
            {
                Klijent = klijent,
                Poslodavac = Context.Poslodavac.SingleOrDefault(p => p.Poslodavac_ID == entity.Poslodavac_ID),
                Radnik = Context.Radnik.SingleOrDefault(r => r.Radnik_ID == entity.Radnik_ID),
                Fakturirao = entity.Fakturirao,
                Opis = entity.Opis,
                NacinPlacanja = entity.NacinPlacanja,
                UkupnaCijena = entity.UkupnaCijena,
                PDV = entity.PDV,
                UkupnoStavke = entity.UkupnoStavke,
                DatumIzdavanja = entity.DatumIzdavanja,
                StavkaRacun = entity.StavkaRacun,
                RokPlacanja = entity.RokPlacanja
            };

            return racun;
        }

        public IQueryable<Racun> DohvatiRacuneZaKlijenta(Klijent entity)
        {
            var query = from s in Entities.Include("Klijent").Include("Poslodavac").Include("Radnik").Include("StavkaRacun")
                        where s.Klijent_ID == entity.Klijent_ID
                        select s;
            return query;
        }

        public IQueryable<Racun> DohvatiSveRacune()
        {
            var query = from r in Entities.Include("Klijent").Include("Poslodavac").Include("Radnik") where r.Poslodavac_ID == 1 select r;
            return query;
        }

        public IQueryable<Racun> DohvatiOdredeniRacun(int id)
        {
            var query = from r in Entities.Include("Klijent").Include("Poslodavac").Include("Radnik") where r.Racun_ID == id select r;
            return query;
        }

        //pretrazivanje
        public IQueryable<Racun> DohvatiPremaPretrazivanju(Klijent entity, int Radnik_ID, PretrazivanjeSortiranje SearchSort)
        {
            int pretrazivanje = SearchSort.DohvatiPretrazivanje();
            int sortiranje = SearchSort.DohvatiSortiranje();

            var query = Entities.Include("Klijent").Include("Poslodavac").Include("Radnik")
                        .Where(r => r.Klijent_ID == entity.Klijent_ID);

            var klijent = Context.Klijent.SingleOrDefault(k => k.Klijent_ID == entity.Klijent_ID);
            // parametar pretrazivanje oznacava koji je radiobutton u toj grupi

            switch (pretrazivanje)
            {
                case 0:
                    query = sortiranje == 1 ? query.OrderByDescending(r => r.Racun_ID) : query.OrderBy(r => r.Racun_ID);
                    break;
                case 1:
                    query = sortiranje == 1 ? query.OrderByDescending(r => r.DatumIzdavanja) : query.OrderBy(r => r.DatumIzdavanja);
                    break;
                case 2:
                    query = sortiranje == 1 ? query.OrderByDescending(r => r.UkupnaCijena) : query.OrderBy(r => r.UkupnaCijena);
                    break;
                case 3:
                    query = query.Where(r => r.Radnik_ID == Radnik_ID);
                    query = sortiranje == 1 ? query.OrderByDescending(r => r.Racun_ID) : query.OrderBy(r => r.Racun_ID);
                    break;
            }
            return query;
        }

        public override int Update(Racun entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
