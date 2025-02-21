﻿using BusinessLogicLayer.Services;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZMGDesktop {
    public partial class FrmLogin : Form {
        private RadnikServices servis = new RadnikServices(new RadnikRepository());
        private int brojacNeuspjesnihPokusaja;

        public FrmLogin() {
            InitializeComponent();
            UcitajPomoc();
        }

        private void UcitajPomoc() {
            this.KeyPreview = true;
            this.KeyDown += FrmLogin_KeyDown;
        }

        private async void Login(object sender, EventArgs e) {
            bool uspjehProvjere = ProvjeriBrojNeuspjesnihPokusaja(brojacNeuspjesnihPokusaja);
            if (uspjehProvjere == true) return;

            var korime = txtKorIme.Text;
            var lozinka = txtLozinka.Text;

            Radnik provjereniRadnik = await ProvjeriKorisnickePodatke(korime, lozinka);
            if (provjereniRadnik != null) {
                brojacNeuspjesnihPokusaja = 0;
                PrikaziFrmPocetna(provjereniRadnik);
            } else {
                brojacNeuspjesnihPokusaja++;
                PrikaziPorukuGreske("Krivi podaci!");
            }
        }

        private async Task<Radnik> ProvjeriKorisnickePodatke(string korime, string lozinka) {
            return await servis.ProvjeriRadnikaAsync(korime, lozinka);
        }

        private bool ProvjeriBrojNeuspjesnihPokusaja(int brojac) {
            if (brojac >= 3) {
                PrikaziPorukuGreske("Prijava na korisnički račun je blokirana. Molimo kontaktirajte administratora.");
                return true;
            } else return false;
        }

        private void PrikaziFrmPocetna(Radnik radnik) {
            FrmPocetna pocetna = new FrmPocetna(radnik);
            pocetna.Show();
            this.Hide();
        }

        private void PrikaziPorukuGreske(string poruka) {
            MessageBox.Show(poruka);
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F1) {
                PrikaziPomoc();
            }
        }

        private void PrikaziPomoc() {
            string path = Path.Combine(Application.StartupPath, "Pomoc\\Pomoc\\Prijava\\prijava.html");
            System.Diagnostics.Process.Start(path);
        }
    }
}
