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
    public class TallitController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Talli voi hyvin";
        }

        // GET api/<controller>/5
        [HttpGet]
        public Tallit[] Get(string id)
        {
            Tallit[] res = new Tallit[103];
            String commandText;
            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4);

            if (osat[0] == "1")
                commandText = "dbo.Select1Tallit";
            else
                commandText = "dbo.SelectTallit";
            int k = 1;

            SqlParameter parameterCus = new SqlParameter("@Cus", SqlDbType.VarChar)
            {
                Value = osat[1]
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
                        Tallit tieto = new Tallit();
                        tieto.Id = (int)reader["Id"];
                        tieto.Cus = (string)reader["Cus"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.Nimi = (string)reader["Nimi"];
                        tieto.Osoite = (string)reader["Osoite"];
                        tieto.Puhelin = (string)reader["Puhelin"];
                        tieto.Sposti = (string)reader["Sposti"];
                        tieto.Yhthenk = (string)reader["Yhthenk"];
                        tieto.AppSposti = (string)reader["AppSposti"];
                        tieto.Muuta = (string)reader["Muuta"];
                        res[k++] = tieto;
                        if (k == 100)
                            break;
                    }
                    Tallit pituus = new Tallit();
                    pituus.Id = k - 1;
                    pituus.Cus = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.Nimi = "";
                    pituus.Osoite = "";
                    pituus.Puhelin = "";
                    pituus.Sposti = "";
                    pituus.Yhthenk = "";
                    pituus.AppSposti = "";
                    pituus.Muuta = "";
                    res[0] = pituus;
                }
                else
                {
                    Tallit tieto = new Tallit();
                    tieto.Id = 0;
                    res[0] = tieto;
                }

                Array.Resize<Tallit>(ref res, k + 3);
                return res;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] Tallit tieto)
        {
            string aa = "";
            String commandText = "";
            SqlParameter[] pars;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertTallit";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@Nimi", tieto.Nimi);
                SqlParameter par4 = new SqlParameter("@Osoite", tieto.Osoite);
                SqlParameter par5 = new SqlParameter("@Puhelin", tieto.Puhelin);
                SqlParameter par6 = new SqlParameter("@Sposti", tieto.Sposti);
                SqlParameter par7 = new SqlParameter("@Yhthenk", tieto.Yhthenk);
                SqlParameter par8 = new SqlParameter("@AppSposti", tieto.AppSposti);
                SqlParameter par9 = new SqlParameter("@Muuta", tieto.Muuta);
                pars = new SqlParameter[]
                {
                        par1, par2, par3, par4, par5, par6, par7, par8, par9
                };
                aa = "Lisätty ";
            }
            else
            {
                commandText = "dbo.UpdateTallit";

                SqlParameter par1 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@Nimi", tieto.Nimi);
                SqlParameter par4 = new SqlParameter("@Osoite", tieto.Osoite);
                SqlParameter par5 = new SqlParameter("@Puhelin", tieto.Puhelin);
                SqlParameter par6 = new SqlParameter("@Sposti", tieto.Sposti);
                SqlParameter par7 = new SqlParameter("@Yhthenk", tieto.Yhthenk);
                SqlParameter par8 = new SqlParameter("@AppSposti", tieto.AppSposti);
                SqlParameter par9 = new SqlParameter("@Muuta", tieto.Muuta);
                pars = new SqlParameter[]
                {
                        par1, par2, par3, par4, par5, par6, par7, par8, par9
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
        public string Delete(string id)
        {
            String commandText;
            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4);

            commandText = "dbo.DeleteTalli";

            SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
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