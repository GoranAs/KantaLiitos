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
    public class TerveysController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Terveys ok";
        }

        // GET api/<controller>/5
        [HttpGet]
        public Terveys[] Get(string id)
        {
            Terveys[] res = new Terveys[503];
            int hepo = 0;
            String commandText = "";
            SqlParameter[] pars = new SqlParameter[6];
            int k = 1;
            string[] osat = new string[6];
            string[] osat1 = new string[6];

            osat1 = id.Split(new char[] { '_' }, 2, StringSplitOptions.None);
            if (osat1[0] == "0")
            {
                commandText = "dbo.SelectKaikkiTerveys";
                SqlParameter par1 = new SqlParameter("@Cus", osat1[1]);
                pars = new SqlParameter[]
                {
                    par1
                };
            }
            else if (osat1[0] == "1")
            {
                osat = osat1[1].Split(new char[] { '_' }, 2, StringSplitOptions.None);
                hepo = Convert.ToInt32(osat[1]);
                commandText = "dbo.SelectTerveys";
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
                commandText = "dbo.SelectAikaTerveys";
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
                commandText = "dbo.SelectLastTerveys";
                SqlParameter par1 = new SqlParameter("@Cus", osat[0]);
                SqlParameter par2 = new SqlParameter("@HevosId", hepo);
                pars = new SqlParameter[]
                {
                    par1, par2
                };
            }
            else
            {
                Terveys tr = new Terveys();
                tr.Id = 0;
                tr.Cus = "";
                tr.Aika = DateTime.Now;
                tr.HevosId = 0;
                tr.Kustannus = 0;
                tr.Laakari = "";
                tr.Sairaala = "";
                tr.Selvennys = "";
                tr.Tiedosto = "";
                tr.Toiminto = "";
                tr.Kuume = 0;
                res[0] = tr;
                return res;
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
                        Terveys tieto = new Terveys();
                        tieto.Id = (int)reader["Id"];
                        tieto.Cus = (string)reader["Cus"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.HevosId = (int)reader["HevosId"];
                        tieto.Toiminto = (string)reader["Toiminto"];
                        tieto.Laakari = (string)reader["Laakari"];
                        tieto.Sairaala = (string)reader["Sairaala"];
                        tieto.Kustannus = (double)reader["Kustannus"];
                        tieto.Selvennys = (string)reader["Selvennys"];
                        tieto.Tiedosto = (string)reader["Tiedosto"];
                        tieto.Kuume = (double)reader["Kuume"];
                        res[k++] = tieto;
                        if (k == 500)
                            break;
                    }
                    Terveys pituus = new Terveys();
                    pituus.Id = k - 1;
                    pituus.Cus = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.HevosId = 0;
                    pituus.Toiminto = "";
                    pituus.Laakari = "";
                    pituus.Sairaala = "";
                    pituus.Kustannus = 0;
                    pituus.Selvennys = "";
                    pituus.Tiedosto = "";
                    pituus.Kuume = 0;
                    res[0] = pituus;
                }
                else
                {
                    Terveys tieto = new Terveys();
                    tieto.Id = 0;
                    res[0] = tieto;
                }
                Array.Resize<Terveys>(ref res, k + 3);
                return res;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] Terveys tieto)
        {
            string aa = "";
            SqlParameter[] pars;
            String commandText;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertTerveys";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
                SqlParameter par4 = new SqlParameter("@Toiminto", tieto.Toiminto);
                SqlParameter par5 = new SqlParameter("@Laakari", tieto.Laakari);
                SqlParameter par6 = new SqlParameter("@Sairaala", tieto.Sairaala);
                SqlParameter par7 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par8 = new SqlParameter("@Selvennys", tieto.Selvennys);
                SqlParameter par9 = new SqlParameter("@Tiedosto", tieto.Tiedosto);
                SqlParameter par10 = new SqlParameter("@Kuume", tieto.Kuume);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6, par7, par8, par9, par10
                };
                aa = "Lisätty ";
            }
            else
            {
                commandText = "dbo.UpdateTerveys";

                SqlParameter par1 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par2 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par3 = new SqlParameter("@Toiminto", tieto.Toiminto);
                SqlParameter par4 = new SqlParameter("@Laakari", tieto.Laakari);
                SqlParameter par5 = new SqlParameter("@Sairaala", tieto.Sairaala);
                SqlParameter par6 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par7 = new SqlParameter("@Selvennys", tieto.Selvennys);
                SqlParameter par8 = new SqlParameter("@Tiedosto", tieto.Tiedosto);
                SqlParameter par9 = new SqlParameter("@Kuume", tieto.Kuume);
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
        public string Put(string id, [FromBody] Terveys tieto)
        {
            String commandText;
            commandText = "dbo.UpdateTerveys";

            SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
            SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
            SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
            SqlParameter par4 = new SqlParameter("@Toiminto", tieto.Toiminto);
            SqlParameter par5 = new SqlParameter("@Laakari", tieto.Laakari);
            SqlParameter par6 = new SqlParameter("@Sairaala", tieto.Sairaala);
            SqlParameter par7 = new SqlParameter("@Kustannus", tieto.Kustannus);
            SqlParameter par8 = new SqlParameter("@Selvennys", tieto.Selvennys);
            SqlParameter par9 = new SqlParameter("@Tiedosto", tieto.Tiedosto);
            SqlParameter par10 = new SqlParameter("@Kuume", tieto.Kuume);
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

            commandText = "dbo.DeleteTerveysRivi";

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