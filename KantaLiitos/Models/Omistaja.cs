using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    public class Omistaja
    {
        public int Id { get; set; }
        public string Cus { get; set; }
        public DateTime Aika { get; set; }
        public string Nimi { get; set; }
        public string Osoite { get; set; }
        public string Puhelin { get; set; }
        public string Sposti { get; set; }
        public string Muuta { get; set; }
        public string Kaynimi { get; set; }
        public string Salasana { get; set; }
    }
}