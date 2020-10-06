using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    public class Terveys
    {
        public int Id { get; set; }
        public string Cus { get; set; }
        public DateTime Aika { get; set; }
        public int HevosId { get; set; }
        public string Toiminto { get; set; }
        public string Laakari { get; set; }
        public string Sairaala { get; set; }
        public double Kustannus { get; set; }
        public string Selvennys { get; set; }
        public string Tiedosto { get; set; }
        public double Kuume { get; set; }
    }
}