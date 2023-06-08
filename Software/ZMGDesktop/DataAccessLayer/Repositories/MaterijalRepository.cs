using DataAccessLayer.Iznimke;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class MaterijalRepository : Repository<Materijal> {
        public MaterijalRepository() : base(new ZMGBaza()) {

        }

        public bool PostojiMaterijal(string naziv) {
            return Entities.Any(k => k.Naziv == naziv);
        }

        public bool ProvjeriQR(string qrKod) {
            var postoji = Entities.SingleOrDefault(k => k.QR_kod == qrKod);
            return postoji != null;
        }

        public Materijal Azuriraj(string qrKod, int kolicina) {
            var postoji = PronadjiMaterijal(qrKod);

            if (postoji != null) {
                postoji.Kolicina += kolicina;
                SaveChanges();
            }

            return postoji;
        }

        private Materijal PronadjiMaterijal(string qrKod) {
            return Entities.SingleOrDefault(k => k.QR_kod == qrKod);
        }

        public override int Add(Materijal entity, bool saveChanges = true) {
            if (PostojiMaterijal(entity.Naziv)) {
                throw new InvalidOperationException("Materijal već postoji");
            }

            var materijal = MapirajMaterijal(entity);
            Entities.Add(materijal);

            if (saveChanges) {
                return SaveChanges();
            }

            return 0;
        }

        private Materijal MapirajMaterijal(Materijal entity) {
            string provjereniOpis = entity.Opis?.ToString() ?? " ";
            bool opasno = entity.OpasnoPoZivot != null;

            var materijal = new Materijal {
                Naziv = entity.Naziv,
                CijenaMaterijala = entity.CijenaMaterijala,
                JedinicaMjere = entity.JedinicaMjere,
                Opis = provjereniOpis,
                OpasnoPoZivot = opasno,
                Kolicina = entity.Kolicina,
                QR_kod = entity.QR_kod
            };

            return materijal;
        }

        public override int Remove(Materijal entity, bool saveChanges = true) {
            if (MozeSeBrisati(entity)) {
                Entities.Remove(entity);

                if (saveChanges) {
                    return SaveChanges();
                }

                return 0;
            }

            throw new BrisanjeMaterijalaException("Zabranjeno brisanje materijala koji se nalazi u radnom nalogu ili primci.");
        }

        private bool MozeSeBrisati(Materijal entity) {
            return entity.RadniNalog.Count == 0 && entity.Primka == null && entity.Usluga == null;
        }

        public override int Update(Materijal entity, bool saveChanges = true) {
            throw new NotImplementedException();
        }
    }

}
