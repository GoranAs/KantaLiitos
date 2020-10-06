using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    public class Talous
    {
        public int Id { get; set; }
        public string Cus { get; set; }
        public DateTime Aika { get; set; }
        public int HevosId { get; set; }
        public string Kohde { get; set; }
        public string Liike { get; set; }
        public double Kustannus { get; set; }
        public string Selvennys { get; set; }
    }
}