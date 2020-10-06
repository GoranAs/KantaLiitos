using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    public class UusiHevonen
    {
        public int Id { get; set; }
        public string Cus { get; set; }
        public DateTime Aika { get; set; }
        public string Tunnus { get; set; }
        public string Nimi { get; set; }
        public string Tyyppi { get; set; }  // SH, LV
        public string Laatu { get; set; }   // Ori, Tamma, Ruuna
        public string Kuva { get; set; }
        public string Omistaja { get; set; }  // Omistajan  nimi
        public string OmisCus { get; set; }  // Omistajan cus
        public string Talli { get; set; } // Tallin nimi
        public string TalliCus { get; set; }  // Tallin cus
        public string Lempinimi { get; set; }
        public string Selvennys { get; set; }
    }
}