using AccessoADatos;
using Clases;
using Servicios.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ControlTemperaturaWeb.Controllers
{
    public class ValuesController : ApiController
    {
        private ILandService _landService;
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();
        // GET api/values
        public List<Land> Get()
        {
            var p = db.Lands.ToList();

            //return _landService.GetAll();
            return p;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
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
