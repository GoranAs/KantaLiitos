using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KantaLiitos.Models
{
    public static class DbCon
    {
        /*
        public static string connectionString = "Data Source = (local);" +
            "user id = HkhLogin;" +
            "password = Hkh_Salasana1;" +
            "Initial Catalog = HkhKanta;" +
            //               "Trusted_Connection = True;" +
            //             "Integrated Security = True;" +
            "Asynchronous Processing = True;";
        */
        public static string connectionString =
            "Server = tcp:hkhkanta.database.windows.net,1433;" +
            "Initial Catalog = hkhkanta; Persist Security Info=False;" +
            "User ID = halpa; Password={+Barajagsjalv-};" +
            "MultipleActiveResultSets=False;Encrypt=False;" +
            "TrustServerCertificate=False;Connection Timeout = 30;";
    }

    [Serializable]
    public class Hevonen
    {
        public int Id { get; set; }
        public string Cus { get; set; }
        public DateTime Aika { get; set; }
        public string Tunnus { get; set; }
        public string Nimi { get; set; }
        public string Tyyppi { get; set; }
        public string Laatu { get; set; }
        public string Kuva { get; set; }
        public string Omistaja { get; set; }
        public string Osoite { get; set; }
        public string Puhelin { get; set; }
        public string Sposti { get; set; }
        public string Lempinimi { get; set; }
    }
}