using PdfSharp.Pdf;
using PdfSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;
using PdfSharp.Drawing;
using EntitiesLayer.Entities;
using PdfSharp.Pdf.Advanced;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.CompilerServices;

namespace BusinessLogicLayer.PDF
{
    public static class GeneriranjePDF
    {
        public static string nazivDatoteke = null;
        static PdfDocument document;
        static PdfPage page;
        static XPen pen;
        static XGraphics gfx;
        static XFont font;
        static double x;
        static double y;
        static double ls;

        private static void NapraviDokument()
        {
            document = new PdfDocument();
            page = document.AddPage();
        }

        private static void PostaviVelicinuDokumenta(PageSize velicina)
        {
            page.Size = velicina;
        }

        private static void PostaviMargine(double gornja, double desna, double donja, double lijeva)
        {
            page.TrimMargins.Top = gornja;
            page.TrimMargins.Right = desna;
            page.TrimMargins.Bottom = donja;
            page.TrimMargins.Left = lijeva;

        }

        private static void PostaviPocetnuSirinuVisinu(double sirina, double visina)
        {
            PostaviSirinu(sirina); // width ---->
            PostaviVisinu(visina); // height gore/dolje
        }

        private static void PostaviSirinu(double sirina)
        {
            x = sirina;
        }

        private static void PostaviVisinu(double visina)
        {
            y = visina;
        }

        // postavljamo olovku za crtanje po pdfu.
        private static void PostaviOlovku(string boja, double debljina)
        {
            pen = new XPen(XColor.FromName(boja), debljina);
        }

        // sluzi za crtanje u objektu tj. u pdfu.
        private static void PostaviGFX()
        {
            gfx = XGraphics.FromPdfPage(page);
        }

        private static void DefinirajFont(string kojiFont, double velicina, XFontStyle stil)
        {
            font = new XFont(kojiFont, velicina, stil);
        }
        private static void DefinirajVertikalniRazmakPostavljenogFonta()
        {
            ls = font.GetHeight();
        }
        private static void DodajRazmakNaVisinu(double dodatak, bool specificniDodatak = false)
        {
            if (!specificniDodatak) y += ls + dodatak;
            else y += dodatak;
        }
        private static void DodajRazmakNaVisinu()
        {
            y += ls;
        }

        private static void UkloniRazmakNaVisinu()
        {
            y -= ls;
        }

        private static void UkloniRazmakNaVisinu(double dodatak, bool specificniDodatak = false)
        {
            if (!specificniDodatak) y -= ls + dodatak;
            else y -= dodatak;
        }

        private static void DodajRazmakNaSirinu(double dodatak, bool specificniDodatak = false)
        {
            if (!specificniDodatak) x += ls + dodatak;
            else x += dodatak;
        }
        private static void CrtajLiniju(double pocetakX, double pocetakY, double noviX, double noviY)
        {
            gfx.DrawLine(pen, new XPoint(pocetakX, pocetakY), new XPoint(noviX, noviY));
            DodajRazmakNaVisinu();
        }
        private static void CrtajLiniju(double pocetakX, double pocetakY, double noviX, double noviY, double dodatak, bool specificniDodatak = false)
        {
            gfx.DrawLine(pen, new XPoint(pocetakX, pocetakY), new XPoint(noviX, noviY));
            DodajRazmakNaVisinu(dodatak, specificniDodatak);
        }

        private static double GetNoviX(double dodatak)
        {
            return x + dodatak;
        }

        private static double GetNoviY(double dodatak)
        {
            return y + dodatak;
        }

        private static void CrtajBezRazmaka(string tekst)
        {
            gfx.DrawString(tekst, font, XBrushes.Black, x, y);
        }

        private static void Crtaj(int broj)
        {
            gfx.DrawString($"{broj}.", font, XBrushes.Black, x, y);
        }

