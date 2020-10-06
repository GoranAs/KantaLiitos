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
    public class OvningController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            return "Ovning Ok";
        }

        // GET api/<controller>/5
        [HttpGet]
        public Ovning[] Get(string id)
        {
            Ovning[] res = new Ovning[503];
            String commandText = "";
            SqlParameter[] pars = new SqlParameter[6];
            int k = 1;
            string[] osat = new string[7];

            osat = id.Split(new char[] { '_' }, 6, StringSplitOptions.None);
            if (osat[0] == "0")
            {
                commandText = "dbo.SelectKaikkiOvning";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                pars = new SqlParameter[]
                {
                        par1
                };
            }
            else if (osat[0] == "1")
            {
                commandText = "dbo.SelectOvning";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@HevosId", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                        par1, par2
                };
            }
            else if (osat[0] == "2")
            {
                commandText = "dbo.SelectAikaOvning";
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
                commandText = "dbo.SelectLastOvning";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@HevosId", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                        par1, par2
                };
            }
            else if (osat[0] == "4")
            {
                commandText = "dbo.SelectIdOvning";
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
                        Ovning tieto = new Ovning();
                        tieto.Id = (int)reader["Id"];
                        tieto.Cus = (string)reader["Cus"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.HevosId = (int)reader["HevosId"];
                        tieto.Toiminto = (string)reader["Toiminto"];
                        tieto.Paikka = (string)reader["Paikka"];
                        tieto.Kustannus = (double)reader["Kustannus"];
                        tieto.Fiilis = (int)reader["Fiilis"];
                        tieto.Selvennys = (string)reader["Selvennys"];
                        tieto.Kesto = (double)reader["Kesto"];
                        tieto.Matka = (double)reader["Matka"];
                        tieto.Nopeus = (double)reader["Nopeus"];
                        tieto.HuoltoId = (int)reader["HuoltoId"];
                        tieto.Har1 = (string)reader["Har1"];
                        tieto.Har2 = (string)reader["Har2"];
                        tieto.Har3 = (string)reader["Har3"];
                        tieto.Har4 = (string)reader["Har4"];
                        tieto.Har5 = (string)reader["Har5"];
                        tieto.Har6 = (string)reader["Har6"];
                        tieto.Har7 = (string)reader["Har7"];
                        tieto.Har8 = (string)reader["Har8"];
                        tieto.Har9 = (string)reader["Har9"];
                        res[k++] = tieto;
                        if (k == 500)
                            break;
                    }
                    Ovning pituus = new Ovning();
                    pituus.Id = k - 1;
                    pituus.Cus = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.HevosId = 0;
                    pituus.Toiminto = "";
                    pituus.Paikka = "";
                    pituus.Kustannus = 0.0;
                    pituus.Fiilis = 0;
                    pituus.Selvennys = "";
                    pituus.Kesto = 0.0;
                    pituus.Matka = 0.0;
                    pituus.Nopeus = 0.0;
                    pituus.HuoltoId = 0;
                    pituus.Har1 = "";
                    pituus.Har2 = "";
                    pituus.Har3 = "";
                    pituus.Har4 = "";
                    pituus.Har5 = "";
                    pituus.Har6 = "";
                    pituus.Har7 = "";
                    pituus.Har8 = "";
                    pituus.Har9 = "";
                    res[0] = pituus;
                }
                else
                {
                    Ovning tieto = new Ovning();
                    tieto.Id = 0;
                    res[0] = tieto;
                }
                Array.Resize<Ovning>(ref res, k);
                return res;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] Ovning tieto)
        {
            string aa = "";
            String commandText = "";
            SqlParameter[] pars;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertOvning";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
                SqlParameter par4 = new SqlParameter("@Toiminto", tieto.Toiminto);
                SqlParameter par5 = new SqlParameter("@Paikka", tieto.Paikka);
                SqlParameter par6 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par7 = new SqlParameter("@Fiilis", tieto.Fiilis);
                SqlParameter par8 = new SqlParameter("@Selvennys", tieto.Selvennys);
                SqlParameter par9 = new SqlParameter("@Kesto", tieto.Kesto);
                SqlParameter par10 = new SqlParameter("@Matka", tieto.Matka);
                SqlParameter par11 = new SqlParameter("@Nopeus", tieto.Nopeus);
                SqlParameter par12 = new SqlParameter("@HuoltoId", tieto.HuoltoId);
                SqlParameter par13 = new SqlParameter("@Har1", tieto.Har1);
                SqlParameter par14 = new SqlParameter("@Har2", tieto.Har2);
                SqlParameter par15 = new SqlParameter("@Har3", tieto.Har3);
                SqlParameter par16 = new SqlParameter("@Har4", tieto.Har4);
                SqlParameter par17 = new SqlParameter("@Har5", tieto.Har5);
                SqlParameter par18 = new SqlParameter("@Har6", tieto.Har6);
                SqlParameter par19 = new SqlParameter("@Har7", tieto.Har7);
                SqlParameter par20 = new SqlParameter("@Har8", tieto.Har8);
                SqlParameter par21 = new SqlParameter("@Har9", tieto.Har9);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6, par7, par8, par9, par10, par11,
                    par12, par13, par14, par15, par16, par17, par18, par19, par20, par21
                };
                aa = "OK-Lisätty ";
            }
            else
            {
                commandText = "dbo.UpdateOvning";

                SqlParameter par1 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@Toiminto", tieto.Toiminto);
                SqlParameter par4 = new SqlParameter("@Paikka", tieto.Paikka);
                SqlParameter par5 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par6 = new SqlParameter("@Fiilis", tieto.Fiilis);
                SqlParameter par7 = new SqlParameter("@Selvennys", tieto.Selvennys);
                SqlParameter par8 = new SqlParameter("@Kesto", tieto.Kesto);
                SqlParameter par9 = new SqlParameter("@Matka", tieto.Matka);
                SqlParameter par10 = new SqlParameter("@Nopeus", tieto.Nopeus);
                SqlParameter par11 = new SqlParameter("@HuoltoId", tieto.HuoltoId);
                SqlParameter par12 = new SqlParameter("@Har1", tieto.Har1);
                SqlParameter par13 = new SqlParameter("@Har2", tieto.Har2);
                SqlParameter par14 = new SqlParameter("@Har3", tieto.Har3);
                SqlParameter par15 = new SqlParameter("@Har4", tieto.Har4);
                SqlParameter par16 = new SqlParameter("@Har5", tieto.Har5);
                SqlParameter par17 = new SqlParameter("@Har6", tieto.Har6);
                SqlParameter par18 = new SqlParameter("@Har7", tieto.Har7);
                SqlParameter par19 = new SqlParameter("@Har8", tieto.Har8);
                SqlParameter par20 = new SqlParameter("@Har9", tieto.Har9);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6, par7, par8, par9, par10,
                    par11, par12, par13, par14, par15, par16, par17, par18, par19, par20
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
        public void Put(int id, [FromBody] string value)
        {
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
                commandText = "dbo.DeleteOvningRivi";

                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@Tunnus", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                    par1, par2
                };
            }
            if (osat[0] == "2")
            {
                commandText = "dbo.LinkOvningHuolto";

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