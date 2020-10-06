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
    public class HuoltoController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Huolto tehty";
        }

        // GET api/<controller>/5
        [HttpGet]
        public Huolto[] Get(string id)
        {
            Huolto[] res = new Huolto[503];
            String commandText = "";
            SqlParameter[] pars = new SqlParameter[6];
            int k = 1;
            string[] osat = new string[7];
            string[] osat1 = new string[7];

            osat = id.Split(new char[] { '_' }, 7, StringSplitOptions.None);
            if (osat[0] == "0")
            {
                commandText = "dbo.SelectKaikkiHuolto";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                pars = new SqlParameter[]
                {
                    par1
                };
            }
            else if (osat[0] == "1")
            {
                commandText = "dbo.SelectHuolto";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@HevosId", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                    par1, par2
                };
            }
            else if (osat[0] == "2")
            {
                commandText = "dbo.SelectAikaHuolto";
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
                commandText = "dbo.SelectLastHuolto";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@HevosId", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                    par1, par2
                };
            }
            else if (osat[0] == "4")
            {
                commandText = "dbo.SelectIdHuolto";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@HarjoitusId", Convert.ToInt32(osat[2]));
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
                        Huolto tieto = new Huolto();
                        tieto.Id = (int)reader["Id"];
                        tieto.Cus = (string)reader["Cus"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.HevosId = (int)reader["HevosId"];
                        tieto.Toiminto = (string)reader["Toiminto"];
                        tieto.Matka = (double)reader["Matka"];
                        tieto.Kustannus = (double)reader["Kustannus"];
                        tieto.Paikka = (string)reader["Paikka"];
                        tieto.Selvennys = (string)reader["Selvennys"];
                        tieto.HarjoitusId = (int)reader["HarjoitusId"];
                        res[k++] = tieto;
                        if (k == 500)
                            break;
                    }
                    Huolto pituus = new Huolto();
                    pituus.Id = k - 1;
                    pituus.Cus = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.HevosId = 0;
                    pituus.Toiminto = "";
                    pituus.Matka = 0;
                    pituus.Kustannus = 0;
                    pituus.Paikka = "";
                    pituus.Selvennys = "";
                    pituus.HarjoitusId = 0;
                    res[0] = pituus;
                }
                else
                {
                    Huolto tieto = new Huolto();
                    tieto.Id = 0;
                    res[0] = tieto;
                }
                Array.Resize<Huolto>(ref res, k);
                return res;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] Huolto tieto)
        {
            string aa = "";
            String commandText = "";
            SqlParameter[] pars;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertHuolto";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
                SqlParameter par4 = new SqlParameter("@Toiminto", tieto.Toiminto);
                SqlParameter par5 = new SqlParameter("@Matka", tieto.Matka);
                SqlParameter par6 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par7 = new SqlParameter("@Paikka", tieto.Paikka);
                SqlParameter par8 = new SqlParameter("@Selvennys", tieto.Selvennys);
                SqlParameter par9 = new SqlParameter("@HarjoitusId", tieto.HarjoitusId);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6, par7, par8, par9
                };
                aa = "Lisätty ";
            }
            else
            {
                commandText = "dbo.UpdateHuolto";

                SqlParameter par1 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par2 = new SqlParameter("@Toiminto", tieto.Toiminto);
                SqlParameter par3 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par4 = new SqlParameter("@Paikka", tieto.Paikka);
                SqlParameter par5 = new SqlParameter("@Selvennys", tieto.Selvennys);
                SqlParameter par6 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par7 = new SqlParameter("@HarjoitusId", tieto.HarjoitusId);
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
        [HttpPut]
        public string Put(string id, [FromBody] Huolto tieto)
        {
            String commandText;
            commandText = "dbo.UpdateHuolto";

            SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
            SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
            SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
            SqlParameter par4 = new SqlParameter("@Toiminto", tieto.Toiminto);
            SqlParameter par5 = new SqlParameter("@Matka", tieto.Matka);
            SqlParameter par6 = new SqlParameter("@Kustannus", tieto.Kustannus);
            SqlParameter par7 = new SqlParameter("@Paikka", tieto.Paikka);
            SqlParameter par8 = new SqlParameter("@Selvennys", tieto.Selvennys);
            SqlParameter par9 = new SqlParameter("@HarjoitusId", tieto.HarjoitusId);
            SqlParameter[] pars = new SqlParameter[]
            {
                par1, par2, par3, par4, par5, par6, par7, par8, par9
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
            String commandText = "";
            SqlParameter[] pars = new SqlParameter[4];
            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4);
            if (osat[0] == "1")
            {
                commandText = "dbo.DeleteHuoltoRivi";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@Tunnus", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                    par1, par2
                };
            }
            else if (osat[0] == "2")
            {
                commandText = "dbo.UpdateHarjHuolto";
                SqlParameter par1 = new SqlParameter("@Huolid", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                    par1
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
                        string bb = "";
                        if (osat[0] == "1")
                        {
                            bb = "Poistettu " + stat.ToString() + " rivi";
                        }
                        else if (osat[0] == "2")
                        {
                            bb = "Huolto - Harjoitus linkitetty";
                        }
                        return bb;
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