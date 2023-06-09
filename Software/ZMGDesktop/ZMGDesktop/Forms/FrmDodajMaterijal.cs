using BusinessLogicLayer.Services;
using ZMGDesktop.ValidacijaUnosa;
using EntitiesLayer.Entities;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static EntitiesLayer.Entities.Enumeracije;
using DataAccessLayer.Repositories;

namespace ZMGDesktop {
    public partial class FrmDodajMaterijal : Form {
        private MaterijalServices matServis = new MaterijalServices(new MaterijalRepository());
        private Validacija validacija = new Validacija();

        public FrmDodajMaterijal() {
            InitializeComponent();
            ucitajPomoc();
        }

        private void ucitajPomoc() {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void FrmDodajMaterijal_Load(object sender, EventArgs e) {
            UcitajMjerneJedinice();
        }

        private void UcitajMjerneJedinice() {
            cmbMjernaJedinica.DataSource = Enum.GetValues(typeof(MjerneJedinice));
        }

        private void btnNatrag_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnDodajMaterijal_Click(object sender, EventArgs e) {
            if (provjeriUnos()) {
                Materijal noviMaterijal = KreirajNoviMaterijal();
                DodajMaterijal(noviMaterijal);
                ZatvoriFormu();
            }
        }

        private Materijal KreirajNoviMaterijal() {
            string naziv = txtNaziv.Text;
            int kolicina = (int)txtKolicina.Value;
            string odabranaJedinica = cmbMjernaJedinica.SelectedItem.ToString();
            float cijena = (float)txtCijena.Value;
            string opis = txtOpis.Text;
            bool opasno = txtOpasno.Checked;
            string qr_kod = GenerirajRandomString();

            Materijal noviMaterijal = new Materijal {
                Naziv = naziv,
                Kolicina = kolicina,
                JedinicaMjere = odabranaJedinica,
                CijenaMaterijala = cijena,
                Opis = opis,
                OpasnoPoZivot = opasno,
                QR_kod = qr_kod
            };

            return noviMaterijal;
        }

        private void DodajMaterijal(Materijal materijal) {
            matServis.dodajMaterijal(materijal);
        }

        private void ZatvoriFormu() {
            this.Close();
        }

        private string GenerirajRandomString() {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 20)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private bool provjeriUnos() {
            if (!ProvjeriPolja())
                return false;

            if (!ProvjeriNaziv())
                return false;

            if (!ProvjeriCijenu())
                return false;

            return true;
        }

        private bool ProvjeriPolja() {
            if (txtNaziv.Text == "" || txtCijena.Value == 0 || txtKolicina.Value == 0 || cmbMjernaJedinica.SelectedItem == null || string.IsNullOrEmpty(txtOpis.Text)) {
                MessageBox.Show("Potrebno je ispuniti sva polja", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ProvjeriNaziv() {
            if (!validacija.provjeraSamoSlova(txtNaziv.Text)) {
                MessageBox.Show("Naziv može sadržavati samo slova");
                return false;
            }
            return true;
        }

        private bool ProvjeriCijenu() {
            if (!validacija.provjeraSamoBrojevi(txtCijena.Value.ToString())) {
                MessageBox.Show("Cijena može sadržavati samo brojeve");
                return false;
            }
            return true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F1) {
                string path = Path.Combine(Application.StartupPath, "Pomoc\\Pomoc\\Skladiste\\DodajMaterijal\\dodajMaterijal.html");
                System.Diagnostics.Process.Start(path);
            }
        }
    }
}

