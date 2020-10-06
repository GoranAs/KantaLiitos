using KantaLiitos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KantaLiitos.Controllers
{
    public class VarsaController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Varsa ok";
        }

        // GET api/<controller>/5
        [HttpGet]
        public Varsa[] Get(string id)
        {
            Varsa[] res = new Varsa[503];
            int hepo = 0;
            String commandText = "";
            SqlParameter[] pars = new SqlParameter[6];
            int k = 1;
            string[] osat = new string[6];
            string[] osat1 = new string[6];

            osat1 = id.Split(new char[] { '_' }, 2, StringSplitOptions.None);
            if (osat1[0] == "1")
            {
                osat = osat1[1].Split(new char[] { '_' }, 2, StringSplitOptions.None);
                hepo = Convert.ToInt32(osat[1]);
                commandText = "dbo.SelectVarsa";
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
                commandText = "dbo.SelectLastVarsa";
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
                        Varsa tieto = new Varsa();
                        tieto.Id = (int)reader["Id"];
                        tieto.Cus = (string)reader["Cus"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.HevosId = (int)reader["HevosId"];
                        tieto.Toiminto = (string)reader["Toiminto"];
                        tieto.Matka = (double)reader["Matka"];
                        tieto.Kustannus = (double)reader["Kustannus"];
                        tieto.Selvennys = (string)reader["Selvennys"];
                        res[k++] = tieto;
                        if (k == 500)
                            break;
                    }
                    Varsa pituus = new Varsa();
                    pituus.Id = k - 1;
                    pituus.Cus = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.HevosId = 0;
                    pituus.Toiminto = "";
                    pituus.Matka = 0;
                    pituus.Kustannus = 0;
                    pituus.Selvennys = "";
                    res[0] = pituus;
                }
                else
                {
                    Varsa tieto = new Varsa();
                    tieto.Id = 0;
                    res[0] = tieto;
                }
                Array.Resize<Varsa>(ref res, k + 3);
                return res;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] Varsa tieto)
        {
            string aa = "";
            string bb = "";
            SqlParameter[] pars;
            String commandText;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertVarsa";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
                SqlParameter par4 = new SqlParameter("@Toiminto", tieto.Toiminto);
                SqlParameter par5 = new SqlParameter("@Matka", tieto.Matka);
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
                commandText = "dbo.UpdateVarsa";

                SqlParameter par1 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par2 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par3 = new SqlParameter("@Toiminto", tieto.Toiminto);
                SqlParameter par4 = new SqlParameter("@Matka", tieto.Matka);
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
                        if (tieto.Cus == "252525252")
                        {
                            bb = LisaaLokiin(1, tieto, aa);
                        }
                        return aa + stat.ToString() + " rivi(ä) - " + bb;
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
        public string Put(string id, [FromBody] Varsa tieto)
        {
            String commandText;
            commandText = "dbo.UpdateVarsa";

            SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
            SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
            SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
            SqlParameter par4 = new SqlParameter("@Toiminto", tieto.Toiminto);
            SqlParameter par5 = new SqlParameter("@Matka", tieto.Matka);
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
            string aa = "Poistettu";
            string bb = "";
            String commandText;
            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4);

            commandText = "dbo.DeleteVarsaRivi";

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
                        if (osat[1] == "252525252")
                        {
                            Varsa tieto = new Varsa();
                            tieto.Cus = osat[1];
                            tieto.Id = Convert.ToInt32(osat[2]);
                            bb = LisaaLokiin(2, tieto, aa);
                        }
                        return aa + stat.ToString() + " rivi(ä) - " + bb;
                    }
                    catch (Exception exception)
                    {
                        return exception.Message;
                    }
                }
            }
        }

        public string LisaaLokiin(int typ, Varsa tieto, string suunta)
        {
            string res = "";
            string[] lg = new string[]
                {
                };
            if (typ == 1)
            {
                lg = new string[]
                {
                    suunta.Substring(0, 4) + ">>" + "Vars" + ">>" + tieto.Id.ToString() + ">>" +
                    tieto.Aika.ToString("MM.dd.yy HH:mm:ss") + ">>" + tieto.Cus + ">>" + tieto.HevosId.ToString() +">>" +
                    tieto.Toiminto + ">>" + tieto.Matka.ToString("F0") + "m>>" + tieto.Kustannus.ToString("F2") + "€>>" +
                    tieto.Selvennys
                };
            }
            else if (typ == 2)
            {
                lg = new string[]
                {
                    suunta.Substring(0, 4) + ">>" + "Vars" + ">>" + tieto.Id.ToString() + ">>" +
                    DateTime.Now.ToString("MM.dd.yy HH:mm:ss") + ">>" + tieto.Cus
                };
            }
            try
            {
                File.AppendAllLines(@"C:\Aapilot\Loki.txt", lg);
                res = "";
            }
            catch (Exception exc)
            {
                res = exc.Message;
            }
            return res;
        }
    }
}