using KantaLiitos.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KantaLiitos.Controllers
{
    public class LokiController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Loki ok";
        }

        // GET api/<controller>/5
        [HttpGet]
        public string[] Get(string id)
        {
            string[] res = new string[] {};
            FileInfo fil = new FileInfo(@"C:\Aapilot\Loki.txt");
            if (fil.Exists)
            {
                try
                {
                    res = File.ReadAllLines(@"C:\Aapilot\Loki.txt");
                }
                catch (Exception exc)
                {
                    res[0] = "Virhe";
                    res[1] = exc.InnerException.Message;
                }
            }
            else
            {
                res[0] = "Virhe";
                res[1] = "Ei lokitiedostoa";
            }
            return res;
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