        private static void Crtaj(string tekst)
        {
            gfx.DrawString(tekst, font, XBrushes.Black, x, y);
            DodajRazmakNaVisinu();
        }
        private static void Crtaj(string tekst, double dodatak, bool specificniDodatak = false)
        {
            gfx.DrawString(tekst, font, XBrushes.Black, x, y);
            DodajRazmakNaVisinu(dodatak, specificniDodatak);
        }

        private static void Crtaj(string tekst, object obj, string propertyPath)
        {
            var propertyNames = propertyPath.Split('.');
            object currentObject = obj;
            if (currentObject == null) return;
            foreach (var propertyName in propertyNames)
            {
                var property = currentObject.GetType().GetProperty(propertyName);
                if (property == null)
                {
    
                    currentObject = null;
                    break;
                }

                currentObject = property.GetValue(currentObject, null);
            }
            if (currentObject != null)
            {
                gfx.DrawString($"{tekst}: {currentObject}", font, XBrushes.Black, x, y);
                DodajRazmakNaVisinu();
            }
        }

        private static void Crtaj(object obj, string propertyPath, double dodatak, bool specificniDodatak = false)
        {
            var propertyNames = propertyPath.Split('.');
            object currentObject = obj;
            if (currentObject == null) return;
            foreach (var propertyName in propertyNames)
            {
                var property = currentObject.GetType().GetProperty(propertyName);
                if (property == null)
                {
                    // Property not found, break the loop
                    currentObject = null;
                    break;
                }

                currentObject = property.GetValue(currentObject, null);
            }
            if (currentObject != null)
            {
                gfx.DrawString($"{currentObject}", font, XBrushes.Black, x, y);
                DodajRazmakNaVisinu(dodatak, specificniDodatak);
            }
        }

        private static void CrtajUzSirinu(string tekst, double dodatak, bool specificniDodatak = false)
        {
            gfx.DrawString(tekst, font, XBrushes.Black, x, y);
            DodajRazmakNaSirinu(dodatak, specificniDodatak);
        }

        private static void CrtajUzSirinu(object obj, string propertyPath, double dodatak, bool specificniDodatak = false)
        {
            var propertyNames = propertyPath.Split('.');
            object currentObject = obj;
            if (currentObject == null) return;
            foreach (var propertyName in propertyNames)
            {
                var property = currentObject.GetType().GetProperty(propertyName);
                if (property == null)
                {
                    // Property not found, break the loop
                    currentObject = null;
                    break;
                }

                currentObject = property.GetValue(currentObject, null);
            }
            if (currentObject != null)
            {
                gfx.DrawString($"{currentObject}", font, XBrushes.Black, x, y);
                DodajRazmakNaSirinu(dodatak, specificniDodatak);
            }
        }

        private static void InitDokument()
        {
            NapraviDokument();
            PostaviVelicinuDokumenta(PageSize.A4);
            PostaviMargine(0.5, 0.5, 0.5, 0.5);
            PostaviPocetnuSirinuVisinu(50, 80);
            PostaviOlovku("black", 2.2);
            PostaviGFX();
            DefinirajFont("Arial", 20, XFontStyle.BoldItalic);
            DefinirajVertikalniRazmakPostavljenogFonta();
        }

        private static void InitPrviDioRacun()
        {
            Crtaj("ZMG DAMIR BICAK");
            DefinirajFont("Arial", 12, XFontStyle.Bold);
            DefinirajVertikalniRazmakPostavljenogFonta();
            Crtaj("ZAŠTITA METALNE GALANTERIJE");
            Crtaj("Sveti Ivan Zelina", 20, true);
        }

        private static void InitDrugiDioRacun()
        {
            DefinirajFont("Arial", 10, XFontStyle.Regular);
            DefinirajVertikalniRazmakPostavljenogFonta();
            Crtaj("NKD 2007:25.61 - OBRADA I PREVLAČENJE METALA");
            Crtaj("OPĆI MEHANIČKI RADOVI", 3);
        }

