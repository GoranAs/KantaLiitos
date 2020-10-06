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
    public class TalousController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Talous ok";
        }

        // GET api/<controller>/5
        [HttpGet]
        public Talous[] Get(string id)
        {
            Talous[] res = new Talous[503];
            int hepo = 0;
            String commandText = "";
            SqlParameter[] pars = new SqlParameter[6];
            int k = 1;
            string[] osat = new string[6];
            string[] osat1 = new string[6];

            osat1 = id.Split(new char[] { '_' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (osat1[0] == "0")
            {
                osat = osat1[1].Split(new char[] { '_' }, 2, StringSplitOptions.None);
                commandText = "dbo.SelectKaikkiTalous";
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
                commandText = "dbo.SelectTalous";
                SqlParameter par1 = new SqlParameter("@Cus", osat[0]);
                SqlParameter par2 = new SqlParameter("@HevosId", hepo);
                pars = new SqlParameter[]
                {
                    par1, par2
                };
            }
            else if (osat1[0] == "2")
            {
                osat = osat1[1].Split(new char[] { '_' }, 2, StringSplitOptions.None);
                hepo = Convert.ToInt32(osat[1]);
                commandText = "dbo.SelectLastTalous";
                SqlParameter par1 = new SqlParameter("@Cus", osat[0]);
                SqlParameter par2 = new SqlParameter("@HevosId", hepo);
                pars = new SqlParameter[]
                {
                    par1, par2
                };
            }

            SqlConnection conn = new SqlConnection(DbCon.connectionString);
                // When the direction of parameter is set as Output, you can get the value after 
                // executing the command.

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.Parameters.AddRange(pars);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                // When using CommandBehavior.CloseConnection, the connection will be closed when the 
                // IDataReader is closed.
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Talous tieto = new Talous
                        {
                            Id = (int)reader["Id"],
                            Cus = (string)reader["Cus"],
                            Aika = (DateTime)reader["Aika"],
                            HevosId = (int)reader["HevosId"],
                            Kohde = (string)reader["Kohde"],
                            Liike = (string)reader["Liike"],
                            Kustannus = (double)reader["Kustannus"],
                            Selvennys = (string)reader["Selvennys"]
                        };
                        res[k++] = tieto;
                        if (k == 500)
                            break;
                    }
                    Talous pituus = new Talous
                    {
                        Id = k - 1,
                        Cus = "",
                        Aika = DateTime.MinValue,
                        HevosId = 0,
                        Kohde = "",
                        Liike = "",
                        Kustannus = 0,
                        Selvennys = "",
                    };
                    res[0] = pituus;
                }
                else
                {
                    Talous tieto = new Talous
                    {
                        Id = 0,
                        HevosId = 0
                    };
                    res[0] = tieto;
                }
                Array.Resize<Talous>(ref res, k + 3);
                return res;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] Talous tieto)
        {
            string aa = "";
            SqlParameter[] pars;
            String commandText;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertTalous";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
                SqlParameter par4 = new SqlParameter("@Kohde", tieto.Kohde);
                SqlParameter par5 = new SqlParameter("@Liike", tieto.Liike);
                SqlParameter par6 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par7 = new SqlParameter("@Selvennys", tieto.Selvennys);
                pars = new SqlParameter[]
                {
                par1, par2, par3, par4, par5, par6, par7
                };
                aa = "Lisätty ";
            }
            else
            {
                commandText = "dbo.UpdateTalous";

                SqlParameter par1 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par2 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par3 = new SqlParameter("@Kohde", tieto.Kohde);
                SqlParameter par4 = new SqlParameter("@Liike", tieto.Liike);
                SqlParameter par5 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par6 = new SqlParameter("@Selvennys", tieto.Selvennys);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6
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
        public string Put(string id, [FromBody] Talous tieto)
        {
            String commandText;
            commandText = "dbo.UpdateTalous";

            SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
            SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
            SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
            SqlParameter par4 = new SqlParameter("@Kohde", tieto.Kohde);
            SqlParameter par5 = new SqlParameter("@Liike", tieto.Liike);
            SqlParameter par6 = new SqlParameter("@Kustannus", tieto.Kustannus);
            SqlParameter par7 = new SqlParameter("@Selvennys", tieto.Selvennys);
            SqlParameter[] pars = new SqlParameter[]
            {
                par1, par2, par3, par4, par5, par6, par7
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

            commandText = "dbo.DeleteTalousRivi";

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