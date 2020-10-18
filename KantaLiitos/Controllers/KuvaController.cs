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

            SqlParameter par1 = new SqlParameter("@Nimi", id);
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
                        Kuva tieto = new Kuva();
                        tieto.Id = (int)reader["Id"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.Nimi = (string)reader["Nimi"];
                        tieto.Data = (byte[])reader["Data"];
                        res[k++] = tieto;
                        if (k == 500)
                            break;
                    }
                    Kuva pituus = new Kuva();
                    pituus.Id = k - 1;
                    pituus.Aika = DateTime.MinValue;
                    pituus.Nimi = "";
                    pituus.Data = null;
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

            SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
            SqlParameter par2 = new SqlParameter("@Nimi", tieto.Nimi);
            SqlParameter par3 = new SqlParameter("@Data", tieto.Data);
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
                        return "Lisätty " + stat.ToString() + " rivi";
                    }
                    catch (Exception exception)
                    {
                        return exception.Message;
                    }
                }
            }
        }

        // PUT: api/Kuva/5
        [HttpPut]
        public string Put(string id, [FromBody] Kuva tieto)
        {
            String commandText;
            commandText = "dbo.UpdateKuva";

            SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
            SqlParameter par2 = new SqlParameter("@Nimi", tieto.Nimi);
            SqlParameter par3 = new SqlParameter("@Data", tieto.Data);
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

            SqlParameter par1 = new SqlParameter("@Nimi", osat[1]);
            SqlParameter[] pars = new SqlParameter[]
            {
                par1
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
