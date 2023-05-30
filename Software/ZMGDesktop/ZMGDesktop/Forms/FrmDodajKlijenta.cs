using BusinessLogicLayer.Services;
using DataAccessLayer.Iznimke;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZMGDesktop.ValidacijaUnosa;

namespace ZMGDesktop
{
    public partial class FrmDodajKlijenta : Form
    {
        private KlijentServices servisKlijent = new KlijentServices();
        private Klijent selektiran;
        private Validacija validacija = new Validacija();
        public FrmDodajKlijenta()
        {
            InitializeComponent();
            ucitajPomoc();
        }

        private void ucitajPomoc()
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        public FrmDodajKlijenta(Klijent klijent)
        {
            InitializeComponent();
            ucitajPomoc();
            this.selektiran = klijent;
        }

        private void btnNatrag_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (provjeriUnos())
            {
                try
                {
                    if (selektiran == null)
                    {
                        dodajKlijenta();
                        Close();
                    }
                    else
                    {
                        azurirajKlijenta(selektiran);
                        Close();
                    }
                }
                catch (UserException ex)
                {
                    MessageBox.Show(ex.Poruka);
                }
                catch (TelefonException ex)
                {
                    MessageBox.Show(ex.Poruka);
                }
                catch (IBANException ex)
                {
                    MessageBox.Show(ex.Poruka);
                }
                catch (OIBException ex)
                {
                    MessageBox.Show(ex.Poruka);
                }
                catch (EmailException ex)
                {
                    MessageBox.Show(ex.Poruka);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void azurirajKlijenta(Klijent selektiran)
        {
            selektiran.Naziv = txtNaziv.Text;
            selektiran.OIB = txtOIB.Text;
            selektiran.Adresa = txtAdresa.Text;
            selektiran.IBAN = txtIBAN.Text;
            selektiran.Mjesto = txtMjesto.Text;
            selektiran.BrojTelefona = txtTelefon.Text;
            selektiran.Email = txtEmail.Text;
            servisKlijent.Update(selektiran);
        }

        private bool provjeriUnos()
        {
            if(!provjeriPolja())
            {
                return false;
            }
            provjeriNaziv();
            provjeriOIB();
            provjeraUlice();
            provjeraRacuna();
            provjeraMjesta();
            provjeraTelefona();
            provjeraMaila();
            return true;
        }

        private void provjeraMaila()
        {
            if (!validacija.provjeraMaila(txtEmail.Text))
            {
                throw new EmailException("Neispravan Email!");
            }
        }

        private void provjeraTelefona()
        {
            if (!validacija.provjeraTelefon(txtTelefon.Text))
            {
                throw new TelefonException("Krivi broj telefona");
                
            }
        }

        private void provjeraMjesta()
        {
            if (!validacija.provjeraMjesta(txtMjesto.Text))
            {
                throw new Exception("Krivo uneseno mjesto");
            }
        }

        private void provjeraRacuna()
        {
            if (!validacija.provjeraRacuna(txtIBAN.Text))
            {
                throw new IBANException("Krivo uneesn IBAN račun");
            }
        }

        private void provjeraUlice()
        {
            if (!validacija.provjeraUlica(txtAdresa.Text))
            {
                throw new Exception("Krivo unesena adresa");
            }
        }

        private void provjeriOIB()
        {
            if (!validacija.provjeraOIB(txtOIB.Text))
            {
                throw new OIBException("Krivo unesen OIB");
            }
        }

        private void provjeriNaziv()
        {
            if (!validacija.provjeraSamoSlova(txtNaziv.Text))
            {
                throw new Exception("Naziv može sadržavati samo slova");
            }
        }

        private bool provjeriPolja()
        {
            if (txtIBAN.Text == "" || txtNaziv.Text == "" || txtMjesto.Text == "" || txtAdresa.Text == "" || txtOIB.Text == "" || txtTelefon.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("Potrebno je ispuniti sva polja", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void dodajKlijenta()
        {
            var klijent = new Klijent
            {
                Naziv = txtNaziv.Text,
                Adresa = txtAdresa.Text,
                Mjesto = txtMjesto.Text,
                OIB = txtOIB.Text,
                BrojTelefona = txtTelefon.Text,
                Email = txtEmail.Text,
                IBAN = txtIBAN.Text
            };
            servisKlijent.Add(klijent);
        }

        private void FrmDodajKlijenta_Load(object sender, EventArgs e)
        {
            if(selektiran != null)
            {
                labelDodajKlijenta.Text = selektiran.Naziv;
                ucitajPodatke(selektiran);
            }
            else
            {
                labelDodajKlijenta.Text = "Dodaj klijenta";
            }
        }

        private void ucitajPodatke(Klijent selektiran)
        {
            txtNaziv.Text = selektiran.Naziv;
            txtOIB.Text = selektiran.OIB;
            txtAdresa.Text = selektiran.Adresa;
            txtIBAN.Text = selektiran.IBAN;
            txtMjesto.Text = selektiran.Mjesto;
            txtTelefon.Text = selektiran.BrojTelefona;
            txtEmail.Text = selektiran.Email;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                string path = Path.Combine(Application.StartupPath, "Pomoc\\Pomoc\\Klijenti\\DodajKlijenta\\dodajKlijenta.html");
                System.Diagnostics.Process.Start(path);
            }
        }
    }
}
