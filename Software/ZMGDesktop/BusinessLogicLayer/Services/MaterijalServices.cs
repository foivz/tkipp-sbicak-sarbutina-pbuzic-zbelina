using CsvHelper;
using CsvHelper.Configuration;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

                var materijali = _materijalRepository.GetAll().ToList();
                return materijali;
            
        }

        public bool ProvjeriQR(string qrKod)
        {
            
                var postoji = _materijalRepository.ProvjeriQR(qrKod);
                if (postoji) return true;
                else return false;
            
            
        }
        public Materijal AzurirajMaterijal(string qrKod, int kolicina)
        {
            Materijal materijal;

            
                materijal = _materijalRepository.Azuriraj(qrKod, kolicina);
            

            return materijal;
        }

        public bool ObrisiMaterijal(Materijal materijal)
        {
            bool uspjeh = false;

                
                    int redovi = _materijalRepository.Remove(materijal);
                    uspjeh = redovi > 0;
                
            

            return uspjeh;
        }

        public bool DodajMaterijal(Materijal materijal) {
            bool uspjeh = false;
            
                try {
                    _materijalRepository.Add(materijal);
                    uspjeh = true;
                } catch (InvalidOperationException ex) {
                throw;
            }
            
            return uspjeh;
        }

        public bool IzvozMaterijala() {
            var materijali = DohvatiMaterijale();
            if (materijali.Count > 0) {
                string generiraniString = GeneracijaCSV(materijali);
                if (generiraniString != string.Empty && generiraniString != null) return true;
                else return false;
            } else return false;

        }

        public string GeneracijaCSV(List<Materijal> materijali) {
            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) {
                csv.WriteRecords(materijali);
                csv.Flush();
                return writer.ToString();
            }
        }


    }
}
