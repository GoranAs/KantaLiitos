using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    
    public class Astutus
    {
        public int Id { get; set; }
        public string Cus { get; set; }
        public DateTime Aika { get; set; }
        public int HevosId { get; set; }
        public string Ori { get; set; }
        public string OriTunnus { get; set; }
        public string Tamma { get; set; }
        public string TammaTunnus { get; set; }
        public double Matka { get; set; }
        public string Paikka { get; set; }
        public double Kustannus { get; set; }
        public string Selvennys { get; set; }
    }
}