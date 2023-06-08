﻿using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZMGDesktop
{
    public partial class FrmLogin : Form
    {
        private RadnikServices servis = new RadnikServices();
        int brojacNeuspjesnihPokusaja;
        public FrmLogin()
        {
            InitializeComponent();
            ucitajPomoc();
        }

        private void ucitajPomoc()
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private async void Login(object sender, EventArgs e)
        {
            if (brojacNeuspjesnihPokusaja >= 3) {
                MessageBox.Show("Prijava na korisnički račun je blokirana. Molimo kontaktirajte administratora.");
                return;
            }

            var korime = txtKorIme.Text;
            var lozinka = txtLozinka.Text;
            Radnik radnik = new Radnik
            {
                Korime = korime,
                Lozinka = lozinka
            };

            Radnik provjereniRadnik = await servis.ProvjeriRadnikaAsync(radnik);
            if (provjereniRadnik != null) {
                brojacNeuspjesnihPokusaja = 0;
                FrmPocetna pocetna = new FrmPocetna(provjereniRadnik);
                pocetna.Show();
                this.Hide();
            } else {
                brojacNeuspjesnihPokusaja++;
                MessageBox.Show("Krivi podaci!");
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                string path = Path.Combine(Application.StartupPath, "Pomoc\\Pomoc\\Prijava\\prijava.html");
                System.Diagnostics.Process.Start(path);
            }
        }
    }
}
