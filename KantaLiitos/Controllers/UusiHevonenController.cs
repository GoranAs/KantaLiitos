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
    public class UusiHevonenController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Hevonen laukkaa";
        }

        // GET api/<controller>/5
        [HttpGet]
        public UusiHevonen[] Get(string id)
        {
            UusiHevonen[] res = new UusiHevonen[103];
            string[] osat = new string[5];
            string commandText = "";
            SqlParameter[] pars = new SqlParameter[4];

            osat = id.Split(new char[] { '_' }, StringSplitOptions.None);
            if (osat[0] == "1")
            {
                commandText = "dbo.SelectUusiHevonen";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                pars = new SqlParameter[]
                {
                    par1
                };
            }
            if (osat[0] == "2")
            {
                commandText = "dbo.SelectIdUusiHevonen";
                SqlParameter par1 = new SqlParameter("@Id", Convert.ToInt32(osat[1]));
                pars = new SqlParameter[]
                {
                    par1
                };
            }
            else if (osat[0] == "3")
            {
                commandText = "dbo.SelectKaikkiUusiHevonen";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                pars = new SqlParameter[]
                {
                    par1
                };
            }
            else if (osat[0] == "4")
            {
                commandText = "dbo.SelectOmistajaUusiHevonen";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                pars = new SqlParameter[]
                {
                    par1
                };
            }
            else if (osat[0] == "5")
            {
                commandText = "dbo.SelectUusiHevonenOrderCus";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                pars = new SqlParameter[]
                {
                    par1
                };
            }
            else if (osat[0] == "6")
            {
                commandText = "dbo.SelectUusiHevonenOrderOmisCus";
                SqlParameter par1 = new SqlParameter("@Cus", osat[1]);
                pars = new SqlParameter[]
                {
                    par1
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
                        UusiHevonen tieto = new UusiHevonen();
                        tieto.Id = (int)reader["Id"];
                        tieto.Cus = (string)reader["Cus"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.Tunnus = (string)reader["Tunnus"];
                        tieto.Nimi = (string)reader["Nimi"];
                        tieto.Tyyppi = (string)reader["Tyyppi"];
                        tieto.Laatu = (string)reader["Laatu"];
                        tieto.Kuva = (string)reader["Kuva"];
                        tieto.Omistaja = (string)reader["Omistaja"];
                        tieto.OmisCus = (string)reader["OmisCus"];
                        tieto.Talli = (string)reader["Talli"];
                        tieto.TalliCus = (string)reader["TalliCus"];
                        tieto.Lempinimi = (string)reader["Lempinimi"];
                        tieto.Selvennys = (string)reader["Selvennys"];

                        if (tieto.Omistaja == null)
                            tieto.Omistaja = "";
                        if (tieto.OmisCus == null)
                            tieto.OmisCus = "";
                        if (tieto.Talli == null)
                            tieto.Talli = "";
                        if (tieto.TalliCus == null)
                            tieto.TalliCus = "";
                        if (tieto.Lempinimi == null)
                            tieto.Lempinimi = "";

                        res[k++] = tieto;
                        if (k == 100)
                            break;
                    }
                    UusiHevonen pituus = new UusiHevonen();
                    pituus.Id = k - 1;
                    pituus.Cus = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.Tunnus = "";
                    pituus.Nimi = "";
                    pituus.Tyyppi = "";
                    pituus.Laatu = "";
                    pituus.Kuva = "";
                    pituus.Omistaja = "";
                    pituus.OmisCus = "";
                    pituus.Talli = "";
                    pituus.TalliCus = "";
                    pituus.Lempinimi = "";
                    pituus.Selvennys = "";
                    res[0] = pituus;
                }
                else
                {
                    UusiHevonen tieto = new UusiHevonen();
                    tieto.Id = 0;
                    res[0] = tieto;
                }

                Array.Resize<UusiHevonen>(ref res, k);
                return res;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] UusiHevonen tieto)
        {
            string aa = "";
            String commandText = "";
            SqlParameter[] pars;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertUusiHevonen";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@Tunnus", tieto.Tunnus);
                SqlParameter par4 = new SqlParameter("@Nimi", tieto.Nimi);
                SqlParameter par5 = new SqlParameter("@Tyyppi", tieto.Tyyppi);
                SqlParameter par6 = new SqlParameter("@Laatu", tieto.Laatu);
                SqlParameter par7 = new SqlParameter("@Kuva", tieto.Kuva);
                SqlParameter par8 = new SqlParameter("@Omistaja", tieto.Omistaja);
                SqlParameter par9 = new SqlParameter("@OmisCus", tieto.OmisCus);
                SqlParameter par10 = new SqlParameter("@Talli", tieto.Talli);
                SqlParameter par11 = new SqlParameter("@TalliCus", tieto.TalliCus);
                SqlParameter par12 = new SqlParameter("@Lempinimi", tieto.Lempinimi);
                SqlParameter par13 = new SqlParameter("@Selvennys", tieto.Selvennys);
                pars = new SqlParameter[]
                {
                        par1, par2, par3, par4, par5, par6, par7, par8, par9, par10,
                        par11, par12, par13
                };
                aa = "Lisätty ";
            }
            else
            {
                commandText = "dbo.UpdateUusiHevonen";

                SqlParameter par1 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par2 = new SqlParameter("@Tunnus", tieto.Tunnus);
                SqlParameter par3 = new SqlParameter("@Nimi", tieto.Nimi);
                SqlParameter par4 = new SqlParameter("@Tyyppi", tieto.Tyyppi);
                SqlParameter par5 = new SqlParameter("@Laatu", tieto.Laatu);
                SqlParameter par6 = new SqlParameter("@Kuva", tieto.Kuva);
                SqlParameter par7 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par8 = new SqlParameter("@Omistaja", tieto.Omistaja);
                SqlParameter par9 = new SqlParameter("@OmisCus", tieto.OmisCus);
                SqlParameter par10 = new SqlParameter("@Talli", tieto.Talli);
                SqlParameter par11 = new SqlParameter("@TalliCus", tieto.TalliCus);
                SqlParameter par12 = new SqlParameter("@Lempinimi", tieto.Lempinimi);
                SqlParameter par13 = new SqlParameter("@Selvennys", tieto.Selvennys);
                pars = new SqlParameter[]
                {
                        par1, par2, par3, par4, par5, par6, par7, par8, par9, par10,
                        par11, par12, par13
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
        public void Put(int id, [FromBody] UusiHevonen tieto)
        {
        }

        // DELETE api/<controller>/5
        public string Delete(string id)
        {
            String commandText;
            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4);

            if (osat[0] == "1")
                commandText = "dbo.Delete1UusiHevonen";
            else
                commandText = "dbo.DeleteUusiHevonen";
            /*
                        if (osat[2].IndexOf('#') != -1)
                            osat[2].Replace('#', '-');
            */
            SqlParameter par2 = new SqlParameter("@Cus", osat[1]);
            SqlParameter par4 = new SqlParameter("@Id", Convert.ToInt32(osat[2]));
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