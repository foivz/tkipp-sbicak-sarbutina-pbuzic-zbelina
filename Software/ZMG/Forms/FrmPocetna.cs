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
    public partial class FrmPocetna : Form
    {
        Radnik radnik;

        private RadnikServices servis = new RadnikServices();
        public FrmPocetna()
        {
            radnik = new Radnik
            {
                Korime = "sbicak20",
                Lozinka = "12345"
            };
            InitializeComponent();
            ucitajPomoc();
        }

        public FrmPocetna(Radnik provjereniRadnik)
        {
            radnik = new Radnik
            {
                Korime = "sbicak20",
                Lozinka = "12345"
            };
            this.FormClosing += new FormClosingEventHandler(MyForm_FormClosing);
            InitializeComponent();
            ucitajPomoc();
        }

        private void ucitajPomoc()
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void btnKlijenti_Click(object sender, EventArgs e)
        {
            FrmPregledKlijenata forma = new FrmPregledKlijenata();
            forma.ShowDialog();
        }

        private void btnRadniNalozi_Click(object sender, EventArgs e)
        {
            FrmPopisRadnihNaloga frmPopisRadnihNaloga = new FrmPopisRadnihNaloga(radnik);
            frmPopisRadnihNaloga.ShowDialog();
        }

        private void btnStanjeSkladista_Click(object sender, EventArgs e)
        {
            FrmKatalog katalog = new FrmKatalog();
            katalog.ShowDialog();
        }

        private void btnIzvjestaji_Click(object sender, EventArgs e)
        {
            FrmPopisIzvjestaja form = new FrmPopisIzvjestaja();
            form.ShowDialog();
        }

        private void MyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnRacuni_Click(object sender, EventArgs e)
        {
            FrmRacuni racuni = new FrmRacuni(radnik);
            racuni.Show();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                string path = Path.Combine(Application.StartupPath, "Pomoc\\Pomoc\\Klijenti\\Pocetna\\pocetna.html");
                System.Diagnostics.Process.Start(path);
            }
        }

        private async void FrmPocetna_Load(object sender, EventArgs e)
        {
            radnik = new Radnik
            {
                Korime = "sbicak20",
                Lozinka = "12345"
            };
            Radnik provjereniRadnik = await servis.ProvjeriRadnikaAsync(radnik);
            if(provjereniRadnik != null)
            {
                radnik = provjereniRadnik;
            }
        }
    }
}
