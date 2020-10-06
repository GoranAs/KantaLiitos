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
    public class HarjToimintoController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string[] Get(string id)
        {
            String commandText = "";
            int k = 0;
            string[] res = new string[20];

            string[] osat = new string[4];
            osat = id.Split(new char[] { '_' }, 3, StringSplitOptions.RemoveEmptyEntries);

            if (osat[0] == "1")
                commandText = "dbo.SelectHarjToiminto";
            else if (osat[0] == "2")
                commandText = "dbo.SelectHuolToiminto";
            else if (osat[0] == "3")
                commandText = "dbo.SelectTervToiminto";
            else if (osat[0] == "4")
                commandText = "dbo.SelectVarsToiminto";
            else if (osat[0] == "5")
                commandText = "dbo.SelectTaloKohde";
            else if (osat[0] == "6")
                commandText = "dbo.SelectTaloLiike";

            SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
            SqlParameter[] pars = new SqlParameter[]
            {
                par1
            };

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
                        if (osat[0] == "5")
                            res[k++] = (string)reader["Kohde"];
                        else if (osat[0] == "6")
                            res[k++] = (string)reader["Liike"];
                        else
                            res[k++] = (string)reader["Toiminto"];
                    }
                }
                else
                {
                    res[0] = "";
                    k = 1;
                }
                Array.Resize(ref res, k);
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