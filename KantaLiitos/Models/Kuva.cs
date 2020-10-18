using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    public class Kuva
    {
        public int Id { get; set; }    
        public DateTime Aika { get; set; }
        public string Nimi { get; set; }
        public byte[] Data { get; set; }
     }
}