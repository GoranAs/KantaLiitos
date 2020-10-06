using KantaLiitos.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using Newtonsoft.Json;

namespace KantaLiitos.Controllers
{
    public class AstutusController : ApiController
    {
        // GET api/astutus
        [HttpGet]
        public string Get()
        {
            return "Astutus tehty";
        }

        // GET api/astutus/5
        [HttpGet]
        public Astutus[] Get(string id)
        {
            Astutus[] res = new Astutus[503];

            String commandText = "dbo.SelectAstutus";
            int k = 1;

            SqlParameter parameterCus = new SqlParameter("@Cus", SqlDbType.VarChar)
            {
                Value = id
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
                        Astutus tieto = new Astutus();
                        tieto.Id = (int)reader["Id"];
                        tieto.Cus = (string)reader["Cus"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.HevosId = (int)reader["HevosId"];
                        tieto.Ori = (string)reader["Ori"];
                        tieto.OriTunnus = (string)reader["OriTunnus"];
                        tieto.Tamma = (string)reader["Tamma"];
                        tieto.TammaTunnus = (string)reader["TammaTunnus"];
                        tieto.Matka = (double)reader["Matka"];
                        tieto.Paikka = (string)reader["Paikka"];
                        tieto.Kustannus = (double)reader["Kustannus"];
                        tieto.Selvennys = (string)reader["Selvennys"];
                        res[k++] = tieto;
                        if (k == 500)
                            break;
                    }
                    Astutus pituus = new Astutus();
                    pituus.Id = k - 1;
                    pituus.Cus = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.OriTunnus = "";
                    pituus.Ori = "";
                    pituus.TammaTunnus = "";
                    pituus.Tamma = "";
                    pituus.HevosId = 0;
                    pituus.Matka = 0;
                    pituus.Kustannus = 0;
                    pituus.Paikka = "";
                    pituus.Selvennys = "";
                    res[0] = pituus;
                }
                else
                {
                    Astutus tieto = new Astutus();
                    tieto.Id = 0;
                    res[0] = tieto;
                }
                Array.Resize<Astutus>(ref res, k + 3);
                return res;
            }
        }

        // POST api/astutus
        [HttpPost]
        public string Post([FromBody] Astutus tieto)
        {
            string aa = "";
            String commandText = "";
            SqlParameter[] pars;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertAstutus";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
                SqlParameter par4 = new SqlParameter("@OriTunnus", tieto.OriTunnus);
                SqlParameter par5 = new SqlParameter("@Ori", tieto.Ori);
                SqlParameter par6 = new SqlParameter("@TammaTunnus", tieto.TammaTunnus);
                SqlParameter par7 = new SqlParameter("@Tamma", tieto.Tamma);
                SqlParameter par8 = new SqlParameter("@Matka", tieto.Matka);
                SqlParameter par9 = new SqlParameter("@Paikka", tieto.Paikka);
                SqlParameter par10 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par11 = new SqlParameter("@Selvennys", tieto.Selvennys);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6, par7, par8, par9, par10, par11
                };
                aa = "Lisätty ";
            }
            else
            {
                commandText = "dbo.UpdateAstutus";

                SqlParameter par1 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par2 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par3 = new SqlParameter("@OriTunnus", tieto.OriTunnus);
                SqlParameter par4 = new SqlParameter("@Ori", tieto.Ori);
                SqlParameter par5 = new SqlParameter("@TammaTunnus", tieto.TammaTunnus);
                SqlParameter par6 = new SqlParameter("@Tamma", tieto.Tamma);
                SqlParameter par7 = new SqlParameter("@Matka", tieto.Matka);
                SqlParameter par8 = new SqlParameter("@Paikka", tieto.Paikka);
                SqlParameter par9 = new SqlParameter("@Kustannus", tieto.Kustannus);
                SqlParameter par10 = new SqlParameter("@Selvennys", tieto.Selvennys);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6, par7, par8, par9, par10
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

        // PUT api/astutus
        [HttpPut]
        public string Put(string id, [FromBody] Astutus tieto)
        {
            String commandText;
            commandText = "dbo.UpdateAstutus";

            SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
            SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
            SqlParameter par3 = new SqlParameter("@HevosId", tieto.HevosId);
            SqlParameter par4 = new SqlParameter("@OriTunnus", tieto.OriTunnus);
            SqlParameter par5 = new SqlParameter("@Ori", tieto.Ori);
            SqlParameter par6 = new SqlParameter("@TammaTunnus", tieto.TammaTunnus);
            SqlParameter par7 = new SqlParameter("@Tamma", tieto.Tamma);
            SqlParameter par8 = new SqlParameter("@Matka", tieto.Matka);
            SqlParameter par9 = new SqlParameter("@Paikka", tieto.Paikka);
            SqlParameter par10 = new SqlParameter("@Kustannus", tieto.Kustannus);
            SqlParameter par11 = new SqlParameter("@Selvennys", tieto.Selvennys);
            SqlParameter[] pars = new SqlParameter[]
            {
                par1, par2, par3, par4, par5, par6, par7, par8, par9, par10, par11
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

        // DELETE api/astutus/5
        [HttpDelete]
        public string Delete(string id)
        {
            String commandText;
            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4, StringSplitOptions.RemoveEmptyEntries);

            if (osat[0] == "1")
                commandText = "dbo.Delete1Astutus";
            else
                commandText = "dbo.DeleteAstutus";
            if (osat[2].IndexOf('#') != -1)
                osat[2].Replace('#', '-');

            SqlParameter par2 = new SqlParameter("@Cus", osat[1]);
            SqlParameter par4 = new SqlParameter("@Tunnus", osat[2]);
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
                        return "Poistettu " + /*stat.ToString()*/ osat[1] + " " + osat[2] + " rivi";
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