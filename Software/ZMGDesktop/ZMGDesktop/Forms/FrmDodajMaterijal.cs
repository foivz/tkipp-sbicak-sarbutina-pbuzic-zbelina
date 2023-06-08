using BusinessLogicLayer.Services;
using DataAccessLayer.Iznimke;
using ZMGDesktop.ValidacijaUnosa;
using EntitiesLayer.Entities;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ZMGDesktop.ValidacijaUnosa;
using static EntitiesLayer.Entities.Enumeracije;
using System.Windows.Markup;
using System.Drawing;

namespace ZMGDesktop
{
    public partial class FrmDodajMaterijal : Form
    {
        MaterijalServices matServis = new MaterijalServices();
        private Validacija validacija = new Validacija();
        public FrmDodajMaterijal()
        {
            InitializeComponent();
            ucitajPomoc();
        }

        private void ucitajPomoc()
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void btnNatrag_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmDodajMaterijal_Load(object sender, EventArgs e)
        {
            UcitajMjerneJedinice();
        }

        private void UcitajMjerneJedinice()
        {
            cmbMjernaJedinica.DataSource = Enum.GetValues(typeof(MjerneJedinice));
        }

        private void btnDodajMaterijal_Click(object sender, EventArgs e)
        {
            if(provjeriUnos()) {
                string naziv = txtNaziv.Text;
                int kolicina = (int)txtKolicina.Value;
                string odabranaJedinica = cmbMjernaJedinica.SelectedItem.ToString();
                float cijena = (float)txtCijena.Value;
                string opis = txtOpis.Text;
                bool opasno = txtOpasno.Checked;

                string qr_kod = GenerirajRandomString();



                Materijal noviMaterijal = new Materijal
                {
                    Naziv = naziv,
                    Kolicina = kolicina,
                    JedinicaMjere = odabranaJedinica,
                    CijenaMaterijala = cijena,
                    Opis = opis,
                    OpasnoPoZivot = opasno,
                    QR_kod = qr_kod
                };
                matServis.dodajMaterijal(noviMaterijal);
                this.Close();
            }
        }


        string GenerirajRandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 20)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        private bool provjeriUnos()
        {
            if (txtNaziv.Text == "" || txtCijena.Value == 0 || txtKolicina.Value == 0 || cmbMjernaJedinica.SelectedItem == null || txtOpis.Text == null)
            {
                MessageBox.Show("Potrebno je ispuniti sva polja", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!validacija.provjeraSamoSlova(txtNaziv.Text))
            {
                MessageBox.Show("Naziv može sadržavati samo slova");
                return false;
            }
            if (!validacija.provjeraSamoBrojevi(txtCijena.Value.ToString())) {
                MessageBox.Show("Cijena može sadržavati samo brojeve");
                return false;
            }
            return true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                string path = Path.Combine(Application.StartupPath, "Pomoc\\Pomoc\\Skladiste\\DodajMaterijal\\dodajMaterijal.html");
                System.Diagnostics.Process.Start(path);
            }
        }
    }
}