        private static void InitTreciDioRacunPoslodavac(Racun racun)
        {
            Crtaj("UPIS U OBRTNI REGISTAR", racun, "Poslodavac.UpisObrtniRegistar");
            Crtaj("Br. OBRTNICE", racun, "Poslodavac.BrojObrtnice");
            Crtaj("OIB", racun, "Poslodavac.OIB");
            Crtaj("Poslovnica", racun, "Poslodavac.Poslovnica");
            Crtaj("Adresa", racun, "Poslodavac.Adresa");
            Crtaj("Mjesto: 10380", racun, "Poslodavac.Mjesto");
            Crtaj("Drzava", racun, "Poslodavac.Drzava");
            Crtaj("Banka", racun, "Poslodavac.Banka");
            PostaviVisinu(203);
            DodajRazmakNaSirinu(150, true);
            Crtaj("TEL.", racun, "Poslodavac.TEL_FAX");
            Crtaj("GSM.", racun, "Poslodavac.BrojTelefona");
            Crtaj("TEL/FAX.", racun, "Poslodavac.TEL_FAX");
            Crtaj("Email", racun, "Poslodavac.Email");
            Crtaj("Ziro racun", racun, "Poslodavac.IBAN");
            PostaviVisinu(180);
            DodajRazmakNaSirinu(210, true);
        }

        private static void InitCetvrtiDioRacunKlijent(Racun racun)
        {
            Crtaj("Prima", 3, true);
            CrtajLiniju(x, y, GetNoviX(140), y, 20, true);
            Crtaj("Naziv", racun, "Klijent.Naziv");
            Crtaj("Adresa", racun, "Klijent.Adresa");
            Crtaj("Mjesto", racun, "Klijent.Mjesto");
            Crtaj("OIB", racun, "Klijent.OIB");
            CrtajLiniju(x, y, GetNoviX(140), y, 40, true);
        }

        private static void InitPetiDioRacunRacun(Racun racun)
        {
            PostaviSirinu(50);
            DefinirajFont("Arial", 10, XFontStyle.Bold);
            Crtaj("RACUN BROJ", racun, "Racun_ID");
            Crtaj("Stavke racuna");
            PostaviSirinu(390);
            DefinirajFont("Arial", 10, XFontStyle.Regular);
            if (racun.DatumIzdavanja == null) CrtajBezRazmaka("Datum izdavanja: nije uneseno");
            else
            {
                gfx.DrawString($"Datum izdavanja: {racun.DatumIzdavanja.Value.ToShortDateString()}", font, XBrushes.Black, x, y);
            }
            UkloniRazmakNaVisinu();
            if (racun.DatumIzdavanja == null) CrtajBezRazmaka("Vrijeme izdavanja: nije uneseno");
            else
            {
                gfx.DrawString($"Vrijeme izdavanja: {racun.DatumIzdavanja.Value.ToShortTimeString()}", font, XBrushes.Black, x, y);
            }

            DodajRazmakNaVisinu();
            PostaviSirinu(50);
            DodajRazmakNaVisinu();
            CrtajLiniju(x, y, 550, y);
        }

        private static void InitSestiDioRacunStavke(List<StavkaRacun> listaStavki)
        {
            // stavke
            DefinirajFont("Arial", 9, XFontStyle.Regular);
            CrtajUzSirinu("r.b", 24, true);
            CrtajUzSirinu("Naziv usluge", 70, true);
            CrtajUzSirinu("Naziv robe", 70, true);
            CrtajUzSirinu("Jed. kolicina", 70, true);
            CrtajUzSirinu("Datum izrade", 70, true);
            CrtajUzSirinu("Kolicina(kg)", 70, true);
            CrtajUzSirinu("Jed. cijena/kg", 70, true);
            CrtajBezRazmaka("Ukupna cijena");

            PostaviSirinu(50);
            DodajRazmakNaVisinu(5);

            StavkaRacun stavka = new StavkaRacun();
            for (int i = 1; i <= 10; i++)
            {
                Crtaj(i);
                // popunavanje izmedu
                if (i <= listaStavki.Count && listaStavki != null)
                {
                    stavka = listaStavki[i - 1];

                    DodajRazmakNaSirinu(24, true);
                    CrtajUzSirinu(stavka, "Usluga.Naziv", 70, true);
                    CrtajUzSirinu(stavka, "Roba.Naziv", 70, true);
                    CrtajUzSirinu(stavka, "KolikoRobePoJedinici", 70, true);
                    gfx.DrawString($"{stavka.DatumIzrade.Value.ToShortDateString()}", font, XBrushes.Black, x, y);
                    DodajRazmakNaSirinu(70, true);
                    CrtajUzSirinu(stavka, "KolicinaRobe", 70, true);
                    CrtajUzSirinu(stavka, "JedinicnaCijena", 70, true);
                    CrtajUzSirinu(stavka, "UkupnaCijenaStavke", 0, true);
                }
                DodajRazmakNaVisinu(5);
                PostaviSirinu(50);
            }
        }

