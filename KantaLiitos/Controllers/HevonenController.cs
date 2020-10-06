using KantaLiitos.Models;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace KantaLiitos.Controllers
{
    public class HevonenController : ApiController
    {
        // GET api/hevonen
        [HttpGet]
        public string Get()
        {
            return "Hevonen laukkaa";
        }

        // GET api/hevonen/5
        [HttpGet]
        public Hevonen[] Get(string id)
        {
            Hevonen[] res = new Hevonen[503];

            String commandText = "dbo.SelectHevonen";
            int k = 1;

            SqlParameter parameterCus = new SqlParameter("@Cus", SqlDbType.VarChar)
            {
                Value = id
            };
            // When the direction of parameter is set as Output, you can get the value after 
            // executing the command.
            
                SqlConnection conn = new SqlConnection(DbCon.connectionString);
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(parameterCus);

                conn.Open();
                // When using CommandBehavior.CloseConnection, the connection will be closed when the 
                // IDataReader is closed.
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Hevonen tieto = new Hevonen();
                        tieto.Id = (int)reader["Id"];
                        tieto.Cus = (string)reader["Cus"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.Tunnus = (string)reader["Tunnus"];
                        tieto.Nimi = (string)reader["Nimi"];
                        tieto.Tyyppi = (string)reader["Tyyppi"];
                        tieto.Laatu = (string)reader["Laatu"];
                        tieto.Kuva = (string)reader["Kuva"];
                        
                        tieto.Omistaja = (string)reader["Omistaja"];
                        
                        tieto.Osoite = (string)reader["Osoite"];
                        tieto.Puhelin = (string)reader["Puhelin"];
                        tieto.Sposti = (string)reader["Sposti"];
                        tieto.Lempinimi = (string)reader["Lempinimi"];
                        
                        if (tieto.Omistaja == null)
                            tieto.Omistaja = "";
                        if (tieto.Osoite == null)
                            tieto.Osoite = "";
                        if (tieto.Puhelin == null)
                            tieto.Puhelin = "";
                        if (tieto.Sposti == null)
                            tieto.Sposti = "";
                        if (tieto.Lempinimi == null)
                            tieto.Lempinimi = "";

                        res[k++] = tieto;
                        if (k == 500)
                            break;
                    }
                    Hevonen pituus = new Hevonen();
                    pituus.Id = k - 1;
                    pituus.Cus = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.Tunnus = "";
                    pituus.Nimi = "";
                    pituus.Tyyppi = "";
                    pituus.Laatu = "";
                    pituus.Kuva = "";
                    pituus.Omistaja = "";
                    pituus.Osoite = "";
                    pituus.Puhelin = "";
                    pituus.Sposti = "";
                    pituus.Lempinimi = "";
                    res[0] = pituus;
                }
                else
                {
                    Hevonen tieto = new Hevonen();
                    tieto.Id = 0;
                    res[0] = tieto;
                }

                Array.Resize<Hevonen>(ref res, k);
                return res;
            }        
        }

        // POST api/hevonen
        [HttpPost]
        public string Post([FromBody] Hevonen tieto)
        {
            string aa = "";
            String commandText = "";
            SqlParameter[] pars;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertHevonen";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@Tunnus", tieto.Tunnus);
                SqlParameter par4 = new SqlParameter("@Nimi", tieto.Nimi);
                SqlParameter par5 = new SqlParameter("@Tyyppi", tieto.Tyyppi);
                SqlParameter par6 = new SqlParameter("@Laatu", tieto.Laatu);
                SqlParameter par7 = new SqlParameter("@Kuva", tieto.Kuva);
                SqlParameter par8 = new SqlParameter("@Omistaja", tieto.Omistaja);
                SqlParameter par9 = new SqlParameter("@Osoite", tieto.Osoite);
                SqlParameter par10 = new SqlParameter("@Puhelin", tieto.Puhelin);
                SqlParameter par11 = new SqlParameter("@Sposti", tieto.Sposti);
                SqlParameter par12 = new SqlParameter("@Lempinimi", tieto.Lempinimi);
                pars = new SqlParameter[]
                {
                        par1, par2, par3, par4, par5, par6, par7, par8, par9, par10, par11, par12
                };
                aa = "Lisätty ";
            }
            else
            {
                commandText = "dbo.UpdateHevonen";

                SqlParameter par1 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par2 = new SqlParameter("@Tunnus", tieto.Tunnus);
                SqlParameter par3 = new SqlParameter("@Nimi", tieto.Nimi);
                SqlParameter par4 = new SqlParameter("@Tyyppi", tieto.Tyyppi);
                SqlParameter par5 = new SqlParameter("@Laatu", tieto.Laatu);
                SqlParameter par6 = new SqlParameter("@Kuva", tieto.Kuva);
                SqlParameter par7 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par8 = new SqlParameter("@Omistaja", tieto.Omistaja);
                SqlParameter par9 = new SqlParameter("@Osoite", tieto.Osoite);
                SqlParameter par10 = new SqlParameter("@Puhelin", tieto.Puhelin);
                SqlParameter par11 = new SqlParameter("@Sposti", tieto.Sposti);
                SqlParameter par12 = new SqlParameter("@Lempinimi", tieto.Lempinimi);
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
                        return aa + stat.ToString() + " rivi(ä)";
                    }
                    catch (Exception exception)
                    {
                        return exception.Message;
                    }
                }
            }
        }

        // PUT api/hevonen -- HUOM PUT ei toimi
        [HttpPut]
        public string Put(string id, [FromBody] Hevonen tieto)
        {
            String commandText;
            commandText = "dbo.UpdateHevonen";

            SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
            SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
            SqlParameter par3 = new SqlParameter("@Tunnus", tieto.Tunnus);
            SqlParameter par4 = new SqlParameter("@Nimi", tieto.Nimi);
            SqlParameter par5 = new SqlParameter("@Tyyppi", tieto.Tyyppi);
            SqlParameter par6 = new SqlParameter("@Laatu", tieto.Laatu);
            SqlParameter par7 = new SqlParameter("@Kuva", tieto.Kuva);
            SqlParameter[] pars = new SqlParameter[]
            {
                par1, par2, par3, par4, par5, par6, par7
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

        // DELETE api/hevonen/5
        [HttpDelete]
        public string Delete(string id)
        {
            String commandText;
            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4);

            if (osat[0] == "1")
                commandText = "dbo.Delete1Hevonen";
            else
                commandText = "dbo.DeleteHevonen";
/*
            if (osat[2].IndexOf('#') != -1)
                osat[2].Replace('#', '-');
*/
            SqlParameter par2 = new SqlParameter("@Cus", osat[1]);
            SqlParameter par4 = new SqlParameter("@Id", Convert.ToInt32(osat[2]));
            SqlParameter[] pars = new SqlParameter[]
            {
                par2, par4
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