﻿using BusinessLogicLayer.LogikaZaRacune;
using BusinessLogicLayer.PDF;
using BusinessLogicLayer.Services;
using DataAccessLayer.Repositories;
using Email;
using EntitiesLayer.Entities;
using EntitiesLayer.GlobalniObjekti;
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
    public partial class FrmIzdajNoviRacun : Form
    {
        //objekti
        Poslodavac poslodavac;
        Klijent selektiratiKlijent;
        Racun racun;
        Radnik radnik;
        BindingList<StavkaRacun> bindingList = new BindingList<StavkaRacun>(GlobalListaStavki.stavkaRacunaList);
        // servisi
        KlijentServices klijentServis;
        PoslodavacServices poslodavacServis;
        RacunanjeAPI racunanjeAPI;
        RacunService racunServis;
        public FrmIzdajNoviRacun(Poslodavac _poslodavac, Radnik _radnik)
        {
            InitializeComponent();
            ucitajPomoc();
            klijentServis= new KlijentServices(new KlijentRepository());
            poslodavacServis = new PoslodavacServices(new PoslodavacRepository());
            racunanjeAPI= new RacunanjeAPI();
            racunServis= new RacunService(new RacunRepository());

            poslodavac = _poslodavac;
            radnik = _radnik;
            racun = new Racun();
        }

        private void ucitajPomoc()
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }


        private void FrmIzdajNoviRacun_Load(object sender, EventArgs e)
        {
            poslodavac = poslodavacServis.GetPoslodavac();
            cmbKlijenti.DataSource = klijentServis.DohvatiKlijente();
            InitTextBoxPoslodavac(poslodavac);
            InitStaticniTxt();
            racun.Radnik = radnik;
        }

        private void InitStaticniTxt()
        {
            txtNacinPlacanja.Text = "(T) Transakcijski račun";
            txtRokPlacanja.Text = "do 15 dana";
            txtUkupniIznos.Text = "0";
            txtUkupno.Text = "0";
            txtPDV.Text = "0";
        }

        private void cmbKlijenti_SelectedIndexChanged(object sender, EventArgs e)
        {
            selektiratiKlijent = cmbKlijenti.SelectedItem as Klijent;
            InitTextBoxKlijent(selektiratiKlijent);
            GlobalListaStavki.stavkaRacunaList.Clear();
            Osvjezi();
        }

        // inicijalizacija textboxova
        private void InitTextBoxKlijent(Klijent klijent)
        {
            txtK_Adresa.Text = klijent.Adresa;
            txtK_Naziv.Text = klijent.Naziv;
            txtK_OIB.Text = klijent.OIB;
            txtK_Email.Text = klijent.Email;
            racun.Klijent = klijent;
        }

        private void InitTextBoxPoslodavac(Poslodavac poslodavac)
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
            racun.Poslodavac = poslodavac;
        }

        private void btnDodajStavke_Click(object sender, EventArgs e)
        {
            FrmDodajStavke formaStavki = new FrmDodajStavke(selektiratiKlijent, racun);
            formaStavki.FormClosing += new FormClosingEventHandler(ChildFormClosing);

            try
            {
                formaStavki.ShowDialog();
            }
            catch (System.IndexOutOfRangeException)
            {
                MessageBox.Show("Index je izvan dosega.", "Iznenadna greška.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        double ukupno, pdv, ukupnoiznos;
        private void ChildFormClosing(object sender, FormClosingEventArgs e)
        {
            Osvjezi();

            ukupno = racunanjeAPI.RacunanjeUkupnog(GlobalListaStavki.stavkaRacunaList);
            pdv = racunanjeAPI.RacunanjePDV(ukupno);
            ukupnoiznos = racunanjeAPI.RacunanjeUkupnogPDV(ukupno, pdv);

            Zaokruzi();
            PostaviTxtZaUkupno();
            PostaviUkupnoRacun();
        }

        private void Zaokruzi()
        {
            ukupno = Math.Round(ukupno, 2);
            pdv = Math.Round(pdv, 2);
            ukupnoiznos = Math.Round(ukupnoiznos, 2);
        }

        private void PostaviTxtZaUkupno()
        {
            txtUkupno.Text = ukupno.ToString();
            txtPDV.Text = pdv.ToString();
            txtUkupniIznos.Text = ukupnoiznos.ToString();
        }

        private void PostaviUkupnoRacun()
        {
            racun.UkupnaCijena = ukupnoiznos;
            racun.PDV = pdv;
            racun.UkupnoStavke = ukupno;
        }

        private void Poruka()
        {
            string from = $"{poslodavac.Email}";
            string to = $"{selektiratiKlijent.Email}";
            string subject = $"Račun {DateTime.Now}";
            string text = "Poštovani,<br> u prilogu se nalazi račun. Izdan je u elektroničnom obliku te valja bez potpisa.";
            EmailAPI.NapraviEmail(from, to, subject, text);
        }

        private async void btnIzdajRacun_Click(object sender, EventArgs e)
        {
            var izdajRacun = Task.Run(() => IzdajRacun());
            await izdajRacun;
            if (izdajRacun.Result)
            {
                Dispose();
            }
        }

        private bool IzdajRacun()
        {
            if (txtOpis.Text.Length >= 100)
            {
                MessageBox.Show("Opis mora biti manji od 100 znakova.", "Izdavanje računa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (GlobalListaStavki.stavkaRacunaList.Count != 0 && txtOpis.Text != "")
            {
                if (chkAutoEmail.Checked)
                {
                    InitRacun(racun);
                    racunServis.DodajRacun(racun);
                    racun = racunServis.DohvatiZadnjiRacun();
                    Poruka();
                    GeneriranjePDF.SacuvajPDF(racun, GlobalListaStavki.stavkaRacunaList);
                    EmailAPI.DodajPrilog(GeneriranjePDF.nazivDatoteke);
                    EmailAPI.Posalji();
                    MessageBox.Show($"Uspješno ste izdali račun i poslali klijentu na mail ({selektiratiKlijent.Email})", "Izdavanje računa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    InitRacun(racun);
                    racunServis.DodajRacun(racun);
                    MessageBox.Show("Uspješno ste izdali račun.", "Izdavanje računa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Morate imati barem jednu stavku i opis je obvezan.", "Izdavanje računa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPDFpregled_Click(object sender, EventArgs e)
        {
            GeneriranjePDF.SacuvajPDF(racun, GlobalListaStavki.stavkaRacunaList);
            GeneriranjePDF.OtvoriPDF();
        }

        private void Osvjezi()
        {
            this.Invalidate();
            BindingSource source = new BindingSource(bindingList, null);
            dgvStavke.DataSource = null;
            dgvStavke.DataSource = source;
            dgvStavke.Columns[0].Visible = false;
            dgvStavke.Columns[1].Visible = false;
            dgvStavke.Columns[2].Visible = false;
            dgvStavke.Columns[9].Visible = false;
        }

        private void InitRacun(Racun racun)
        {
            racun.NacinPlacanja = txtNacinPlacanja.Text;
            racun.RokPlacanja = txtRokPlacanja.Text;
            racun.StavkaRacun = GlobalListaStavki.stavkaRacunaList;
            racun.Fakturirao = radnik.ToString();
            racun.Poslodavac = poslodavac;
            racun.DatumIzdavanja = DateTime.Now;
            racun.Klijent = selektiratiKlijent;
            racun.Klijent_ID = selektiratiKlijent.Klijent_ID;
            racun.Poslodavac_ID = poslodavac.Poslodavac_ID;
            racun.Opis = txtOpis.Text;
            racun.UkupnaCijena = ukupnoiznos;
            racun.PDV = pdv;
            racun.UkupnoStavke = ukupno;
            racun.Radnik= radnik;
            racun.Radnik_ID = radnik.Radnik_ID;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                string path = Path.Combine(Application.StartupPath, "Pomoc\\Pomoc\\Racuni\\DodajRacun\\dodajRacun.html");
                System.Diagnostics.Process.Start(path);
            }
        }
    }
}
