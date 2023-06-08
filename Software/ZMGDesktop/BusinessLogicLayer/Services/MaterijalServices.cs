using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class MaterijalServices
    {

        private readonly IMaterijalRepository _materijalRepository;

        public MaterijalServices(IMaterijalRepository materijalRepository)
        {
            this._materijalRepository = materijalRepository;
        }


        public List<Materijal> DohvatiMaterijale()
        {
            using (var repo = new MaterijalRepository())
            {
                var materijali = _materijalRepository.GetAll().ToList();
                return materijali;
            }
        }

        public bool ProvjeriQR(string qrKod)
        {
            using(var repo = new MaterijalRepository())
            {
                var postoji = _materijalRepository.ProvjeriQR(qrKod);
                if (postoji) return true;
                else return false;
            }
            
        }
        public Materijal AzurirajMaterijal(string qrKod, int kolicina)
        {
            Materijal materijal;

            using (var repo = new MaterijalRepository())
            {
                materijal = _materijalRepository.Azuriraj(qrKod, kolicina);
            }

            return materijal;
        }

        public bool obrisiMaterijal(Materijal materijal)
        {
            bool uspjeh = false;

                using (var repo = new MaterijalRepository())
                {
                    int redovi = _materijalRepository.Remove(materijal);
                    uspjeh = redovi > 0;
                }
            

            return uspjeh;
        }

        public bool dodajMaterijal(Materijal materijal)
        {
            bool uspjeh = false;
            using (var repo = new MaterijalRepository())
            {
                int affectedRows = _materijalRepository.Add(materijal);
                uspjeh = affectedRows > 0;
            }
            return uspjeh;
        }


    }
}
