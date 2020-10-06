using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    public class OmistajanTallit
    {
        public int Id { get; set; }
        public string OmistajanCus { get; set; }
        public string TallinCus { get; set; }
        public DateTime Aika { get; set; }
        public string Nimi { get; set; }
        public int TallinId { get; set; }
        public int OmistajanId { get; set; }
    }
}