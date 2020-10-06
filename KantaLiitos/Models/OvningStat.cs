using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    public class OvningStat
    {
        public string Toiminto { get; set; }
        public int Nr { get; set; }     // count(toiminto)
        public double Len { get; set; } // kokonaismatka
        public double AvgLen { get; set; } // keskimääräinen matka
        public double Tid { get; set; } // kokonaisaika
        public double AvgNop { get; set; } // keskimääräinen nopeus
    }
}