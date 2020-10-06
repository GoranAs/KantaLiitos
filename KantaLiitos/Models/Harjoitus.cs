using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    public class Harjoitus
    {
        public int Id { get; set; }
        public string Cus { get; set; }
        public DateTime Aika { get; set; }
        public int HevosId { get; set; }
        public string Toiminto { get; set; }
        public int Sarjat { get; set; }
        public int Toistot { get; set; }
        public int Teho { get; set; }
        public int Matka { get; set; }
        public int Toistovali { get; set; }
        public int Sarjavali { get; set; }
        public string Paikka { get; set; }
        public double Kustannus { get; set; }
        public string Selvennys { get; set; }
        public double Kesto { get; set; }
    }

    public class Harjoitus1
    {
        public int Id { get; set; }
        public string Cus { get; set; }
        public DateTime Aika { get; set; }
        public int HevosId { get; set; }
        public string Toiminto { get; set; }
        public int Toistot { get; set; }
        public double Tehonopeus { get; set; }
        public double Tehoaika { get; set; }
        public double Matka { get; set; }
        public double Toistovali { get; set; }
        public string Paikka { get; set; }
        public double Kustannus { get; set; }
        public int Fiilis { get; set; }
        public string Selvennys { get; set; }
        public double Kesto { get; set; }
        public int HuoltoId { get; set; }
        public string Kuvaus { get; set; }
    }
}