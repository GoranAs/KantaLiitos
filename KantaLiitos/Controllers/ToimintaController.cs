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
    public class ToimintaController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Toiminta tehty";
        }

        // GET api/<controller>/5
        [HttpGet]
        public Toiminta[] Get(string id)
        {
            Toiminta[] res = new Toiminta[1000];

            String commandText = "";
            int k = 1;

            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4, StringSplitOptions.RemoveEmptyEntries);
            commandText = "dbo.SelectToimintaAjassa";

            SqlParameter par1 = new SqlParameter("@Cus", osat[0]);
            SqlParameter par2 = new SqlParameter("@HevosId", Convert.ToInt32(osat[1]));
            SqlParameter[] pars = new SqlParameter[]
            {
                par1, par2
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
                        Toiminta tieto = new Toiminta();

                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.Tyyppi = (string)reader["Tyyppi"];
                        tieto.Id = (int)reader["Id"];
                        tieto.Toiminto = (string)reader["Toiminto"];
                        tieto.Kustannus = (double)reader["Kustannus"];
                        tieto.Matka = (double)reader["Matka"];
                        tieto.Toistot = (int)reader["Toistot"];
                        tieto.Palkinto = (double)reader["Palkinto"];
                        tieto.Palkkio = (double)reader["Palkkio"];
                        tieto.Kesto = (double)reader["Kesto"];
                        tieto.Tehoaika = (double)reader["Tehoaika"];
                        tieto.Muuta = (string)reader["Muuta"];

                        res[k++] = tieto;
                    }
                    Toiminta pituus = new Toiminta();
                    pituus.Id = k - 1;
                    pituus.Palkinto = 0.0;
                    pituus.Palkkio = 0.0;
                    pituus.Tyyppi = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.Kustannus = 0.0;
                    pituus.Toistot = 0;
                    pituus.Palkinto = 0.0;
                    pituus.Palkkio = 0;
                    pituus.Kesto = 0;
                    pituus.Tehoaika = 0;
                    pituus.Muuta = "";
                    res[0] = pituus;
                }
                else
                {
                    Toiminta data = new Toiminta();
                    data.Id = 0;
                    res[0] = data;
                    k = 1;
                }
                Array.Resize<Toiminta>(ref res, k + 1);
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