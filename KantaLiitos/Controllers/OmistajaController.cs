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
    public class OmistajaController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Omistaja tehty";
        }

        // GET api/<controller>/5
        [HttpGet]
        public Omistaja[] Get(string id)
        {
            Omistaja[] res = new Omistaja[503];

            string commandText = "dbo.SelectOmistaja";
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
                        Omistaja tieto = new Omistaja();
                        tieto.Id = (int)reader["Id"];
                        tieto.Cus = (string)reader["Cus"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.Nimi = (string)reader["Nimi"];
                        tieto.Osoite = (string)reader["Osoite"];
                        tieto.Puhelin = (string)reader["Puhelin"];
                        tieto.Sposti = (string)reader["Sposti"];
                        tieto.Muuta = (string)reader["Muuta"];
                        tieto.Kaynimi = (string)reader["Kaynimi"];
                        tieto.Salasana = (string)reader["Salasana"];
                        res[k++] = tieto;
                        if (k == 500)
                            break;
                    }
                    Omistaja pituus = new Omistaja();
                    pituus.Id = k - 1;
                    pituus.Cus = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.Nimi = "";
                    pituus.Osoite = "";
                    pituus.Puhelin = "";
                    pituus.Sposti = "";
                    pituus.Muuta = "";
                    pituus.Kaynimi = "";
                    pituus.Salasana = "";
                    res[0] = pituus;
                }
                else
                {
                    Omistaja tieto = new Omistaja();
                    tieto.Id = 0;
                    res[0] = tieto;
                }
                Array.Resize<Omistaja>(ref res, k);
                return res;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] Omistaja tieto)
        {
            string aa = "";
            SqlParameter[] pars;
            String commandText;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertOmistaja";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@Nimi", tieto.Nimi);
                SqlParameter par4 = new SqlParameter("@Osoite", tieto.Osoite);
                SqlParameter par5 = new SqlParameter("@Puhelin", tieto.Puhelin);
                SqlParameter par6 = new SqlParameter("@Sposti", tieto.Sposti);
                SqlParameter par7 = new SqlParameter("@Muuta", tieto.Muuta);
                SqlParameter par8 = new SqlParameter("@Kaynimi", tieto.Kaynimi);
                SqlParameter par9 = new SqlParameter("@Salasana", tieto.Salasana);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6, par7, par8, par9
                };
                aa = "Lisätty ";
            }
            else
            {
                commandText = "dbo.UpdateOmistaja";

                SqlParameter par1 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@Nimi", tieto.Nimi);
                SqlParameter par4 = new SqlParameter("@Osoite", tieto.Osoite);
                SqlParameter par5 = new SqlParameter("@Puhelin", tieto.Puhelin);
                SqlParameter par6 = new SqlParameter("@Sposti", tieto.Sposti);
                SqlParameter par7 = new SqlParameter("@Muuta", tieto.Muuta);
                SqlParameter par8 = new SqlParameter("@Kaynimi", tieto.Kaynimi);
                SqlParameter par9 = new SqlParameter("@Salasana", tieto.Salasana);
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
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public string Delete(string id)
        {
            String commandText;
            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4);

            commandText = "dbo.DeleteOmistaja";

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