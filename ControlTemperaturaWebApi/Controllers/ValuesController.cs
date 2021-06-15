using AccessoADatos;
using ControlTemperaturaWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ControlTemperaturaWebApi.Controllers
{
    public class ValuesController : ApiController
    {
    private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();

 
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id, string Token)
        {
            string empleadoImg = "";
            if (Token.Equals("62468a24-e1dc-407c-975b-7b00e8775b8b"))

            {
                // public virtual List<QR> QRHistory { get; set; }

                  empleadoImg = db.Employees.FirstOrDefault(z => z.Id == id).Img;

            }
            return empleadoImg;
        }

            // POST api/values
            public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
