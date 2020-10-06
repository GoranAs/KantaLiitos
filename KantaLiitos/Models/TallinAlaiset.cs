using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    public class TallinAlaiset
    {
        public DateTime Aika { get; set; }
        public string Tunn { get; set; }
        public string Nimi { get; set; }
        public string Rek { get; set; }
        public string Tyyppi { get; set; }
        public string Laatu { get; set; }
        public string Muuta { get; set; }
    }
}