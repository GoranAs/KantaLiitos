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
    public class Harjoitus1Controller : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            return "Harjoitus on ohi";
        }

        // GET api/<controller>/5
        [HttpGet]
        public Harjoitus1[] Get(string id)
        {
            Harjoitus1[] res = new Harjoitus1[503];
            String commandText = "";
            SqlParameter[] pars = new SqlParameter[6];
            int k = 1;
            string[] osat = new string[7];

            osat = id.Split(new char[] { '_' }, 6, StringSplitOptions.None);
            if (osat[0] == "0")
            {
                commandText = "dbo.SelectKaikkiHarjoitus1";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                pars = new SqlParameter[]
                {
                        par1
                };
            }
            else if (osat[0] == "1")
            {
                commandText = "dbo.SelectHarjoitus1";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@HevosId", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                        par1, par2
                };
            }
            else if (osat[0] == "2")
            {
                commandText = "dbo.SelectAikaHarjoitus1";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@HevosId", Convert.ToInt32(osat[2]));
                SqlParameter par3 = new SqlParameter("@Alku", osat[3]);
                SqlParameter par4 = new SqlParameter("@Loppu", osat[4]);
                pars = new SqlParameter[]
                {
                        par1, par2, par3, par4
                };
            }
            else if (osat[0] == "3")
            {
                commandText = "dbo.SelectLastHarjoitus1";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@HevosId", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                        par1, par2
                };
            }
            else if (osat[0] == "4")
            {
                commandText = "dbo.SelectIdHarjoitus1";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@HuoltoId", Convert.ToInt32(osat[2]));
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
                        Harjoitus1 tieto = new Harjoitus1();
                        tieto.Id = (int)reader["Id"];
                        tieto.Cus = (string)reader["Cus"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.HevosId = (int)reader["HevosId"];
                        tieto.Toiminto = (string)reader["Toiminto"];
                        tieto.Toistot = (int)reader["Toistot"];
                        tieto.Tehonopeus = (double)reader["Tehonopeus"];
                        tieto.Tehoaika = (double)reader["Tehoaika"];
                        tieto.Matka = (double)reader["Matka"];
                        tieto.Toistovali = (double)reader["Toistovali"];
                        tieto.Paikka = (string)reader["Paikka"];
                        tieto.Kustannus = (double)reader["Kustannus"];
                        tieto.Fiilis = (int)reader["Fiilis"];
                        tieto.Selvennys = (string)reader["Selvennys"];
                        tieto.Kesto = (double)reader["Kesto"];
                        tieto.HuoltoId = (int)reader["HuoltoId"];
                        tieto.Kuvaus = (string)reader["Kuvaus"];
                        res[k++] = tieto;
                        if (k == 500)
                            break;
                    }
                    Harjoitus1 pituus = new Harjoitus1();
                    pituus.Id = k - 1;
                    pituus.Cus = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.HevosId = 0;
                    pituus.Toiminto = "";
                    pituus.Toistot = 0;
                    pituus.Tehonopeus = 0.0;
                    pituus.Tehoaika = 0.0;
                    pituus.Matka = 0.0;
                    pituus.Toistovali = 0.0;
                    pituus.Paikka = "";
                    pituus.Kustannus = 0.0;
                    pituus.Fiilis = 0;
                    pituus.Selvennys = "";
                    pituus.Kesto = 0.0;
                    pituus.HuoltoId = 0;
                    pituus.Kuvaus = "";
                    res[0] = pituus;
                }
                else
                {
                    Harjoitus1 tieto = new Harjoitus1();
                    tieto.Id = 0;
                    res[0] = tieto;
                }
                Array.Resize<Harjoitus1>(ref res, k);
                return res;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] Harjoitus1 tieto)
        {
            string aa = "";
            String commandText = "";
            SqlParameter[] pars;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertHarjoitus1";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
                SqlParameter par4 = new SqlParameter("@Toiminto", tieto.Toiminto);
                SqlParameter par5 = new SqlParameter("@Toistot", tieto.Toistot);
                SqlParameter par6 = new SqlParameter("@Tehonopeus", tieto.Tehonopeus);
                SqlParameter par7 = new SqlParameter("@Tehoaika", tieto.Tehoaika);
                SqlParameter par8 = new SqlParameter("@Matka", tieto.Matka);
                SqlParameter par9 = new SqlParameter("@Toistovali", tieto.Toistovali);
                SqlParameter par10 = new SqlParameter("@Paikka", tieto.Paikka);
                SqlParameter par11 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par12 = new SqlParameter("@Fiilis", tieto.Fiilis);
                SqlParameter par13 = new SqlParameter("@Selvennys", tieto.Selvennys);
                SqlParameter par14 = new SqlParameter("@Kesto", tieto.Kesto);
                SqlParameter par15 = new SqlParameter("@HuoltoId", tieto.HuoltoId);
                SqlParameter par16 = new SqlParameter("@Kuvaus", tieto.Kuvaus);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6, par7, par8, par9, par10, par11, 
                    par12, par13, par14, par15, par16
                };
                aa = "OK-Lisätty ";
            }
            else
            {
                commandText = "dbo.UpdateHarjoitus1";

                SqlParameter par1 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@Toiminto", tieto.Toiminto);
                SqlParameter par4 = new SqlParameter("@Toistot", tieto.Toistot);
                SqlParameter par5 = new SqlParameter("@Tehonopeus", tieto.Tehonopeus);
                SqlParameter par6 = new SqlParameter("@Tehoaika", tieto.Tehoaika);
                SqlParameter par7 = new SqlParameter("@Matka", tieto.Matka);
                SqlParameter par8 = new SqlParameter("@Toistovali", tieto.Toistovali);
                SqlParameter par9 = new SqlParameter("@Paikka", tieto.Paikka);
                SqlParameter par10 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par11 = new SqlParameter("@Fiilis", tieto.Fiilis);
                SqlParameter par12 = new SqlParameter("@Selvennys", tieto.Selvennys);
                SqlParameter par13 = new SqlParameter("@Kesto", tieto.Kesto);
                SqlParameter par14 = new SqlParameter("@HuoltoId", tieto.HuoltoId);
                SqlParameter par15 = new SqlParameter("@Kuvaus", tieto.Kuvaus);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6, par7, par8, par9, par10, 
                    par11, par12, par13, par14, par15
                };
                aa = "OK-Muutettu ";
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
        public string Put(string id, [FromBody] Harjoitus1 tieto)
        {
            String commandText;
            commandText = "dbo.UpdateHarjoitus1";

            SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
            SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
            SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
            SqlParameter par4 = new SqlParameter("@Toiminto", tieto.Toiminto);
            SqlParameter par5 = new SqlParameter("@Toistot", tieto.Toistot);
            SqlParameter par6 = new SqlParameter("@Tehonopeus", tieto.Tehonopeus);
            SqlParameter par7 = new SqlParameter("@Tehoaika", tieto.Tehoaika);
            SqlParameter par8 = new SqlParameter("@Matka", tieto.Matka);
            SqlParameter par9 = new SqlParameter("@Toistovali", tieto.Toistovali);
            SqlParameter par10 = new SqlParameter("@Paikka", tieto.Paikka);
            SqlParameter par11 = new SqlParameter("@Kustannus", tieto.Kustannus);
            SqlParameter par12 = new SqlParameter("@Fiilis", tieto.Fiilis);
            SqlParameter par13 = new SqlParameter("@Selvennys", tieto.Selvennys);
            SqlParameter par14 = new SqlParameter("@Kesto", tieto.Kesto);
            SqlParameter par15 = new SqlParameter("@HuoltoId", tieto.HuoltoId);
            SqlParameter par16 = new SqlParameter("@Kuvaus", tieto.Kuvaus);
            SqlParameter[] pars = new SqlParameter[]
            {
                par1, par2, par3, par4, par5, par6, par7, par8, par9, par10, 
                par11, par12, par13, par14, par15, par16
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
            string res = "";
            String commandText = "";
            SqlParameter[] pars = new SqlParameter[4];
            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4);

            if (osat[0] == "1")
            {
                commandText = "dbo.DeleteHarjoitus1Rivi";

                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@Tunnus", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                    par1, par2
                };
            }
            if (osat[0] == "2")
            {
                commandText = "dbo.LinkHarjHuolto";

                SqlParameter par1 = new SqlParameter("@Linkid", Convert.ToInt32(osat[2]));
                SqlParameter par2 = new SqlParameter("@Id", Convert.ToInt32(osat[3]));
                pars = new SqlParameter[]
                {
                    par1, par2
                };
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
                        if (osat[0] == "1")
                        {
                            res = "OK-Poistettu ";
                        }
                        else if (osat[0] == "2")
                        {
                            res = "OK-Muutettu ";
                        }
                        return res + stat.ToString() + " rivi";
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
