using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    public class Toiminta
    {
        public DateTime Aika { get; set; }
        public string Tyyppi { get; set; }
        public int Id { get; set; }
        public string Toiminto { get; set; }
        public double Kustannus { get; set; }
        public double Matka { get; set; }
        public int Toistot { get; set; }
        public double Palkinto { get; set; }
        public double Palkkio { get; set; }
        public double Kesto { get; set; }
        public double Tehoaika { get; set; }
        public string Muuta { get; set; }
    }
}
