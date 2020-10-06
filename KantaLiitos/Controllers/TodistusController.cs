
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO.Compression;
using KantaLiitos.Models;

namespace KantaLiitos.Controllers
{
    public class TodistusController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] Todistus tieto)
        {
            if (ModelState.IsValid)
            {
                int aa = tieto.Data.Length;
                return tieto.Nimi + " " + aa.ToString();
            }
            else
            {
                return tieto.Nimi + " ModelState";
            }
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