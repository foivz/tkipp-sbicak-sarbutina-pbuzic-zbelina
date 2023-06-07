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
        
        private static void Crtaj(string tekst)
        {
            gfx.DrawString(tekst, font, XBrushes.Black, x, y);
            DodajRazmakNaVisinu();
        }
        private static void Crtaj(string tekst, double dodatak, bool specificniDodatak = false)
        {
            gfx.DrawString(tekst, font, XBrushes.Black, x, y);
            DodajRazmakNaVisinu(dodatak);
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

        private static void Crtaj(string tekst, double dodatak, object obj, string propertyPath, bool specificniDodatak = false)
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
                DodajRazmakNaVisinu(dodatak);
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
            x += 150;
            gfx.DrawString($"TEL. {racun.Poslodavac.TEL_FAX}", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString($"GSM. {racun.Poslodavac.BrojTelefona}", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString($"TEL/FAX. {racun.Poslodavac.TEL_FAX}", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString($"Email: {racun.Poslodavac.Email}", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString($"Žiro račun: {racun.Poslodavac.IBAN}", font, XBrushes.Black, x, y);
            //telefone treba promijeniti
            y = 180;
            x += 210;
            gfx.DrawString("Prima", font, XBrushes.Black, x, y);
            y += 3;
            gfx.DrawLine(pen, new XPoint(x, y), new XPoint(x+140, y));
            y += 20;
            //klijent
            gfx.DrawString($"Naziv: {racun.Klijent.Naziv}", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString($"Adresa: {racun.Klijent.Adresa}", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString($"Mjesto: {racun.Klijent.Mjesto}", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString($"OIB: {racun.Klijent.OIB}", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawLine(pen, new XPoint(x, y), new XPoint(x + 140, y));
            y += 40;

            // cetvrti dio -- racun i stavke

            x = 50;
            font = new XFont("Arial", 10, XFontStyle.Bold);
            gfx.DrawString($"RAČUN BROJ: {racun.Racun_ID}", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString($"Stavke računa:", font, XBrushes.Black, x, y);
            x = 390;
            font = new XFont("Arial", 10, XFontStyle.Regular);
            if (racun.DatumIzdavanja == null)
            {
                gfx.DrawString($"Datum izdavanja: nije uneseno", font, XBrushes.Black, x, y);
            }
            else
            {
                gfx.DrawString($"Datum izdavanja: {racun.DatumIzdavanja.Value.ToShortDateString()}", font, XBrushes.Black, x, y);
            }
            y -= ls;
            if (racun.DatumIzdavanja == null)
            {
                 gfx.DrawString($"Vrijeme izdavanja: nije uneseno", font, XBrushes.Black, x, y);
            }
            else
            {
                gfx.DrawString($"Vrijeme izdavanja: {racun.DatumIzdavanja.Value.ToShortTimeString()}", font, XBrushes.Black, x, y);
            }
            
            y += ls;
            x = 50;
            y += ls - 3;
            gfx.DrawLine(pen, new XPoint(x, y), new XPoint(550, y));
            y += ls + 3;


            // stavke
            font = new XFont("Arial", 9, XFontStyle.Regular);
            gfx.DrawString($"r.b.", font, XBrushes.Black, x, y);
            // 5 stavki izemdu 50 i 550
            x += 24;
            gfx.DrawString($"Naziv usluge", font, XBrushes.Black, x, y);
            //x = 74
            x += 70;
            gfx.DrawString($"Naziv robe", font, XBrushes.Black, x, y);
            //x = 144
            x += 70;
            gfx.DrawString($"Jed. količina", font, XBrushes.Black, x, y);
            //x = 214
            x += 70;
            gfx.DrawString($"Datum izrade", font, XBrushes.Black, x, y);
            //x = 284
            x += 70;
            gfx.DrawString($"Količina(kg)", font, XBrushes.Black, x, y);
            //x = 354;
            x += 70;
            gfx.DrawString($"Jed. cijena/kg", font, XBrushes.Black, x, y);
            //x = 424
            x += 70;
            gfx.DrawString($"Ukupna cijena", font, XBrushes.Black, x, y);
            //x = 494


            x = 50;
            y += ls + 5;
            StavkaRacun stavka = new StavkaRacun();
            for (int i = 1; i <= 10; i++)
            {
                gfx.DrawString($"{i}.", font, XBrushes.Black, x, y);
                // popunavanje izmedu
                if (i <= listaStavki.Count && listaStavki != null)
                {
                    stavka = listaStavki[i - 1];

                    x += 24;
                    gfx.DrawString($"{stavka.Usluga.Naziv}", font, XBrushes.Black, x, y);
                    //x = 74
                    x += 70;
                    gfx.DrawString($"{stavka.Roba.Naziv}", font, XBrushes.Black, x, y);
                    //x = 144
                    x += 70;
                    gfx.DrawString($"{stavka.KolikoRobePoJedinici}", font, XBrushes.Black, x, y);
                    //x = 214
                    x += 70;
                    gfx.DrawString($"{stavka.DatumIzrade.Value.ToShortDateString()}", font, XBrushes.Black, x, y);
                    //x = 284
                    x += 70;
                    gfx.DrawString($"{stavka.KolicinaRobe}", font, XBrushes.Black, x, y);
                    //x = 354;
                    x += 70;
                    gfx.DrawString($"{stavka.JedinicnaCijena}", font, XBrushes.Black, x, y);
                    //x = 424
                    x += 70;
                    gfx.DrawString($"{stavka.UkupnaCijenaStavke}", font, XBrushes.Black, x, y);
                    //x = 494
                }
                //kraj
                y += ls + 5;
                x = 50;
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
