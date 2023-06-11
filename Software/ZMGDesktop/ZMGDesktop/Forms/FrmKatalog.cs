using BusinessLogicLayer.Services;
using DataAccessLayer.Iznimke;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZMGDesktop {
    public partial class FrmKatalog : Form {
        private readonly UslugaServices uslugaServis;
        private readonly MaterijalServices matServis;
        private readonly RobaService robaService;

        public FrmKatalog() {
            InitializeComponent();
            uslugaServis = new UslugaServices(new UslugaRepository());
            matServis = new MaterijalServices(new MaterijalRepository());
            robaService = new RobaService(new RobaRepository());
            ucitajPomoc();
        }

        private void ucitajPomoc() {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void btnDodaj_Click(object sender, EventArgs e) {
            FrmDodajMaterijal dodaj = new FrmDodajMaterijal();
            dodaj.ShowDialog();
            OsvjeziPrikazMaterijala();
        }

        private async void btnObrisi_Click(object sender, EventArgs e) {
            var odabraniMaterijal = dgvMaterijali.CurrentRow.DataBoundItem as Materijal;
            if (odabraniMaterijal != null) {
                try {
                    await Task.Run(() => matServis.ObrisiMaterijal(odabraniMaterijal));
                } catch (BrisanjeMaterijalaException iznimka) {
                    MessageBox.Show(iznimka.Poruka, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                OsvjeziPrikazMaterijala();
            }
        }

        private void btnZaprimi_Click(object sender, EventArgs e) {
            FrmZaprimiMaterijal zaprimi = new FrmZaprimiMaterijal();
            zaprimi.ShowDialog();
            OsvjeziPrikazMaterijala();
        }

        private void btnNatrag_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void FrmKatalog_Load(object sender, EventArgs e) {
            OsvjeziPrikazMaterijala();
            OsvjeziPrikazUsluga();
            UcitajRobu();
        }

        private void UcitajRobu()
        {
            var roba = robaService.DohvatiSvuRobu();
            dgvRoba.DataSource = roba;
            dgvRoba.Columns[5].Visible = false;
            dgvRoba.Columns[6].Visible = false;
        }

        private async void OsvjeziPrikazUsluga() {
            var usluge = await Task.Run(() => uslugaServis.DohvatiUsluge());

            dgvUsluge.DataSource = usluge;
            dgvUsluge.Columns["StavkaRacun"].Visible = false;
        }

        private async void OsvjeziPrikazMaterijala() {
            var materijali = await Task.Run(() => matServis.DohvatiMaterijale());

            dgvMaterijali.DataSource = materijali;
            dgvMaterijali.Columns["Primka_ID"].Visible = false;
            dgvMaterijali.Columns["Usluga_ID"].Visible = false;
            dgvMaterijali.Columns["Primka"].Visible = false;
            dgvMaterijali.Columns["Usluga"].Visible = false;
            dgvMaterijali.Columns["RadniNalog"].Visible = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F1) {
                string path = Path.Combine(Application.StartupPath, "Pomoc\\Pomoc\\Skladiste\\Skladiste\\skladiste.html");
                System.Diagnostics.Process.Start(path);
            }
        }

        private void btnIzvoz_Click(object sender, EventArgs e) {
            var generiraniCSV = matServis.IzvozMaterijala();
            PohraniCSV(generiraniCSV);
        }

        private void PohraniCSV(string generiraniCSV) {
            try {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV datoteke (*.csv)|*.csv";
                saveFileDialog.Title = "Odaberi mjesto za spremanje CSV datoteke";
                saveFileDialog.FileName = "generirani_podaci.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    string filePath = saveFileDialog.FileName;

                    // Spremanje generiranog CSV-a na odabrano mjesto
                    File.WriteAllText(filePath, generiraniCSV);

                    MessageBox.Show("CSV datoteka je uspješno spremljena.");
                }
            } catch (Exception ex) {
                MessageBox.Show("Došlo je do greške prilikom spremanja CSV datoteke: " + ex.Message);
            }
        }
    }
}

