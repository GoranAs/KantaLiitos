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
    public class KilpailuController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Kilpailu voitettu";
        }

        // GET api/<controller>/5
        [HttpGet]
        public Kilpailu[] Get(string id)
        {
            Kilpailu[] res = new Kilpailu[503];
            int hepo = 0;
            int kesto = 0;
            String commandText = "";
            SqlParameter[] pars = new SqlParameter[6];
            int k = 1;
            string[] osat = new string[5];
            string[] osat1 = new string[5];

            osat1 = id.Split(new char[] { '_' }, 2, StringSplitOptions.None);
            if (osat1[0] == "0")
            {
                osat = osat1[1].Split(new char[] { '_' }, 4, StringSplitOptions.None);
                commandText = "dbo.SelectKaikkiKilpailu";

                SqlParameter par1 = new SqlParameter("@Cus", osat[0]);
                pars = new SqlParameter[]
                {
                    par1
                };
            }
            if (osat1[0] == "1")
            {
                osat = osat1[1].Split(new char[] { '_' }, 4, StringSplitOptions.None);
                hepo = Convert.ToInt32(osat[1]);
                kesto = Convert.ToInt32(osat[2]);
                if (kesto == 0)
                {
                    commandText = "dbo.SelectKilpailu";
                }
                else
                {
                    kesto = -1 * kesto;
                    commandText = "dbo.SelectAikaKilpailu";
                }
                SqlParameter par1 = new SqlParameter("@Cus", osat[0]);
                SqlParameter par2 = new SqlParameter("@HevosId", hepo);
                SqlParameter par3 = new SqlParameter("@Kesto", kesto);
                pars = new SqlParameter[]
                {
                    par1, par2, par3
                };
            }
            else if (osat1[0] == "2")
            {
                osat = osat1[1].Split(new char[] { '_' }, 2, StringSplitOptions.None);
                hepo = Convert.ToInt32(osat[1]);
                commandText = "dbo.SelectLastKilpailu";
                SqlParameter par1 = new SqlParameter("@Cus", osat[0]);
                SqlParameter par2 = new SqlParameter("@HevosId", hepo);
                pars = new SqlParameter[]
                {
                    par1, par2
                };
            }

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
                        Kilpailu tieto = new Kilpailu()
                        {
                            Id = (int)reader["Id"],
                            Cus = (string)reader["Cus"],
                            Aika = (DateTime)reader["Aika"],
                            HevosId = (int)reader["HevosId"],
                            Sijoitus = (int)reader["Sijoitus"],
                            Ohjastaja = (string)reader["Ohjastaja"],
                            Palkinto = (double)reader["Palkinto"],
                            Paikka = (string)reader["Paikka"],
                            Palkkio = (double)reader["Palkkio"],
                            Kustannus = (double)reader["Kustannus"],
                            Selvennys = (string)reader["Selvennys"]
                        };
                        res[k++] = tieto;
                        if (k == 500)
                            break;
                    }
                    Kilpailu pituus = new Kilpailu
                    {
                        Id = k - 1,
                        Cus = "",
                        Aika = DateTime.MinValue,
                        HevosId = 0,
                        Sijoitus = 0,
                        Ohjastaja = "",
                        Palkinto = 0,
                        Paikka = "",
                        Palkkio = 0,
                        Kustannus = 0,
                        Selvennys = ""
                    };
                    res[0] = pituus;
                }
                else
                {
                    Kilpailu tieto = new Kilpailu
                    {
                        Id = 0
                    };
                    res[0] = tieto;
                }
                Array.Resize<Kilpailu>(ref res, k + 3);
                return res;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] Kilpailu tieto)
        {
            string aa = "";
            SqlParameter[] pars;
            String commandText;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertKilpailu";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
                SqlParameter par4 = new SqlParameter("@Sijoitus", tieto.Sijoitus);
                SqlParameter par5 = new SqlParameter("@Ohjastaja", tieto.Ohjastaja);
                SqlParameter par6 = new SqlParameter("@Palkinto", tieto.Palkinto);
                SqlParameter par7 = new SqlParameter("@Paikka", tieto.Paikka);
                SqlParameter par8 = new SqlParameter("@Palkkio", tieto.Palkkio);
                SqlParameter par9 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par10 = new SqlParameter("@Selvennys", tieto.Selvennys);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6, par7, par8, par9, par10
                };
                aa = "Lisätty ";
            }
            else
            {
                commandText = "dbo.UpdateKilpailu";

                SqlParameter par1 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par2 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par3 = new SqlParameter("@Sijoitus", tieto.Sijoitus);
                SqlParameter par4 = new SqlParameter("@Ohjastaja", tieto.Ohjastaja);
                SqlParameter par5 = new SqlParameter("@Palkinto", tieto.Palkinto);
                SqlParameter par6 = new SqlParameter("@Paikka", tieto.Paikka);
                SqlParameter par7 = new SqlParameter("@Palkkio", tieto.Palkkio);
                SqlParameter par8 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par9 = new SqlParameter("@Selvennys", tieto.Selvennys);
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
        [HttpPut]
        public string Put(string id, [FromBody] Kilpailu tieto)
        {

            String commandText;
            commandText = "dbo.UpdateKilpailu";

            SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
            SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
            SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
            SqlParameter par4 = new SqlParameter("@Sijoitus", tieto.Sijoitus);
            SqlParameter par5 = new SqlParameter("@Ohjastaja", tieto.Ohjastaja);
            SqlParameter par6 = new SqlParameter("@Palkinto", tieto.Palkinto);
            SqlParameter par7 = new SqlParameter("@Paikka", tieto.Paikka);
            SqlParameter par8 = new SqlParameter("@Palkkio", tieto.Palkkio);
            SqlParameter par9 = new SqlParameter("@Kustannus", tieto.Kustannus);
            SqlParameter par10 = new SqlParameter("@Selvennys", tieto.Selvennys);
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
            String commandText;
            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4);
            
            commandText = "dbo.DeleteKilpailuRivi";

            SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
            SqlParameter par2 = new SqlParameter("@Tunnus", Convert.ToInt32(osat[2]));
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