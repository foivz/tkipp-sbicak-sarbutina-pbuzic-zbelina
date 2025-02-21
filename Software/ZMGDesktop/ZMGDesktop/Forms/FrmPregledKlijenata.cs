﻿using BusinessLogicLayer.Services;
using DataAccessLayer.Iznimke;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZMGDesktop
{
    public partial class FrmPregledKlijenata : Form
    {
        private KlijentServices servis = new KlijentServices(new KlijentRepository());

        private Timer timer = new Timer();
        
        public FrmPregledKlijenata()
        {
            InitializeComponent();
            ucitajPomoc();
            timer.Interval = 1300;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            string pretrazi = txtSearch.Text;
            var pretrazeniKlijenti = servis.Pretrazi(pretrazi);
            dgvKlijenti.DataSource = pretrazeniKlijenti;
        }

        private void ucitajPomoc()
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void FrmPregledKlijenata_Load(object sender, EventArgs e)
        {
            ucitajKlijente();
        }

        private void ucitajKlijente()
        {
            var klijenti = servis.DohvatiKlijente();
            dgvKlijenti.DataSource = klijenti;
            dgvKlijenti.Columns[8].Visible = false;
            dgvKlijenti.Columns[9].Visible = false;
            dgvKlijenti.Columns[10].Visible = false;
            dgvKlijenti.Columns[11].Visible = false;
        }

        private void btnDetaljiKlijenta_Click(object sender, EventArgs e)
        {
            var selektiran = dohvatiSelektiranog();
            if(selektiran != null)
            {
                FrmDetaljniPrikazKlijenta forma = new FrmDetaljniPrikazKlijenta(selektiran);
                forma.ShowDialog();
            }
        }

        private Klijent dohvatiSelektiranog()
        {
            return dgvKlijenti.CurrentRow.DataBoundItem as Klijent;
        }

        private void btnDodajKlijenta_Click(object sender, EventArgs e)
        {
            FrmDodajKlijenta forma = new FrmDodajKlijenta();
            forma.ShowDialog();
            ucitajKlijente();
        }

        private void btnIzbrisiKlijenta_Click(object sender, EventArgs e)
        {
            var selektiran = dohvatiSelektiranog();
            try
            {
                if (selektiran != null)
                {
                    izbrisiKlijenta(selektiran);
                }
            }
            catch (BrisanjeKlijentaException ex)
            {
                MessageBox.Show(ex.Poruka);
            }
           
        }

        private void izbrisiKlijenta(Klijent selektiran)
        {
            servis.Remove(selektiran);
            ucitajKlijente();
        }

        private void btnUrediKlijenta_Click(object sender, EventArgs e)
        {
            var selektiran = dohvatiSelektiranog();
            if(selektiran != null)
            {
                FrmDodajKlijenta forma = new FrmDodajKlijenta(selektiran);
                forma.ShowDialog();
                ucitajKlijente();
            }
        }

        private void btnNatrag_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUveziKlijenta_Click(object sender, EventArgs e)
        {
            FrmXML form = new FrmXML();
            form.ShowDialog();
            ucitajKlijente();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                string path = Path.Combine(Application.StartupPath, "Pomoc\\Pomoc\\Klijenti\\PregledKlijenta\\Pregled.html");
                System.Diagnostics.Process.Start(path);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Start();
        }

        private void SortirajKlijentePoUkupnomBrojuRacuna()
        {
            var sortiraniKlijenti = servis.SortirajKlijentePoUkupnomBrojuRacuna();
            dgvKlijenti.DataSource = sortiraniKlijenti;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SortirajKlijentePoUkupnomBrojuRacuna();
        }
    }
}
