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
    public class VarsStatController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Varsa status";
        }

        // GET api/<controller>/5
        [HttpGet]
        public VarsStat[] Get(string id)
        {
            VarsStat[] res = new VarsStat[103];
            string[] osat = new string[5];
            string commandText = "";
            SqlParameter[] pars = new SqlParameter[4];

            osat = id.Split(new char[] { '_' }, StringSplitOptions.None);
            if (osat[0] == "1")
            {
                commandText = "dbo.SelectVarsStat";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@Hevosid", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                    par1, par2
                };
            }
            else if (osat[0] == "2")
            {
                commandText = "dbo.SelectVarsStatLkm";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@Hevosid", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                    par1, par2
                };
            }
            else if (osat[0] == "3")
            {
                commandText = "dbo.SelectVarsStatAika";
                SqlParameter par1 = new SqlParameter("@Aika", osat[1]);
                SqlParameter par2 = new SqlParameter("@Cus", osat[2]);
                SqlParameter par3 = new SqlParameter("@Hevosid", Convert.ToInt32(osat[3]));
                pars = new SqlParameter[]
                {
                    par1, par2, par3
                };
            }
            else if (osat[0] == "4")
            {
                commandText = "dbo.SelectVarsStatAikaLkm";
                SqlParameter par1 = new SqlParameter("@Aika", osat[1]);
                SqlParameter par2 = new SqlParameter("@Cus", osat[2]);
                SqlParameter par3 = new SqlParameter("@Hevosid", Convert.ToInt32(osat[3]));
                pars = new SqlParameter[]
                {
                    par1, par2, par3
                };
            }
            int k = 1;

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
                        VarsStat tieto = new VarsStat();
                        tieto.Toiminto = (string)reader["Toiminto"];
                        tieto.Lkm = (int)reader["Lkm"];
                        tieto.Kust = (double)reader["Kust"];
                        res[k++] = tieto;
                        if (k == 100)
                            break;
                    }
                    VarsStat pituus = new VarsStat();
                    pituus.Lkm = k - 1;
                    pituus.Toiminto = "";
                    pituus.Kust = 0.0;
                    res[0] = pituus;
                }
                else
                {
                    VarsStat tieto = new VarsStat();
                    tieto.Lkm = 0;
                    res[0] = tieto;
                }

                Array.Resize<VarsStat>(ref res, k);
                return res;
            }
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}