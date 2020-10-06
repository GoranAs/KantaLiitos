using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    public class Ovning
    {
        public int Id { get; set; }
        public string Cus { get; set; }
        public DateTime Aika { get; set; }
        public int HevosId { get; set; }
        public string Toiminto { get; set; }
        public string Paikka { get; set; }
        public double Kustannus { get; set; }
        public int Fiilis { get; set; }
        public string Selvennys { get; set; }
        public double Kesto { get; set; }   // kokonaisaika
        public double Matka { get; set; }   // kokonaispituus
        public double Nopeus { get; set; }  // keskimääräinen nopeus
        public int HuoltoId { get; set; }
        public string Har1 { get; set; }
        public string Har2 { get; set; }
        public string Har3 { get; set; }
        public string Har4 { get; set; }
        public string Har5 { get; set; }
        public string Har6 { get; set; }
        public string Har7 { get; set; }
        public string Har8 { get; set; }
        public string Har9 { get; set; }
    }
}