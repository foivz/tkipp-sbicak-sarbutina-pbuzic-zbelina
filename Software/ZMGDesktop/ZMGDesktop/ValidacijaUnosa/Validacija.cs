﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZMGDesktop.ValidacijaUnosa
{
    public class Validacija
    {
        public Validacija()
        {

        }

        public bool provjeraOIB(string oib)
        {
            bool validan = false;
            if (oib.Length == 11 && Regex.IsMatch(oib, @"^[0-9]+$"))
            {
                validan = true;
            }
            return validan;
        }

        public bool provjeraRacuna(string racun)
        {
            bool validan = false;
            if (Regex.IsMatch(racun, @"^HR\d{19}$"))
            {
                validan = true;
            }
            return validan;
        }

        public bool provjeraSamoSlova(string tekst)
        {
            bool validan = false;
            if(Regex.IsMatch(tekst, @"^[\p{L}\s]+$"))
            {
                validan = true;
            }
            return validan;
        }

        public bool provjeraMjesta(string mjesto)
        {
            bool validan = false;
            if(Regex.IsMatch(mjesto, @"^[\p{L}\s]+$"))
            {
                validan = true;
            }
            return validan;
        }

        public bool provjeraMaila(string email)
        {
            bool validan = false;
            if(Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                validan = true;
            }
            return validan;
        }

        public bool provjeraUlica(string ulica)
        {
            bool validan = false;
            if(Regex.IsMatch(ulica, @"^[\p{L}\d\s]+$"))
            {
                validan = true;
            }
            return validan;
        }

        public bool provjeraSamoBrojevi(string brojevi)
        {
            bool validan = false;
            if(Regex.IsMatch(brojevi, @"^[0-9]+$"))
            {
                validan = true;
            }
            return validan;
        }

        public bool provjeraTelefon(string telefon)
        {
            bool validan = false;
            if (Regex.IsMatch(telefon, @"^[0-9]+$") && telefon.Length > 5 && telefon.Length <= 10)
            {
                validan = true;
            }
            return validan;
        }
    }
}
