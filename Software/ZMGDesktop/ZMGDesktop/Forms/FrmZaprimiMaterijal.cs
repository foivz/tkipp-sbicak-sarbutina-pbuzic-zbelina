using BusinessLogicLayer.Services;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using DataAccessLayer.Repositories;

namespace ZMGDesktop {
    public partial class FrmZaprimiMaterijal : Form {
        private readonly MaterijalServices matServis = new MaterijalServices(new MaterijalRepository());
        private string provjereniQR;
        private FilterInfoCollection filterInfoCollection;
        private VideoCaptureDevice captureDevice = null;

        public FrmZaprimiMaterijal() {
            InitializeComponent();
            ucitajPomoc();
        }

        private void FrmZaprimiMaterijal_Load(object sender, EventArgs e) {
            numKolicina.Visible = false;
            btnZaprimi.Visible = false;
            lblKolicina.Visible = false;

            PopuniCboDevice();
            KonfigurirajCaptureDevice();
        }

        private void PopuniCboDevice() {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo filterInfo in filterInfoCollection) {
                cboDevice.Items.Add(filterInfo.Name);
            }

            cboDevice.SelectedIndex = 0;
        }

        private void KonfigurirajCaptureDevice() {
            FilterInfo selectedFilterInfo = filterInfoCollection[cboDevice.SelectedIndex];
            captureDevice = new VideoCaptureDevice(selectedFilterInfo.MonikerString);
            captureDevice.NewFrame += CaptureDevice_NewFrame;
        }

        private void btnKreni_Click(object sender, EventArgs e) {
            captureDevice.Start();
            timer1.Start();
        }

        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs) {
            picQR.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void ucitajPomoc() {
            KeyPreview = true;
            KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void btnNatrag_Click(object sender, EventArgs e) {
            Close();
        }

        public void SkenirajMaterijal(string skeniranQR) {
            bool uspjeh = matServis.ProvjeriQR(skeniranQR);

            if (uspjeh) {
                provjereniQR = skeniranQR;
                lblSkeniraj.Visible = false;
                numKolicina.Visible = true;
                btnZaprimi.Visible = true;
                lblKolicina.Visible = true;
            }
        }

        private void btnProba_Click(object sender, EventArgs e) {
            string proba = "WNQDSCM90PALUKXBANUA";
            SkenirajMaterijal(proba);
        }

        private void btnZaprimi_Click(object sender, EventArgs e) {
            int kolicina = (int)numKolicina.Value;
            var materijal = matServis.AzurirajMaterijal(provjereniQR, kolicina);

            if (materijal != null) {
                FrmPrimka primka = new FrmPrimka(materijal, kolicina);
                primka.ShowDialog();
                Close();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F1) {
                string path = Path.Combine(Application.StartupPath, "Pomoc\\Pomoc\\Skladiste\\ZaprimiMaterijal\\zaprimiMaterijal.html");
                System.Diagnostics.Process.Start(path);
            }
        }

        private void FrmZaprimiMaterijal_FormClosing(object sender, FormClosingEventArgs e) {
            try {
                if (captureDevice != null && captureDevice.IsRunning)
                    captureDevice.Stop();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (picQR.Image == null)
                return;

            BarcodeReader barcode = new BarcodeReader();
            Result result = barcode.Decode((Bitmap)picQR.Image);

            if (result != null) {
                SkenirajMaterijal(result.ToString());
                timer1.Stop();
                ZaustaviCaptureDevice();
            }
        }

        private void ZaustaviCaptureDevice() {
            if (captureDevice != null && captureDevice.IsRunning)
                captureDevice.Stop();
        }
    }
}
