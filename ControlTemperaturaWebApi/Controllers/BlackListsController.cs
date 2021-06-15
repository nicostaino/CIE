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
using Clases.Models;

namespace ControlTemperaturaWebApi.Controllers
{
    public class BlackListsController : ApiController
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();

     

        // GET: api/BlackLists/5
        [ResponseType(typeof(BlackList))]
        public IHttpActionResult GetBlackList(long id)
        {
            BlackList blackList = db.BlackLists.Find(id);
            if (blackList == null)
            {
                return NotFound();
            }

            return Ok(blackList);
        }

        // PUT: api/BlackLists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBlackList(long id, BlackList blackList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != blackList.Id)
            {
                return BadRequest();
            }

            db.Entry(blackList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlackListExists(id))
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

        // POST: api/BlackLists
        [ResponseType(typeof(BlackList))]
        public IHttpActionResult PostBlackList(BlackList blackList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BlackLists.Add(blackList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = blackList.Id }, blackList);
        }

        // DELETE: api/BlackLists/5
        [ResponseType(typeof(BlackList))]
        public IHttpActionResult DeleteBlackList(long id)
        {
            BlackList blackList = db.BlackLists.Find(id);
            if (blackList == null)
            {
                return NotFound();
            }

            db.BlackLists.Remove(blackList);
            db.SaveChanges();

            return Ok(blackList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BlackListExists(long id)
        {
            return db.BlackLists.Count(e => e.Id == id) > 0;
        }
    }
}