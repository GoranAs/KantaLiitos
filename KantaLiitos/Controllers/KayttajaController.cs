using KantaLiitos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KantaLiitos.Controllers
{
    public class KayttajaController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Kayttaja ok";
        }

        // GET api/<controller>/5
        [HttpGet]
        public Kayttaja[] Get(string id)
        {
            Kayttaja[] res = new Kayttaja[503];

            String commandText = "dbo.SelectKayttaja";
            int k = 1;

            SqlParameter par1 = new SqlParameter("@Cus", id);
            SqlParameter[] pars = new SqlParameter[]
            {
                par1
            };

            // When the direction of parameter is set as Output, you can get the value after 
            // executing the command.

            SqlConnection conn = new SqlConnection(DbCon.connectionString);
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(pars);

                conn.Open();
                // When using CommandBehavior.CloseConnection, the connection will be closed when the 
                // IDataReader is closed.
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Kayttaja tieto = new Kayttaja();
                        tieto.Id = (int)reader["Id"];
                        tieto.Cus = (string)reader["Cus"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.Kaynimi = (string)reader["Kaynimi"];
                        tieto.Salasana = (string)reader["Salasana"];
                        tieto.Nimi = (string)reader["Nimi"];
                        tieto.Osoite = (string)reader["Osoite"];
                        tieto.Pono = (string)reader["Pono"];
                        tieto.Kaupunki = (string)reader["Kaupunki"];
                        tieto.Sposti = (string)reader["Sposti"];
                        tieto.Puhelin = (string)reader["Puhelin"];
                        tieto.TalliId = (int)reader["TalliId"];
                        tieto.Oikeus = (int)reader["Oikeus"];
                        res[k++] = tieto;
                        if (k == 500)
                            break;
                    }
                    Kayttaja pituus = new Kayttaja();
                    pituus.Id = k - 1;
                    pituus.Cus = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.Kaynimi = "";
                    pituus.Salasana = "";
                    pituus.Nimi = "";
                    pituus.Osoite = "";
                    pituus.Pono = "";
                    pituus.Kaupunki = "";
                    pituus.Sposti = "";
                    pituus.Puhelin = "";
                    pituus.TalliId = 0;
                    pituus.Oikeus = 0;
                    res[0] = pituus;
                }
                else
                {
                    Kayttaja tieto = new Kayttaja();
                    tieto.Id = 0;
                    res[0] = tieto;
                }
                Array.Resize<Kayttaja>(ref res, k + 3);
                return res;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] Kayttaja tieto)
        {
            string aa = "";
            SqlParameter[] pars;
            String commandText;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertKayttaja";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@Kaynimi", tieto.Kaynimi);
                SqlParameter par4 = new SqlParameter("@Salasana", tieto.Salasana);
                SqlParameter par5 = new SqlParameter("@Nimi", tieto.Nimi);
                SqlParameter par6 = new SqlParameter("@Osoite", tieto.Osoite);
                SqlParameter par7 = new SqlParameter("@Pono", tieto.Pono);
                SqlParameter par8 = new SqlParameter("@Kaupunki", tieto.Kaupunki);
                SqlParameter par9 = new SqlParameter("@Sposti", tieto.Sposti);
                SqlParameter par10 = new SqlParameter("@Puhelin", tieto.Puhelin);
                SqlParameter par11 = new SqlParameter("@TalliId", tieto.TalliId);
                SqlParameter par12 = new SqlParameter("@Oikeus", tieto.Oikeus);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6, par7, par8, par9, par10, par11, par12
                };
                aa = "Lisätty ";
            }
            else
            {
                commandText = "dbo.UpdateKayttaja";

                SqlParameter par1 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@Kaynimi", tieto.Kaynimi);
                SqlParameter par4 = new SqlParameter("@Salasana", tieto.Salasana);
                SqlParameter par5 = new SqlParameter("@Nimi", tieto.Nimi);
                SqlParameter par6 = new SqlParameter("@Osoite", tieto.Osoite);
                SqlParameter par7 = new SqlParameter("@Pono", tieto.Pono);
                SqlParameter par8 = new SqlParameter("@Kaupunki", tieto.Kaupunki);
                SqlParameter par9 = new SqlParameter("@Sposti", tieto.Sposti);
                SqlParameter par10 = new SqlParameter("@Puhelin", tieto.Puhelin);
                SqlParameter par11 = new SqlParameter("@TalliId", tieto.TalliId);
                SqlParameter par12 = new SqlParameter("@Oikeus", tieto.Oikeus);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6, par7, par8, par9, par10, par11, par12
                };
                aa = "Muutettu ";
            }

            using (SqlConnection conn = new SqlConnection(DbCon.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect 
                    // type is only for OLE DB.  
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(pars);
                    try
                    {
                        conn.Open();
                        int stat = cmd.ExecuteNonQuery();
                        return aa + stat.ToString() + " rivi";
                    }
                    catch (Exception exception)
                    {
                        return exception.Message;
                    }
                }
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public string Put(string id, [FromBody] Kayttaja tieto)
        {
            String commandText;
            commandText = "dbo.UpdateKayttaja";

            SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
            SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
            SqlParameter par3 = new SqlParameter("@Kaynimi", tieto.Kaynimi);
            SqlParameter par4 = new SqlParameter("@Salasana", tieto.Salasana);
            SqlParameter par5 = new SqlParameter("@Nimi", tieto.Nimi);
            SqlParameter par6 = new SqlParameter("@Osoite", tieto.Osoite);
            SqlParameter par7 = new SqlParameter("@Pono", tieto.Pono);
            SqlParameter par8 = new SqlParameter("@Kaupunki", tieto.Kaupunki);
            SqlParameter par9 = new SqlParameter("@Sposti", tieto.Sposti);
            SqlParameter par10 = new SqlParameter("@Puhelin", tieto.Puhelin);
            SqlParameter[] pars = new SqlParameter[]
            {
                par1, par2, par3, par4, par5, par6, par7, par8, par9, par10
            };

            using (SqlConnection conn = new SqlConnection(DbCon.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect 
                    // type is only for OLE DB.  
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(pars);
                    try
                    {
                        conn.Open();
                        int stat = cmd.ExecuteNonQuery();
                        return "Muutettu " + stat.ToString() + " rivi";
                    }
                    catch (Exception exception)
                    {
                        return exception.Message;
                    }
                }
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public string Delete(string id)
        {
            String commandText = "";
            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4);
            if (osat[0] == "1")
                commandText = "dbo.DeleteUusiKayttaja";

            SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
            SqlParameter par2 = new SqlParameter("@Id", Convert.ToInt32(osat[2]));
            SqlParameter[] pars = new SqlParameter[]
            {
                par1, par2
            };

            using (SqlConnection conn = new SqlConnection(DbCon.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect 
                    // type is only for OLE DB.  
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(pars);
                    try
                    {
                        conn.Open();
                        int stat = cmd.ExecuteNonQuery();
                        return "Poistettu " + stat.ToString() + " rivi";
                    }
                    catch (Exception exception)
                    {
                        return exception.Message;
                    }
                }
            }
        }
    }
}