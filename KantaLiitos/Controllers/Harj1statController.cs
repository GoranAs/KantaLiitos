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
    public class Harj1statController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            return "Harj1stat";
        }

        // GET api/<controller>/5
        [HttpGet]
        public Harj1stat[] Get(string id)
        {
            Harj1stat[] res = new Harj1stat[23];
            String commandText = "";
            SqlParameter[] pars = new SqlParameter[6];
            int k = 1;
            string[] osat = new string[6];

            osat = id.Split(new char[] { '_' }, 5, StringSplitOptions.None);
            if (osat[0] == "1")
            {
                commandText = "dbo.SelectHarj1StatLen";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@Hevosid", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                par1, par2
                };
            }
            else if (osat[0] == "2")
            {
                commandText = "dbo.SelectHarj1StatNr";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@Hevosid", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                par1, par2
                };
            }
            else if (osat[0] == "3")
            {
                commandText = "dbo.SelectHarj1StatLenAika";
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
                        Harj1stat tieto = new Harj1stat();
                        tieto.Toiminto = (string)reader["Toiminto"];
                        tieto.Nr = (int)reader["Nr"];
                        tieto.Len = (double)reader["Len"];
                        tieto.Tid = (double)reader["Tid"];
                        tieto.Nop = (double)reader["Nop"];
                        tieto.Aik = (double)reader["Aik"];
                        tieto.Toi = (int)reader["Toi"];
                        res[k++] = tieto;
                        if (k == 20)
                            break;
                    }
                    Harj1stat pituus = new Harj1stat();
                    pituus.Nr = k - 1;
                    pituus.Toiminto = "";
                    res[0] = pituus;
                }
                else
                {
                    Harj1stat tieto = new Harj1stat();
                    tieto.Nr = 0;
                    res[0] = tieto;
                }
                Array.Resize<Harj1stat>(ref res, k);
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