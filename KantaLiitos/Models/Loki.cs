using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    public class Loki
    {
        public string Aika { get; set; }
        public string Cus { get; set; }
        public string HevosId { get; set; }
        public string Nimi { get; set; }
        public string Toiminta { get; set; }    // Harjoitus, Huolto, Terveys, Kilpailu, Talous
        public string Suunta { get; set; }      // Talletus, Luku, Muutos, Poisto
        public string Tieto { get; set; }      // Toimintaan liittyvää tietoa        
    }
}