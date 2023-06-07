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
            if(!specificniDodatak) y += ls + dodatak;
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

        private static void CrtajUzSirinu(string tekst, double dodatak, bool specificniDodatak = false)
        {
            gfx.DrawString(tekst, font, XBrushes.Black, x, y);
            DodajRazmakNaSirinu(dodatak, specificniDodatak);
        }

        private static void Crtaj(string tekst, object obj, string propertyPath)
        {
            var propertyNames = propertyPath.Split('.');
            object currentObject = obj;
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
                gfx.DrawString($"{tekst}: {currentObject}", font, XBrushes.Black, x, y);
                DodajRazmakNaVisinu();
            }
        }

        private static void Crtaj(string tekst, object obj, string propertyPath, double dodatak, bool specificniDodatak = false)
        {
            var propertyNames = propertyPath.Split('.');
            object currentObject = obj;
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
                gfx.DrawString($"{tekst}: {currentObject}", font, XBrushes.Black, x, y);
                DodajRazmakNaVisinu(dodatak, specificniDodatak);
            }
        }

        private static void CrtajUzSirinu(string tekst, object obj, string propertyPath, double dodatak, bool specificniDodatak = false)
        {
            var propertyNames = propertyPath.Split('.');
            object currentObject = obj;
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
                gfx.DrawString($"{tekst}{currentObject}", font, XBrushes.Black, x, y);
                DodajRazmakNaSirinu(dodatak, specificniDodatak);
            }
        }

        public static void SacuvajPDF(Racun racun, List<StavkaRacun> listaStavki = null)
        {
            NapraviDokument();
            PostaviVelicinuDokumenta(PageSize.A4);
            PostaviMargine(0.5,0.5,0.5,0.5);
            PostaviPocetnuSirinuVisinu(50, 80);
            PostaviOlovku("black", 2.2);
            PostaviGFX();
            DefinirajFont("Arial", 20, XFontStyle.BoldItalic);
            DefinirajVertikalniRazmakPostavljenogFonta();

            // prvi dio racuna
            Crtaj("ZMG DAMIR BICAK");
            DefinirajFont("Arial", 12, XFontStyle.Bold);
            DefinirajVertikalniRazmakPostavljenogFonta();
            Crtaj("ZAŠTITA METALNE GALANTERIJE");
            Crtaj("Sveti Ivan Zelina", 20, true);
            //drugi dio racuna
            DefinirajFont("Arial", 10, XFontStyle.Regular);
            DefinirajVertikalniRazmakPostavljenogFonta();
            Crtaj("NKD 2007:25.61 - OBRADA I PREVLAČENJE METALA");
            Crtaj("OPĆI MEHANIČKI RADOVI", 3);
            //treci dio
            //poslodavac

            Crtaj("UPIS U OBRTNI REGISTAR", racun, "Poslodavac.UpisObrtniRegistar");
            Crtaj("Br. OBRTNICE", racun, "Poslodavac.BrojObrtnice");
            Crtaj("OIB", racun, "Poslodavac.OIB");
            Crtaj("Poslovnica", racun, "Poslodavac.Poslovnica");
            Crtaj("Adresa", racun, "Poslodavac.Adresa");
            Crtaj("Mjesto: 10380", racun, "Poslodavac.Mjesto");
            Crtaj("Drzava", racun, "Poslodavac.Drzava");
            Crtaj("Banka", racun, "Poslodavac.Banka");
            PostaviVisinu(203);
            //treci dio -- drugio dio, druga strana
            DodajRazmakNaSirinu(150, true);
            Crtaj("TEL.", racun, "Poslodavac.TEL_FAX");
            Crtaj("GSM.", racun, "Poslodavac.BrojTelefona");
            Crtaj("TEL/FAX.", racun, "Poslodavac.TEL_FAX");
            Crtaj("Email", racun, "Poslodavac.Email");
            Crtaj("Ziro racun", racun, "Poslodavac.IBAN");
            PostaviVisinu(180);
            DodajRazmakNaSirinu(210, true);
            Crtaj("Prima", 3, true);
            CrtajLiniju(x, y, GetNoviX(140), y, 20, true);
            //klijent
            Crtaj("Naziv", racun, "Klijent.Naziv");
            Crtaj("Adresa", racun, "Klijent.Adresa");
            Crtaj("Mjesto", racun, "Klijent.Mjesto");
            Crtaj("OIB", racun, "Klijent.OIB");
            CrtajLiniju(x, y, GetNoviX(140), y, 40, true);

            // cetvrti dio -- racun i stavke
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
                    CrtajUzSirinu("", stavka, "Usluga.Naziv", 70, true);
                    CrtajUzSirinu("", stavka, "Roba.Naziv", 70, true);
                    CrtajUzSirinu("", stavka, "KolikoRobePoJedinici", 70, true);
                    CrtajUzSirinu("", stavka, "DatumIzrade", 70, true);
                    CrtajUzSirinu("", stavka, "KolicinaRobe", 70, true);
                    CrtajUzSirinu("", stavka, "JedinicnaCijena", 70, true);
                    CrtajUzSirinu("", stavka, "UkupnaCijenaStavke", 0, true);
                }
                //kraj
                DodajRazmakNaVisinu(5);
                y += ls + 5;
                x = 50;
                PostaviSirinu(50);
            }
            font = new XFont("Arial", 10, XFontStyle.Bold);
            y -= ls;
            gfx.DrawLine(pen, new XPoint(x, y), new XPoint(550, y));
            x = 424;
            y += ls;
            gfx.DrawString($"Ukupno:", font, XBrushes.Black, x, y);
            x = 494;
            gfx.DrawString($"{racun.UkupnoStavke}", font, XBrushes.Black, x, y);
            y += ls + 3;
            x = 424;
            gfx.DrawString($"PDV(25%):", font, XBrushes.Black, x, y);
            x = 494;
            gfx.DrawString($"{racun.PDV}", font, XBrushes.Black, x, y);
            y += ls + 3;
            x = 424;
            gfx.DrawString($"Ukupno(EUR):", font, XBrushes.Black, x, y);
            x = 494;
            gfx.DrawString($"{racun.UkupnaCijena}", font, XBrushes.Black, x, y);
            y += 5;
            gfx.DrawLine(pen, new XPoint(424, y), new XPoint(550, y));
            y += ls + ls + ls;

            // peti dio -- kraj racuna, izdan u takvom obliku, fakturirao itd.
            x = 50;
            font = new XFont("Arial", 9, XFontStyle.Regular);
            gfx.DrawString($"Način plaćanja: {racun.NacinPlacanja} - rok plaćanja{racun.RokPlacanja}", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString($"Porez na dodanu vrijednost je zaračunat prema zakonu o PDV-u objavljenog u NN 47/95 - 87/09,94/09,22/12,136/12.", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString($"U slučaju ne plaćanja po dospijeću, ovaj račun može poslužiti kao vjerodostojna isprava za ovršni postupak.", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString($"Reklamacije po ovom računu uvažavamo 8 (osam) dana po njegovom primitku.", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString($"Valuta plaćanja je u EURIMA.", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString($"Ovaj dokument je izdan u elektronskom obliku, te je valjan bez potpisa i pečata.", font, XBrushes.Black, x, y);
            y += ls + ls + ls;
            x = 354;
            gfx.DrawString($"Fakturirao: {racun.Radnik.ToString()}", font, XBrushes.Black, x, y);


            nazivDatoteke = $"ZMG - RACUN BROJ {racun.Racun_ID}.pdf";

            try
            {
                document.Save(nazivDatoteke);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Proces za PDF je zauzet! Pričekajte.", "Prioritet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            //Load PDF File for viewing
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
