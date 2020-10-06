using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    public class Kilpailu
    {
        public int Id { get; set; }
        public string Cus { get; set; }
        public DateTime Aika { get; set; }
        public int HevosId { get; set; }
        public int Sijoitus { get; set; }
        public string Ohjastaja { get; set; }
        public double Palkinto { get; set; }
        public string Paikka { get; set; }
        public double Palkkio { get; set; }
        public double Kustannus { get; set; }
        public string Selvennys { get; set; }
    }
}