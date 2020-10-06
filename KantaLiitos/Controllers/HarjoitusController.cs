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
    public class HarjoitusController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            return "Harjoitus on ohi";
        }

        // GET api/<controller>/5
        [HttpGet]
        public Harjoitus[] Get(string id)
        {
            Harjoitus[] res = new Harjoitus[503];
            int hepo = 0;
            String commandText = "";
            SqlParameter[] pars = new SqlParameter[6];
            int k = 1;
            string[] osat = new string[6];
            string[] osat1 = new string[6];

            osat1 = id.Split(new char[] { '_' }, 2, StringSplitOptions.None);
            if (osat1[0] == "0")
            {
                osat = osat1[1].Split(new char[] { '_' }, 2, StringSplitOptions.None);
                commandText = "dbo.SelectKaikkiHarjoitus";
                SqlParameter par1 = new SqlParameter("@Cus", osat[0]);
                pars = new SqlParameter[]
                {
                    par1
                };
            }
            if (osat1[0] == "1")
            {
                osat = osat1[1].Split(new char[] { '_' }, 2, StringSplitOptions.None);
                hepo = Convert.ToInt32(osat[1]);
                commandText = "dbo.SelectHarjoitus";
                SqlParameter par1 = new SqlParameter("@Cus", osat[0]);
                SqlParameter par2 = new SqlParameter("@HevosId", hepo);
                pars = new SqlParameter[]
                {
                    par1, par2
                };
            }
            else if (osat1[0] == "2")
            {
                osat = osat1[1].Split(new char[] { '_' }, 5, StringSplitOptions.None);
                hepo = Convert.ToInt32(osat[1]);
                commandText = "dbo.SelectAikaHarjoitus";
                SqlParameter par1 = new SqlParameter("@Cus", osat[0]);
                SqlParameter par2 = new SqlParameter("@HevosId", hepo);
                SqlParameter par3 = new SqlParameter("@Alku", osat[2]);
                SqlParameter par4 = new SqlParameter("@Loppu", osat[3]);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4
                };
            }
            else if (osat1[0] == "3")
            {
                osat = osat1[1].Split(new char[] { '_' }, 2, StringSplitOptions.None);
                hepo = Convert.ToInt32(osat[1]);
                commandText = "dbo.SelectLastHarjoitus";
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
                        Harjoitus tieto = new Harjoitus();
                        tieto.Id = (int)reader["Id"];
                        tieto.Cus = (string)reader["Cus"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.HevosId = (int)reader["HevosId"];
                        tieto.Toiminto = (string)reader["Toiminto"];
                        tieto.Sarjat = (int)reader["Sarjat"];
                        tieto.Toistot = (int)reader["Toistot"];
                        tieto.Teho = (int)reader["Teho"];
                        tieto.Matka = (int)reader["Matka"];
                        tieto.Toistovali = (int)reader["Toistovali"];
                        tieto.Sarjavali = (int)reader["Sarjavali"];
                        tieto.Paikka = (string)reader["Paikka"];
                        tieto.Kustannus = (double)reader["Kustannus"];
                        tieto.Selvennys = (string)reader["Selvennys"];
                        tieto.Kesto = (double)reader["Kesto"];
                        res[k++] = tieto;
                        if (k == 500)
                            break;
                    }
                    Harjoitus pituus = new Harjoitus();
                    pituus.Id = k - 1;
                    pituus.Cus = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.HevosId = 0;
                    pituus.Toiminto = "";
                    pituus.Sarjat = 0;
                    pituus.Toistot = 0;
                    pituus.Teho = 0;
                    pituus.Matka = 0;
                    pituus.Toistovali = 0;
                    pituus.Sarjavali = 0;
                    pituus.Paikka = "";
                    pituus.Kustannus = 0;
                    pituus.Selvennys = "";
                    pituus.Kesto = 0;
                    res[0] = pituus;
                }
                else
                {
                    Harjoitus tieto = new Harjoitus();
                    tieto.Id = 0;
                    res[0] = tieto;
                }
                Array.Resize<Harjoitus>(ref res, k + 3);
                return res;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] Harjoitus tieto)
        {
            string aa = "";
            String commandText = "";
            SqlParameter[] pars;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertHarjoitus";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
                SqlParameter par4 = new SqlParameter("@Toiminto", tieto.Toiminto);
                SqlParameter par5 = new SqlParameter("@Sarjat", tieto.Sarjat);
                SqlParameter par6 = new SqlParameter("@Toistot", tieto.Toistot);
                SqlParameter par7 = new SqlParameter("@Teho", tieto.Teho);
                SqlParameter par8 = new SqlParameter("@Matka", tieto.Matka);
                SqlParameter par9 = new SqlParameter("@Toistovali", tieto.Toistovali);
                SqlParameter par10 = new SqlParameter("@Sarjavali", tieto.Sarjavali);
                SqlParameter par11 = new SqlParameter("@Paikka", tieto.Paikka);
                SqlParameter par12 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par13 = new SqlParameter("@Selvennys", tieto.Selvennys);
                SqlParameter par14 = new SqlParameter("@Kesto", tieto.Kesto);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6, par7, par8, par9, par10, par11, par12, par13, par14
                };
                aa = "Lisätty ";
            }
            else
            {
                commandText = "dbo.UpdateHarjoitus";

                SqlParameter par1 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par2 = new SqlParameter("@Toiminto", tieto.Toiminto);
                SqlParameter par3 = new SqlParameter("@Sarjat", tieto.Sarjat);
                SqlParameter par4 = new SqlParameter("@Toistot", tieto.Toistot);
                SqlParameter par5 = new SqlParameter("@Teho", tieto.Teho);
                SqlParameter par6 = new SqlParameter("@Matka", tieto.Matka);
                SqlParameter par7 = new SqlParameter("@Toistovali", tieto.Toistovali);
                SqlParameter par8 = new SqlParameter("@Sarjavali", tieto.Sarjavali);
                SqlParameter par9 = new SqlParameter("@Paikka", tieto.Paikka);
                SqlParameter par10 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par11 = new SqlParameter("@Selvennys", tieto.Selvennys);
                SqlParameter par12 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par13 = new SqlParameter("@Kesto", tieto.Kesto);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6, par7, par8, par9, par10, par11, par12, par13
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
        public string Put(string id, [FromBody] Harjoitus tieto)
        {
            String commandText;
            commandText = "dbo.UpdateHarjoitus";

            SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
            SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
            SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
            SqlParameter par4 = new SqlParameter("@Toiminto", tieto.Toiminto);
            SqlParameter par5 = new SqlParameter("@Sarjat", tieto.Sarjat);
            SqlParameter par6 = new SqlParameter("@Toistot", tieto.Toistot);
            SqlParameter par7 = new SqlParameter("@Teho", tieto.Teho);
            SqlParameter par8 = new SqlParameter("@Matka", tieto.Matka);
            SqlParameter par9 = new SqlParameter("@Toistovali", tieto.Toistovali);
            SqlParameter par10 = new SqlParameter("@Sarjavali", tieto.Sarjavali);
            SqlParameter par11 = new SqlParameter("@Paikka", tieto.Paikka);
            SqlParameter par12 = new SqlParameter("@Kustannus", tieto.Kustannus);
            SqlParameter par13 = new SqlParameter("@Selvennys", tieto.Selvennys);
            SqlParameter[] pars = new SqlParameter[]
            {
                par1, par2, par3, par4, par5, par6, par7, par8, par9, par10, par11, par12, par13
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

            commandText = "dbo.DeleteHarjoitusRivi";

            SqlParameter par2 = new SqlParameter("@Cus", osat[1]);
            SqlParameter par4 = new SqlParameter("@Tunnus", Convert.ToInt32(osat[2]));
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