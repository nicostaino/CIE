using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;
using AccessoADatos;
using Clases;
using Servicios.Services;
 
  namespace ControlTemperaturaWebApi.Controllers
{
    public class LandsController : ApiController
    {
       // private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();
            private ILandService _landService;
            public LandsController(ILandService landService)
            {
                _landService = landService;
            }

            // GET: api/Lands
            public List<Land> GetLands()
        {
            List<Land> listaRetorno = new List<Land>();
            foreach (var campo in _landService.GetAll() )
            {
                var aInsertar = new Land();
                aInsertar.Id = campo.Id;
                aInsertar.Name = campo.Name;
                listaRetorno.Add(aInsertar);
            }
            return listaRetorno;
        }

        // GET: api/Lands/5
        [ResponseType(typeof(Land))]
        public IHttpActionResult GetLand(long id)
        {
            Land land = _landService.GetById(id);
            if (land == null)
            {
                return NotFound();
            }

            return Ok(land);
        }

      

    }
}