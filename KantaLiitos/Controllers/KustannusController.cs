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
    public class KustannusController : ApiController
    {
        // GET: api/Kustannus
        [HttpGet]
        public string Get()
        {
            return "Kustannus tehty";
        }

        // GET: api/Kustannus/5
        [HttpGet]
        public Kustannus[] Get(string id)
        {
            Kustannus tieto = new Kustannus();
            Kustannus[] res = new Kustannus[10];

            String commandText = "";
            SqlParameter[] pars = new SqlParameter[5];
            int k = 1;
            int hepo = 0;
            
            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4, StringSplitOptions.RemoveEmptyEntries);

            if (osat[0] == "1")
            {
                commandText = "dbo.Kustannus";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@HevosId", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                    par1, par2
                };
            }
            else if (osat[0] == "2")
            {
                commandText = "dbo.Kaikkikustannus";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                SqlParameter par2 = new SqlParameter("@HevosId", Convert.ToInt32(osat[2]));
                pars = new SqlParameter[]
                {
                    par1, par2
                };
            }
            else if (osat[0] == "3")
            {
                commandText = "dbo.KaikkiOmistajankustannus";
                SqlParameter par1 = new SqlParameter("@OmisCus", osat[1]);
                pars = new SqlParameter[]
                {
                    par1
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
                        string a1 = reader["Palkinto"].ToString();
                        string a2 = reader["Palkkio"].ToString();
                        string a3 = reader["Kilpkust"].ToString();
                        string a4 = reader["Harjkust"].ToString();
                        string a5 = reader["Huolkust"].ToString();
                        string a6 = reader["Tervkust"].ToString();
                        string a7 = reader["Varskust"].ToString();
                        string a8 = reader["Talokust"].ToString();
                        string a9 = reader["Astukust"].ToString();
                        if (a1 == null) a1 = "1";
                        if (a2 == null) a2 = "1";
                        if (a3 == null) a3 = "1";
                        if (a4 == null) a4 = "1";
                        if (a5 == null) a5 = "1";
                        if (a6 == null) a6 = "1";
                        if (a7 == null) a7 = "1";
                        if (a8 == null) a8 = "1";
                        if (a9 == null) a9 = "1";
                        tieto.Palkinto = Convert.ToDouble(a1);
                        tieto.Palkkio = Convert.ToDouble(a2);
                        tieto.Kilpkust = Convert.ToDouble(a3);
                        tieto.Harjkust = Convert.ToDouble(a4);
                        tieto.Huolkust = Convert.ToDouble(a5);
                        tieto.Tervkust = Convert.ToDouble(a6);
                        tieto.Varskust = Convert.ToDouble(a7);
                        tieto.Talokust = Convert.ToDouble(a8);
                        tieto.Astukust = Convert.ToDouble(a9);

                        //tieto.Palkinto = (double)reader["Palkinto"];
                        //tieto.Palkkio = (double)reader["Palkkio"];
                        //tieto.Kilpkust = (double)reader["Kilpkust"];
                        //tieto.Harjkust = (double)reader["Harjkust"];
                        //tieto.Huolkust = (double)reader["Huolkust"];
                        //tieto.Tervkust = (double)reader["Tervkust"];
                        //tieto.Varskust = (double)reader["Varskust"];
                        //tieto.Talokust = (double)reader["Talokust"];
                        //tieto.Astukust = (double)reader["Astukust"];

                        res[k++] = tieto;
                        if (k == 2)
                            break;
                    }
                    Kustannus pituus = new Kustannus();
                    pituus.Kilpkust = k - 1;
                    pituus.Palkinto = 0;
                    pituus.Palkkio = 0;
                    pituus.Harjkust = 0;
                    pituus.Huolkust = 0;
                    pituus.Tervkust = 0;
                    pituus.Varskust = 0;
                    pituus.Talokust = 0;
                    pituus.Astukust = 0;
                    res[0] = pituus;
                }
                else
                {
                    Kustannus data = new Kustannus();
                    data.Kilpkust = 0;
                    res[0] = data;
                    k = 1;
                }
                Array.Resize<Kustannus>(ref res, k + 1);
                return res;
            }
        }

        // POST: api/Kustannus
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Kustannus/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Kustannus/5
        public void Delete(int id)
        {
        }
    }
}
