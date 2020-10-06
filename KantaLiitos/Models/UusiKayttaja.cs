using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    public class UusiKayttaja
    {
        public int Id { get; set; }
        public string Cus { get; set; }     // Käytön Cus
        public DateTime Aika { get; set; }
        public string Kaynimi { get; set; } // Kirjaudutaan tällä
        public string Salasana { get; set; }
        public string Nimi { get; set; }    // Käyttäjän nimi
        public string TalliNimi { get; set; }    // Tallin nimi
        public string OmistajaCus { get; set; } // Omistaja Cus tai tyhjä
        public string TalliCus { get; set; }    // Tallin Cus
        public int Oikeus { get; set; }     // 1=talli=kirjoitus, 2=omistaja=luku
        public string Muuta { get; set; }
    }
}