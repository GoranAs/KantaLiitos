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
    public class OvningStatController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Stat ok";
        }

        // GET api/<controller>/5
        [HttpGet]
        public OvningStat[] Get(string id)
        {
            OvningStat[] res = new OvningStat[23];
            String commandText = "";
            SqlParameter[] pars = new SqlParameter[6];
            int k = 1;
            string[] osat = new string[6];

            osat = id.Split(new char[] { '_' }, 5, StringSplitOptions.None);
            if (osat[0] == "1")
            {
                commandText = "dbo.SelectOvningStatLen";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@Hevosid", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                par1, par2
                };
            }
            else if (osat[0] == "2")
            {
                commandText = "dbo.SelectOvningStatNr";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@Hevosid", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                par1, par2
                };
            }
            else if (osat[0] == "3")
            {
                commandText = "dbo.SelectOvningStatLenAika";
                SqlParameter par1 = new SqlParameter("@Aika", osat[1]);
                SqlParameter par2 = new SqlParameter("@Cus", osat[2]);
                SqlParameter par3 = new SqlParameter("@Hevosid", Convert.ToInt32(osat[3]));
                pars = new SqlParameter[]
                {
                par1, par2, par3
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
                        OvningStat tieto = new OvningStat();
                        tieto.Toiminto = (string)reader["Toiminto"];
                        tieto.Nr = (int)reader["Nr"];
                        tieto.Len = (double)reader["Len"];
                        tieto.AvgLen = (double)reader["AvgLen"];
                        tieto.Tid = (double)reader["Tid"];
                        tieto.AvgNop = (double)reader["AvgNop"];
                        res[k++] = tieto;
                        if (k == 20)
                            break;
                    }
                    OvningStat pituus = new OvningStat();
                    pituus.Nr = k - 1;
                    pituus.Toiminto = "";
                    res[0] = pituus;
                }
                else
                {
                    OvningStat tieto = new OvningStat();
                    tieto.Nr = 0;
                    res[0] = tieto;
                }
                Array.Resize<OvningStat>(ref res, k);
                return res;
            }
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}