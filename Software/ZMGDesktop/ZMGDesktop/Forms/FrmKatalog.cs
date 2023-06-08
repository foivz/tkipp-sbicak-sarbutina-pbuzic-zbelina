﻿using BusinessLogicLayer.Services;
using DataAccessLayer.Iznimke;
using DataAccessLayer.Repositories;
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
    public partial class FrmKatalog : Form
    {
        UslugaServices uslugaServis = new UslugaServices(new UslugaRepository());
        MaterijalServices matServis = new MaterijalServices(new MaterijalRepository());
        RobaService robaService = new RobaService(new RobaRepository());
        public FrmKatalog()
        {
            InitializeComponent();
            ucitajPomoc();
        }

        private void ucitajPomoc()
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            FrmDodajMaterijal dodaj = new FrmDodajMaterijal();
            dodaj.ShowDialog();
            PrikaziMaterijale();
            Update();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            var odabraniMaterijal = dgvMaterijali.CurrentRow.DataBoundItem as Materijal;
            if(odabraniMaterijal != null)
            {
                try
                {
                    matServis.obrisiMaterijal(odabraniMaterijal);
                }
                catch (BrisanjeMaterijalaException iznimka)
                {
                    MessageBox.Show(iznimka.Poruka, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

               
                
                PrikaziMaterijale();
            }
        }

        private void btnZaprimi_Click(object sender, EventArgs e)
        {
            FrmZaprimiMaterijal zaprimi = new FrmZaprimiMaterijal();
            zaprimi.ShowDialog();
            PrikaziMaterijale();
        }

        private void btnNatrag_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmKatalog_Load(object sender, EventArgs e)
        {
            PrikaziMaterijale();
            PrikaziUsluge();
            UcitajRobu();
        }

        private void UcitajRobu()
        {
            var roba = robaService.DohvatiSvuRobu();
            dgvRoba.DataSource = roba;
            dgvRoba.Columns[5].Visible = false;
            dgvRoba.Columns[6].Visible = false;
        }

        private void PrikaziUsluge()
        {
            var usluge =  uslugaServis.DohvatiUsluge();
            dgvUsluge.DataSource = usluge;
            dgvUsluge.Columns["StavkaRacun"].Visible = false;
        }

        private void PrikaziMaterijale()
        {
            var materijali =  matServis.DohvatiMaterijale();

            dgvMaterijali.DataSource = materijali;
            dgvMaterijali.Columns["Primka_ID"].Visible = false;
            dgvMaterijali.Columns["Usluga_ID"].Visible = false;
            dgvMaterijali.Columns["Primka"].Visible = false;
            dgvMaterijali.Columns["Usluga"].Visible = false;
            dgvMaterijali.Columns["RadniNalog"].Visible = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                string path = Path.Combine(Application.StartupPath, "Pomoc\\Pomoc\\Skladiste\\Skladiste\\skladiste.html");
                System.Diagnostics.Process.Start(path);
            }
        }
    }
}
