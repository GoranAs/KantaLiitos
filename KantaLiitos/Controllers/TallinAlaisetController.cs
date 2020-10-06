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
    public class TallinAlaisetController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Kaikki alaiset";
        }

        // GET api/<controller>/5
        [HttpGet]
        public TallinAlaiset[] Get(string id)
        {
            TallinAlaiset[] res = new TallinAlaiset[103];
            string[] osat = new string[5];
            String commandText = "";
            SqlParameter[] pars = new SqlParameter[4];
            commandText = "dbo.SelectTallinAlaiset";

            osat = id.Split(new char[] { '_' }, StringSplitOptions.None);
            if (osat[0] == "1")
            {
                commandText = "dbo.SelectTallinAlaiset";
            }
            SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
            pars = new SqlParameter[]
            {
                    par1
            };
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
                        TallinAlaiset tieto = new TallinAlaiset();
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.Tunn = (string)reader["Tunn"];
                        tieto.Nimi = (string)reader["Nimi"];
                        tieto.Rek = (string)reader["Rek"];
                        tieto.Tyyppi = (string)reader["Tyyppi"];
                        tieto.Laatu = (string)reader["Laatu"];
                        tieto.Muuta = (string)reader["Muuta"];
                        res[k++] = tieto;
                        if (k == 100)
                            break;
                    }
                    TallinAlaiset pituus = new TallinAlaiset();
                    pituus.Aika = DateTime.MinValue;
                    pituus.Tunn = (k - 1).ToString();
                    pituus.Nimi = "";
                    pituus.Rek = "";
                    pituus.Tyyppi = "";
                    pituus.Laatu = "";
                    pituus.Muuta = "";
                    res[0] = pituus;
                }
                else
                {
                    TallinAlaiset tieto = new TallinAlaiset();
                    tieto.Tunn = "0";
                    res[0] = tieto;
                }
                Array.Resize<TallinAlaiset>(ref res, k);
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