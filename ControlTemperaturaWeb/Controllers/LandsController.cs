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
using AccessoADatos;
using Clases;

namespace ControlTemperaturaWeb.Controllers
{
    public class LandsController : ApiController
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();

        // GET: api/Lands
        public List<Land> GetLands()
        {
            List<Land> listaRetorno = new List<Land>();
            foreach(var campo in db.Lands)
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
            Land land = db.Lands.Find(id);
            if (land == null)
            {
                return NotFound();
            }

            return Ok(land);
        }

        // PUT: api/Lands/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLand(long id, Land land)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != land.Id)
            {
                return BadRequest();
            }

            db.Entry(land).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LandExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Lands
        [ResponseType(typeof(Land))]
        public IHttpActionResult PostLand(Land land)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lands.Add(land);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = land.Id }, land);
        }

        // DELETE: api/Lands/5
        [ResponseType(typeof(Land))]
        public IHttpActionResult DeleteLand(long id)
        {
            Land land = db.Lands.Find(id);
            if (land == null)
            {
                return NotFound();
            }

            db.Lands.Remove(land);
            db.SaveChanges();

            return Ok(land);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LandExists(long id)
        {
            return db.Lands.Count(e => e.Id == id) > 0;
        }
    }
}