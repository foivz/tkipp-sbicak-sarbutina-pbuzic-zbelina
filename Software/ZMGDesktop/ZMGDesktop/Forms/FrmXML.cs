using BusinessLogicLayer.Services;
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
using System.Xml;
using System.Xml.Linq;
using ZMGDesktop.ValidacijaUnosa;

namespace ZMGDesktop
{
    public partial class FrmXML : Form
    {
        private string fileName;
        private KlijentServices servisKlijent = new KlijentServices(new KlijentRepository());
        private Validacija validacija = new Validacija();
        public FrmXML()
        {
            InitializeComponent();
            ucitajPomoc();
        }

        private void ucitajPomoc()
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void FrmXML_Load(object sender, EventArgs e)
        {
            btnUnesi.Enabled = false;
        }

        private void btnOdaberi_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Odaberite XML datoteku";
            openFileDialog.Filter = "XML File (*.xml)|*.xml";
            DialogResult result = openFileDialog.ShowDialog(); 
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                btnUnesi.Enabled = true;
                fileName = openFileDialog.FileName;
                MessageBox.Show(fileName);
            }
        }

        private void btnUnesi_Click(object sender, EventArgs e)
        {
            try
            {
                XDocument xDoc = XDocument.Load(fileName);
                List<Klijent> klijentiList = ParsirajKlijente(xDoc);
                if (klijentiList.Count != 0)
                {
                    DodajKlijente(klijentiList);
                    PrikaziKlijente();
                    MessageBox.Show("Uspješno učitani korisnici");
                }
                else
                {
                    MessageBox.Show("Krivi format XML datoteke, ne mogu pročitati");
                }
            }
            catch (UserException ex)
            {
                HandleException("Postoji već klijent s ovim nazivom.");
            }
            catch (TelefonException ex)
            {
                HandleException("Postoji već klijent s ovim brojem telefona.");
            }
            catch (IBANException ex)
            {
                HandleException("Postoji već klijent s ovim IBAN računom.");
            }
            catch (OIBException ex)
            {
                HandleException("Postoji već klijent s ovim OIB-om.");
            }
            catch (EmailException ex)
            {
                HandleException("Postoji već klijent s ovim Email-om.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dogodila se greška, klijenti nisu uvezeni");
            }
        }

        private List<Klijent> ParsirajKlijente(XDocument xDoc)
        {
            List<Klijent> klijentiList = xDoc.Descendants("klijent")
        .Select(klijent =>
        {
            Klijent noviKlijent = new Klijent();

            noviKlijent.Naziv = ProvjeriElement(klijent, "Naziv");
            noviKlijent.Adresa = ProvjeriElement(klijent, "Adresa");
            noviKlijent.Mjesto = ProvjeriElement(klijent, "Mjesto");
            noviKlijent.OIB = ProvjeriElement(klijent, "OIB");
            noviKlijent.BrojTelefona = ProvjeriElement(klijent, "BrojTelefona");
            noviKlijent.Email = ProvjeriElement(klijent, "Email");
            noviKlijent.IBAN = ProvjeriElement(klijent, "IBAN");

            return noviKlijent;
        })
        .ToList();

            return klijentiList;
        }

        private string ProvjeriElement(XElement klijent, string elementName)
        {
            XElement element = klijent.Element(elementName);
            if (element != null)
            {
                return element.Value;
            }
            else
            {
                throw new Exception($"Nedostaje element '{elementName}' u klijentu.");
            }
        }


        private void DodajKlijente(List<Klijent> klijentiList)
        {
            foreach (var klijent in klijentiList)
            {
                if (provjeri(klijent))
                {
                    servisKlijent.Add(klijent);
                }
                else
                {
                    MessageBox.Show("Neuspješno ubacivanje korisnika", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void PrikaziKlijente()
        {
            dgvKlijentiXML.DataSource = servisKlijent.DohvatiKlijente();
            dgvKlijentiXML.Columns[0].Visible = false;
            dgvKlijentiXML.Columns[8].Visible = false;
            dgvKlijentiXML.Columns[9].Visible = false;
            dgvKlijentiXML.Columns[10].Visible = false;
            dgvKlijentiXML.Columns[11].Visible = false;
        }

        private void HandleException(string poruka)
        {
            dgvKlijentiXML.DataSource = servisKlijent.DohvatiKlijente();
            MessageBox.Show(poruka);
        }

        private bool provjeri(Klijent klijent)
        {
            provjeriKlijenta(klijent);
            provjeriNaziv(klijent);
            provjeriOIB(klijent);
            provjeraUlica(klijent);
            provjeraRacuna(klijent);
            provjeraMjesta(klijent);
            provjeraTelefona(klijent);
            provjeraMaila(klijent);
            return true;
        }

        private void provjeraMaila(Klijent klijent)
        {
            if (!validacija.provjeraMaila(klijent.Email))
            {
                throw new EmailException("Krivi email");
            }
        }

        private void provjeraTelefona(Klijent klijent)
        {
            if (!validacija.provjeraTelefon(klijent.BrojTelefona))
            {
                throw new TelefonException("Krivi broj telefona");
            }
        }

        private void provjeraMjesta(Klijent klijent)
        {
            if (!validacija.provjeraMjesta(klijent.Mjesto))
            {
                throw new Exception("Krivo uneseno mjesto");
            }
        }

        private void provjeraRacuna(Klijent klijent)
        {
            if (!validacija.provjeraRacuna(klijent.IBAN))
            {
                throw new Exception("Krivo uneesn IBAN račun");
            }
        }

        private void provjeraUlica(Klijent klijent)
        {
            if (!validacija.provjeraUlica(klijent.Adresa))
            {
                throw new Exception("Krivo unesena adresa");
            }
        }

        private void provjeriOIB(Klijent klijent)
        {
            if (!validacija.provjeraOIB(klijent.OIB))
            {
                throw new OIBException("Krivo unesen OIB");
            }
        }

        private void provjeriNaziv(Klijent klijent)
        {
            if (!validacija.provjeraSamoSlova(klijent.Naziv))
            {
                throw new Exception("Naziv može sadržavati samo slova");
            }
        }

        private void provjeriKlijenta(Klijent klijent)
        {
            if (klijent.IBAN == "" || klijent.Naziv == "" || klijent.Mjesto == "" || klijent.Adresa == "" || klijent.OIB == "" || klijent.BrojTelefona == "" || klijent.Email == "")
            {
                throw new Exception("Potrebno je ispuniti sva polja");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                string path = Path.Combine(Application.StartupPath, "Pomoc\\Pomoc\\Klijenti\\UveziKlijentaXML\\uveziKlijenta.html");
                System.Diagnostics.Process.Start(path);
            }
        }
        
    }
}
