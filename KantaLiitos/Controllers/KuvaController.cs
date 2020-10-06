using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KantaLiitos.Models;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace KantaLiitos.Controllers
{
    public class KuvaController : ApiController
    {
        // GET: api/Kuva
        [HttpGet]
        public string Get()
        {
            return "Kuva saatu";
        }

        // GET: api/Kuva/5
        [HttpGet]
        public Kuva[] Get(string id)
        {
            Kuva[] res = new Kuva[503];

            String commandText = "dbo.SelectKuva";
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
                        Kuva tieto = new Kuva();
                        tieto.Id = (int)reader["Id"];
                        tieto.Cus = (string)reader["Cus"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.HevosId = (int)reader["HevosId"];
                        tieto.Tunnus = (string)reader["Tunnus"];
                        tieto.Nimi = (string)reader["Nimi"];
                        tieto.Laakari = (string)reader["Laakari"];
                        tieto.Sairaala = (string)reader["Sairaala"];
                        tieto.Data = (byte[])reader["Data"];
                        tieto.Kustannus = (double)reader["Kustannus"];
                        tieto.Selvennys = (string)reader["Selvennys"];
                        tieto.Tyyppi = (int)reader["Tyyppi"];
                        tieto.Tiedosto = (string)reader["Tiedosto"];
                        res[k++] = tieto;
                        if (k == 500)
                            break;
                    }
                    Kuva pituus = new Kuva();
                    pituus.Id = k - 1;
                    pituus.Cus = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.HevosId = 0;
                    pituus.Tunnus = "";
                    pituus.Nimi = "";
                    pituus.Laakari = "";
                    pituus.Sairaala = "";
                    pituus.Data = null;
                    pituus.Kustannus = 0;
                    pituus.Selvennys = "";
                    pituus.Tyyppi = 0;
                    pituus.Tiedosto = "";
                    res[0] = pituus;
                }
                else
                {
                    Kuva tieto = new Kuva();
                    tieto.Id = 0;
                    res[0] = tieto;
                }
                Array.Resize<Kuva>(ref res, k + 3);
                return res;
            }
        }

        // POST: api/Kuva
        [HttpPost]
        public string Post([FromBody] Kuva tieto)
        {
            String commandText;
            commandText = "dbo.InsertKuva";

            SqlParameter par1 = new SqlParameter("@Cus", tieto.Cus);
            SqlParameter par2 = new SqlParameter("@Aika", tieto.Aika);
            SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
            SqlParameter par4 = new SqlParameter("@Tunnus", tieto.Tunnus);
            SqlParameter par5 = new SqlParameter("@Nimi", tieto.Nimi);
            SqlParameter par6 = new SqlParameter("@Laakari", tieto.Laakari);
            SqlParameter par7 = new SqlParameter("@Sairaala", tieto.Sairaala);
            SqlParameter par8 = new SqlParameter("@Data", tieto.Data);
            SqlParameter par9 = new SqlParameter("@Kustannus", tieto.Kustannus);
            SqlParameter par10 = new SqlParameter("@Selvennys", tieto.Selvennys);
            SqlParameter par11 = new SqlParameter("@Tyyppi", tieto.Tyyppi);
            SqlParameter par12 = new SqlParameter("@Tiedosto", tieto.Tiedosto);
            SqlParameter[] pars = new SqlParameter[]
            {
                par1, par2, par3, par4, par5, par6, par7, par8, par9, par10, par11, par12
            };

            return "Tähän asti ok";
            /*
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
                        return "Lisätty " + stat.ToString() + " rivi";
                    }
                    catch (Exception exception)
                    {
                        return exception.Message;
                    }
                }
            }
            */
        }

        // PUT: api/Kuva/5
        [HttpPut]
        public string Put(string id, [FromBody] Kuva tieto)
        {
            String commandText;
            commandText = "dbo.UpdateKuva";

            SqlParameter par1 = new SqlParameter("@Cus", tieto.Cus);
            SqlParameter par2 = new SqlParameter("@Aika", tieto.Aika);
            SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
            SqlParameter par4 = new SqlParameter("@Tunnus", tieto.Tunnus);
            SqlParameter par5 = new SqlParameter("@Nimi", tieto.Nimi);
            SqlParameter par6 = new SqlParameter("@Laakari", tieto.Laakari);
            SqlParameter par7 = new SqlParameter("@Sairaala", tieto.Sairaala);
            SqlParameter par8 = new SqlParameter("@Data", tieto.Data);
            SqlParameter par9 = new SqlParameter("@Kustannus", tieto.Kustannus);
            SqlParameter par10 = new SqlParameter("@Selvennys", tieto.Selvennys);
            SqlParameter par11 = new SqlParameter("@Tyyppi", tieto.Tyyppi);
            SqlParameter par12 = new SqlParameter("@Tiedosto", tieto.Tiedosto);
            SqlParameter[] pars = new SqlParameter[]
            {
                par1, par2, par3, par4, par5, par6, par7, par8, par9, par10, par11, par12
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

        // DELETE: api/Kuva/5
        [HttpDelete]
        public string Delete(string id)
        {
            String commandText;
            string[] osat = new string[6];
            osat = id.Split(new char[] { '_' }, 5, StringSplitOptions.RemoveEmptyEntries);

            if (osat[0] == "1")
                commandText = "dbo.Delete1Kuva";
            else
                commandText = "dbo.DeleteKuva";
            if (osat[3].IndexOf('#') != -1)
                osat[3].Replace('#', '-');

            SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
            SqlParameter par2 = new SqlParameter("@HevosId", osat[2]);
            SqlParameter par3 = new SqlParameter("@Tiedosto", osat[3]);
            SqlParameter[] pars = new SqlParameter[]
            {
                par1, par2, par3
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
                        return "Poistettu " + /*stat.ToString()*/ osat[1] + " " + osat[2] + " " + osat[3] + " rivi";
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
