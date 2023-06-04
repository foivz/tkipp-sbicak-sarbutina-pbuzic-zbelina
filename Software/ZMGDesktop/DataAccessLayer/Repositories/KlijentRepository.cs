using DataAccessLayer.Iznimke;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class KlijentRepository : Repository<Klijent>, IKlijentRepository
    {
        public KlijentRepository() : base(new ZMGBaza())
        {

        }

        public override IQueryable<Klijent> GetAll()
        {
            var query = from s in Entities
                        select s;
            return query;
        }

        public IQueryable<Klijent> DohvatiDesetNajboljih()
        {
            var query = Entities.Where(x => x.Racun.Count > 0)
                     .OrderByDescending(x => x.Racun.Count)
                     .Take(10);
            return query;
        }

        public override int Add(Klijent entity, bool saveChanges = true)
        {
            var klijent = new Klijent
            {
                Naziv = entity.Naziv,
                Adresa = entity.Adresa,
                Mjesto = entity.Mjesto,
                OIB = entity.OIB,
                BrojTelefona = entity.BrojTelefona,
                IBAN = entity.IBAN,
                Email = entity.Email,
                ukupniBrojRacuna = 0,
            };
            vecPostoji(entity, klijent);
            Entities.Add(klijent);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        private void vecPostoji(Klijent entity, Klijent klijent)
        {
            provjeriNaziv(entity, klijent);
            provjeriOib(entity, klijent);
            provjeriIBAN(entity, klijent);
            provjeriEmail(entity, klijent);
            provjeriTelefon(entity, klijent);
        }

        private void provjeriTelefon(Klijent entity, Klijent klijent)
        {
            var telefon = Entities.SingleOrDefault(t => t.BrojTelefona == entity.BrojTelefona);
            if(telefon != null && telefon != klijent)
            {
                throw new TelefonException("Postoji vec klijent s ovim brojem telefona: " + telefon.BrojTelefona);
            }
        }

        private void provjeriEmail(Klijent entity, Klijent klijent)
        {
            var email = Entities.SingleOrDefault(e => e.Email == entity.Email);
            if(email != null && email != klijent)
            {
                throw new EmailException("Postoji vec klijent s ovim Email-om: " + email.Email);
            }
        }

        private void provjeriIBAN(Klijent entity, Klijent klijent)
        {
            var iban = Entities.SingleOrDefault(ib => ib.IBAN == entity.IBAN);
            if(iban != null && iban != klijent)
            {
                throw new IBANException("Postoji vec klijent s ovim IBAN-om: " + iban.IBAN);
            }
        }

        private void provjeriOib(Klijent entity, Klijent klijent)
        {
            var oib = Entities.SingleOrDefault(o => o.OIB == entity.OIB);
            if(oib != null && oib != klijent)
            {
                throw new OIBException("Postoji vec klijent s ovim OIB-om: " + oib.OIB);
            }
        }

        private void provjeriNaziv(Klijent entity, Klijent klijent)
        {
            var naziv = Entities.SingleOrDefault(n => n.Naziv == entity.Naziv);
            if(naziv != null && naziv != klijent)
            {
                throw new UserException("Postoji vec klijent s ovim nazivom: " + klijent.Naziv);
            }
        }

        public override int Update(Klijent entity, bool saveChanges = true)
        {

            var klijent = Entities.SingleOrDefault(k => k.Klijent_ID == entity.Klijent_ID);
            vecPostoji(entity, klijent);
            dodjeliSvojstva(entity, klijent);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        private void dodjeliSvojstva(Klijent entity, Klijent klijent)
        {
            klijent.Naziv = entity.Naziv;
            klijent.Adresa = entity.Adresa;
            klijent.BrojTelefona = entity.BrojTelefona;
            klijent.Mjesto = entity.Mjesto;
            klijent.OIB = entity.OIB;
            klijent.Email = entity.Email;
            klijent.IBAN = entity.IBAN;
        }

        public override int Remove(Klijent entity, bool saveChanges = true)
        {
            Entities.Attach(entity);
            if (provjeri(entity))
            {
                Entities.Remove(entity);
                if (saveChanges)
                {
                    return SaveChanges();
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                throw new BrisanjeKlijentaException("Zabranjeno brisanje klijenta. Klijent ima radne naloge, račune i robu");
            }
        }

        public bool provjeri(Klijent entity)
        {
            if(entity.Racun.Count == 0 && entity.RadniNalog.Count == 0 && entity.Roba.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
