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
    public class OmistajanTallitController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Talli saatu";
        }

        // GET api/<controller>/5
        [HttpGet]
        public OmistajanTallit[] Get(string id)
        {
            OmistajanTallit[] res = new OmistajanTallit[103];
            String commandText;
            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4);

            if (osat[0] == "1")
                commandText = "dbo.Select1OmistajanTallit";
            else
                commandText = "dbo.SelectOmistajanTallit";
            int k = 1;

            SqlParameter parameterOmistajanCus = new SqlParameter("@OmistajanCus", SqlDbType.VarChar)
            {
                Value = osat[1]
            };
            SqlParameter parameterTallinCus = new SqlParameter("@TallinCus", SqlDbType.VarChar)
            {
                Value = osat[2]
            };
            // When the direction of parameter is set as Output, you can get the value after 
            // executing the command.

            SqlConnection conn = new SqlConnection(DbCon.connectionString);
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(parameterOmistajanCus);
                cmd.Parameters.Add(parameterTallinCus);

                conn.Open();
                // When using CommandBehavior.CloseConnection, the connection will be closed when the 
                // IDataReader is closed.
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OmistajanTallit tieto = new OmistajanTallit();
                        tieto.Id = (int)reader["Id"];
                        tieto.OmistajanCus = (string)reader["OmistajanCus"];
                        tieto.TallinCus = (string)reader["TallinCus"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.Nimi = (string)reader["Nimi"];
                        tieto.OmistajanId = (int)reader["OmistajanId"];
                        tieto.TallinId = (int)reader["TallinId"];
                        res[k++] = tieto;
                        if (k == 100)
                            break;
                    }
                    OmistajanTallit pituus = new OmistajanTallit();
                    pituus.Id = k - 1;
                    pituus.OmistajanCus = "";
                    pituus.TallinCus = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.Nimi = "";
                    pituus.OmistajanId = 0;
                    pituus.TallinId = 0;
                    res[0] = pituus;
                }
                else
                {
                    OmistajanTallit tieto = new OmistajanTallit();
                    tieto.Id = 0;
                    res[0] = tieto;
                }

                Array.Resize<OmistajanTallit>(ref res, k + 3);
                return res;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] OmistajanTallit tieto)
        {
            string aa = "";
            String commandText = "";
            SqlParameter[] pars;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertOmistajanTallit";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@OmistajanCus", tieto.OmistajanCus);
                SqlParameter par3 = new SqlParameter("@TallinCus", tieto.TallinCus);
                SqlParameter par4 = new SqlParameter("@Nimi", tieto.Nimi);
                SqlParameter par5 = new SqlParameter("@TallinId", tieto.TallinId);
                SqlParameter par6 = new SqlParameter("@OmistajanId", tieto.OmistajanId);
                pars = new SqlParameter[]
                {
                        par1, par2, par3, par4, par5, par6
                };
                aa = "Lisätty ";
            }
            else
            {
                commandText = "dbo.UpdateOmistajanTallit";

                SqlParameter par1 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par2 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par3 = new SqlParameter("@OmistajanCus", tieto.OmistajanCus);
                SqlParameter par4 = new SqlParameter("@TallinCus", tieto.TallinCus);
                SqlParameter par5 = new SqlParameter("@Nimi", tieto.Nimi);
                SqlParameter par6 = new SqlParameter("@TallinId", tieto.TallinId);
                SqlParameter par7 = new SqlParameter("@OmistajanId", tieto.OmistajanId);
                pars = new SqlParameter[]
                {
                        par1, par2, par3, par4, par5, par6, par7
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

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public string Delete(string id)
        {
            String commandText;
            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4);

            if (osat[0] == "1")
                commandText = "dbo.Delete1OmistajanTallit";
            else
                commandText = "dbo.DeleteOmistajanTallit";
            SqlParameter par1 = new SqlParameter("@OmistajanCus", osat[1]);
            SqlParameter par2 = new SqlParameter("@TallinCus", osat[2]);
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