using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class PrimkaRepository : Repository<Primka>, IPrimkaRepository
    {
        public PrimkaRepository() : base(new ZMGBaza())
        {

        }

        public override int Add(Primka entity, bool saveChanges = true) {
            CheckIfPrimkaExists(entity);

            var primka = CreateNewPrimka(entity);

            AddPrimkaToCollection(primka);

            if (saveChanges) {
                return SaveChanges();
            }

            return 0;
        }

        private void CheckIfPrimkaExists(Primka entity) {
            var existingPrimka = Entities.SingleOrDefault(k => k.Primka_ID == entity.Primka_ID);
            if (existingPrimka != null) {
                throw new InvalidOperationException("Primka već postoji");
            }
        }

        private Primka CreateNewPrimka(Primka entity) {
            return new Primka {
                Naziv_Materijal = entity.Naziv_Materijal,
                Kolicina = entity.Kolicina,
                Datum = entity.Datum
            };
        }

        private void AddPrimkaToCollection(Primka primka) {
            Entities.Add(primka);
        }


        public override int Update(Primka entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }


    }
}
