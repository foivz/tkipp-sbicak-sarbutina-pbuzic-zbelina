using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using System;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DataAccessLayer.Repositories;


namespace ZMGDesktop {
    public partial class FrmPrimka : Form {
        private Materijal mat;
        private int kol;
        private PrimkaServices primkaServis = new PrimkaServices(new PrimkaRepository());

        public FrmPrimka(Materijal materijal, int kolicina) {
            mat = materijal;
            kol = kolicina;
            InitializeComponent();
            UcitajPomoc();
        }

        private void UcitajPomoc() {
            this.KeyPreview = true;
            this.KeyDown += FrmPrimka_KeyDown;
        }

        private void btnZatvori_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void UcitajPrimku(object sender, EventArgs e) {
            PostaviPolja();
            PohraniUBazu();
        }

        private void PohraniUBazu() {
            Primka primka = new Primka() {
                Naziv_Materijal = mat.Naziv.ToString(),
                Kolicina = kol,
                Datum = DateTime.Now.Date
            };

            primkaServis.DodajPrimku(primka);
        }

        private void PostaviPolja() {
            txtKolicina.Text = kol.ToString();
            txtMjernaJedinica.Text = mat.JedinicaMjere.ToString();
            txtNaziv.Text = mat.Naziv.ToString();
            txtDatum.Text = DateTime.Now.ToString();
        }

        private void btnPohrani_Click(object sender, EventArgs e) {
            CreateAndSavePdf();
        }

        private void CreateAndSavePdf() {
            using (Document doc = new Document(PageSize.A4, 10, 10, 42, 35)) {
                if (ShowSaveFileDialog(out string filePath)) {
                    try {
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Create)) {
                            PdfWriter writer = PdfWriter.GetInstance(doc, fileStream);
                            doc.Open();

                            AddTitle(doc, "Podaci o materijalu");
                            AddParagraph(doc, "Naziv: " + txtNaziv.Text);
                            AddParagraph(doc, "Količina: " + txtKolicina.Text + " " + txtMjernaJedinica.Text);
                            AddParagraph(doc, "Datum: " + txtDatum.Text);

                            doc.Close();
                            ShowMessageBox("PDF dokument uspješno kreiran i spremljen.");
                        }
                    } catch (Exception ex) {
                        ShowMessageBox("Greška prilikom stvaranja i spremanja PDF dokumenta: " + ex.Message);
                    }
                }
            }
        }

        private bool ShowSaveFileDialog(out string filePath) {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog()) {
                saveFileDialog.Filter = "PDF Documents (.pdf)|.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    filePath = saveFileDialog.FileName;
                    return true;
                }
            }

            filePath = null;
            return false;
        }

        private void AddTitle(Document doc, string text) {
            Paragraph title = new Paragraph(text);
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);
        }

        private void AddParagraph(Document doc, string text) {
            Paragraph paragraph = new Paragraph(text);
            doc.Add(paragraph);
        }

        private void ShowMessageBox(string message) {
            MessageBox.Show(message);
        }

        private void FrmPrimka_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F1) {
                string path = Path.Combine(Application.StartupPath, "Pomoc\\Pomoc\\Skladiste\\Primka\\primka.html");
                System.Diagnostics.Process.Start(path);
            }
        }
    }
}
