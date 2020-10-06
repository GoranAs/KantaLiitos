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
    public class UusiKayttajaController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Kayttaja ok";
        }

        // GET api/<controller>/5
        [HttpGet]
        public UusiKayttaja[] Get(string id)
        {
            UusiKayttaja[] res = new UusiKayttaja[103];
            string[] osat = new string[5];
            String commandText = "";
            SqlParameter[] pars = new SqlParameter[4];
            commandText = "dbo.SelectUusiKayttaja";

            osat = id.Split(new char[] { '_' }, StringSplitOptions.None);
            if (osat[0] == "1")
            {
                commandText = "dbo.SelectUusiKayttaja";
            }
            else if (osat[0] == "2")
            {
                commandText = "dbo.SelectCusUusiKayttaja";
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
                        UusiKayttaja tieto = new UusiKayttaja();
                        tieto.Id = (int)reader["Id"];
                        tieto.Cus = (string)reader["Cus"];
                        tieto.Aika = (DateTime)reader["Aika"];
                        tieto.Kaynimi = (string)reader["Kaynimi"];
                        tieto.Salasana = (string)reader["Salasana"];
                        tieto.Nimi = (string)reader["Nimi"];
                        tieto.TalliNimi = (string)reader["TalliNimi"];
                        tieto.TalliCus = (string)reader["TalliCus"];
                        tieto.OmistajaCus = (string)reader["OmistajaCus"];
                        tieto.Oikeus = (int)reader["Oikeus"];
                        tieto.Muuta = (string)reader["Muuta"];
                        res[k++] = tieto;
                        if (k == 100)
                            break;
                    }
                    UusiKayttaja pituus = new UusiKayttaja();
                    pituus.Id = k - 1;
                    pituus.Cus = "";
                    pituus.Aika = DateTime.MinValue;
                    pituus.Kaynimi = "";
                    pituus.Salasana = "";
                    pituus.Nimi = "";
                    pituus.TalliNimi = "";
                    pituus.TalliCus = "";
                    pituus.OmistajaCus = "";
                    pituus.Oikeus = 0;
                    pituus.Muuta = "";
                    res[0] = pituus;
                }
                else
                {
                    UusiKayttaja tieto = new UusiKayttaja();
                    tieto.Id = 0;
                    res[0] = tieto;
                }
                Array.Resize<UusiKayttaja>(ref res, k);
                return res;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] UusiKayttaja tieto)
        {
            string aa = "";
            SqlParameter[] pars;
            String commandText;

            if (tieto.Id == 0)
            {
                commandText = "dbo.InsertUusiKayttaja";

                SqlParameter par1 = new SqlParameter("@Aika", tieto.Aika);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@Kaynimi", tieto.Kaynimi);
                SqlParameter par4 = new SqlParameter("@Salasana", tieto.Salasana);
                SqlParameter par5 = new SqlParameter("@Nimi", tieto.Nimi);
                SqlParameter par6 = new SqlParameter("@TalliNimi", tieto.TalliNimi);
                SqlParameter par7 = new SqlParameter("@TalliCus", tieto.TalliCus);
                SqlParameter par8 = new SqlParameter("@OmistajaCus", tieto.OmistajaCus);
                SqlParameter par9 = new SqlParameter("@Oikeus", tieto.Oikeus);
                SqlParameter par10 = new SqlParameter("@Muuta", tieto.Muuta);
                pars = new SqlParameter[]
                {
                    par1, par2, par3, par4, par5, par6, par7, par8, par9, par10
                };
                aa = "Lisätty ";
            }

            else
            {
                commandText = "dbo.UpdateUusiKayttaja";

                SqlParameter par1 = new SqlParameter("@Id", tieto.Id);
                SqlParameter par2 = new SqlParameter("@Cus", tieto.Cus);
                SqlParameter par3 = new SqlParameter("@Kaynimi", tieto.Kaynimi);
                SqlParameter par4 = new SqlParameter("@Salasana", tieto.Salasana);
                SqlParameter par5 = new SqlParameter("@Nimi", tieto.Nimi);
                SqlParameter par6 = new SqlParameter("@TalliNimi", tieto.TalliNimi);
                SqlParameter par7 = new SqlParameter("@TalliCus", tieto.TalliCus);
                SqlParameter par8 = new SqlParameter("@OmistajaCus", tieto.OmistajaCus);
                SqlParameter par9 = new SqlParameter("@Oikeus", tieto.Oikeus);
                SqlParameter par10 = new SqlParameter("@Muuta", tieto.Muuta);
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
                        return aa + stat.ToString() + " rivi";
                    }
                    catch (Exception exception)
                    {
                        return exception.Message;
                    }
                }
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public string Delete(string id)
        {
            String commandText;
            string[] osat = new string[5];
            osat = id.Split(new char[] { '_' }, 4);

            if (osat[0] == "1")
                commandText = "dbo.DeleteUusiKayttaja";
            else
                commandText = "dbo.DeleteUusiKayttaja";
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