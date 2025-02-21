﻿using BusinessLogicLayer.PDF;
using BusinessLogicLayer.Services;
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
    public partial class FrmDetaljniPregledRacun : Form
    {
        //objekti
        Racun racun;
        List<StavkaRacun> stavkeList;
        Poslodavac poslodavac;
        Klijent klijent;
        Radnik radnik;

        //servisi
        StavkaRacunService stavkaServis;
        public FrmDetaljniPregledRacun(Racun _racun)
        {
            InitializeComponent();
            ucitajPomoc();
            racun = _racun;
            poslodavac = racun.Poslodavac;
            klijent= racun.Klijent;
            radnik= racun.Radnik;

            stavkaServis = new StavkaRacunService(new StavkaRepository());
        }

        private void ucitajPomoc()
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void FrmDetaljniPregledRacun_Load(object sender, EventArgs e)
        {
            lblBrojRacuna.Text = racun.Racun_ID.ToString();
            InitPoslodavac();
            InitKlijent();
            InitStavke();
            InitUkupno();
            InitPlacanje();
            InitDatum();
            Osvjezi();
        }

        private void InitPoslodavac()
        {
            txtP_BrObrtnice.Text = poslodavac.BrojObrtnice;
            txtP_Banka.Text = poslodavac.Banka;
            txtP_BrojMobitela.Text = poslodavac.BrojTelefona;
            txtP_Drzava.Text = poslodavac.Drzava;
            txtP_Mjesto.Text = poslodavac.Mjesto;
            txtP_Ulica.Text = poslodavac.Adresa;
            txtP_TELFAX.Text = poslodavac.TEL_FAX;
            txtP_ObrtniRegistar.Text = poslodavac.UpisObrtniRegistar;
            txtP_Poslovnica.Text = poslodavac.Poslovnica;
            txtP_Naziv.Text = poslodavac.Naziv;
            txtP_IBAN.Text = poslodavac.IBAN;
            txtP_OIB.Text = poslodavac.OIB;
        }
        private void InitKlijent()
        {
            txtK_Adresa.Text = klijent.Adresa;
            txtK_Email.Text = klijent.Email;
            txtK_Mjesto.Text = klijent.Mjesto;
            txtK_Naziv.Text= klijent.Naziv;
            txtK_OIB.Text= klijent.OIB;
            txtK_TEL.Text = klijent.BrojTelefona;
            txtK_IBAN.Text = klijent.IBAN;
        }
        private void InitUkupno()
        {
            txtUkupno.Text = racun.UkupnoStavke.ToString();
            txtPDV.Text = racun.PDV.ToString();
            txtUkupniIznos.Text = racun.UkupnaCijena.ToString();
        }
        private void InitStavke()
        {
            stavkeList = stavkaServis.DohvatiStavkeRacuna(racun.Racun_ID);
        }

        private void Osvjezi()
        {
            dgvStavke.DataSource = stavkeList;
            dgvStavke.Columns[0].Visible = false;
            dgvStavke.Columns[1].Visible = false;
            dgvStavke.Columns[2].Visible = false;
            dgvStavke.Columns[9].Visible = false;
        }

        private void InitDatum()
        {
            txtDatumIzdavanja.Text = racun.DatumIzdavanja.Value.ToShortDateString();
            txtVrijeme.Text = racun.DatumIzdavanja.Value.ToShortTimeString();

            txtOpis.Text = racun.Opis;
        }
        private void InitPlacanje()
        {
            txtNacinPlacanja.Text = racun.NacinPlacanja;
            txtRokPlacanja.Text = racun.RokPlacanja;
            txtFakturirao.Text = racun.Fakturirao;
        }
        private void btnPDFpregled_Click(object sender, EventArgs e)
        {
            GeneriranjePDF.SacuvajPDF(racun, stavkeList);
            GeneriranjePDF.OtvoriPDF();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                string path = Path.Combine(Application.StartupPath, "Pomoc\\Pomoc\\Racuni\\DetaljiRacuna\\detaljiRacuna.html");
                System.Diagnostics.Process.Start(path);
            }
        }

        private void btnNatrag_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