        private static void InitSedmiDioRacunUkupno(Racun racun)
        {
            DefinirajFont("Arial", 10, XFontStyle.Bold);
            UkloniRazmakNaVisinu();
            CrtajLiniju(x, y, 550, y);
            PostaviSirinu(424);
            DodajRazmakNaVisinu();
            CrtajBezRazmaka("Ukupno:");
            PostaviSirinu(494);
            Crtaj(racun, "UkupnoStavke", 3);
            PostaviSirinu(424);
            CrtajBezRazmaka("PDV(25%):");
            PostaviSirinu(494);
            Crtaj(racun, "PDV", 3);
            PostaviSirinu(424);
            CrtajBezRazmaka("Ukupno(EUR):");
            PostaviSirinu(494);
            Crtaj(racun, "UkupnaCijena", 3);
            PostaviSirinu(424);
            CrtajLiniju(x, y, 550, y, ls * 3, true);
        }

        private static void InitOsmiDioRacunNapomene(Racun racun)
        {
            PostaviSirinu(50);
            DefinirajFont("Arial", 9, XFontStyle.Regular);
            gfx.DrawString($"Način plaćanja: {racun.NacinPlacanja} - rok plaćanja{racun.RokPlacanja}", font, XBrushes.Black, x, y);
            DodajRazmakNaVisinu();
            Crtaj("Porez na dodanu vrijednost je zaračunat prema zakonu o PDV-u objavljenog u NN 47/95 - 87/09,94/09,22/12,136/12.");
            Crtaj("U slučaju ne plaćanja po dospijeću, ovaj račun može poslužiti kao vjerodostojna isprava za ovršni postupak.");
            Crtaj("Reklamacije po ovom računu uvažavamo 8 (osam) dana po njegovom primitku.");
            Crtaj("Valuta plaćanja je u EURIMA.");
            Crtaj("Ovaj dokument je izdan u elektronskom obliku, te je valjan bez potpisa i pečata.", ls * 3, true);
            PostaviSirinu(354);
            Crtaj("Fakturirao", racun, "Radnik");
        }

        public static int SacuvajPDF(Racun racun, List<StavkaRacun> listaStavki = null)
        {
            InitDokument();
            InitPrviDioRacun();
            InitDrugiDioRacun();
            InitTreciDioRacunPoslodavac(racun);
            InitCetvrtiDioRacunKlijent(racun);
            InitPetiDioRacunRacun(racun);
            InitSestiDioRacunStavke(listaStavki);
            InitSedmiDioRacunUkupno(racun);
            InitOsmiDioRacunNapomene(racun);
 
            nazivDatoteke = $"ZMG - RACUN BROJ {racun.Racun_ID}.pdf";

            try
            {
                document.Save(nazivDatoteke);
                return 1;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Proces za PDF je zauzet! Pričekajte.", "Prioritet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

        }
        public static void OtvoriPDF()
        {
            if (nazivDatoteke != null)
            {
                Process.Start(nazivDatoteke);
            }
        }
    }
}
