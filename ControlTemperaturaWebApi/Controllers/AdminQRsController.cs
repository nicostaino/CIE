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
    public class AdminQRsController : ApiController
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();

        // GET: api/AdminQRs
        public List<AdminQR> GetAdminQRs()
        {
            var x = db.Database.SqlQuery<AdminQR>("SELECT *   FROM[Temperatura].[dbo].[AdminQRs]");

           // return Json(x.ToList<LandUser>(), JsonRequestBehavior.AllowGet);
            return x.ToList();
        }

        // GET: api/AdminQRs/5
        [ResponseType(typeof(AdminQR))]
        public IHttpActionResult GetAdminQR(long id)
        {
            AdminQR adminQR = db.AdminQRs.Find(id);
            if (adminQR == null)
            {
                return NotFound();
            }

            return Ok(adminQR);
        }

        // PUT: api/AdminQRs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdminQR(long id, AdminQR adminQR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adminQR.Id)
            {
                return BadRequest();
            }

            db.Entry(adminQR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminQRExists(id))
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

        // POST: api/AdminQRs
        [ResponseType(typeof(AdminQR))]
        public IHttpActionResult PostAdminQR(AdminQR adminQR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AdminQRs.Add(adminQR);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = adminQR.Id }, adminQR);
        }

        // DELETE: api/AdminQRs/5
        [ResponseType(typeof(AdminQR))]
        public IHttpActionResult DeleteAdminQR(long id)
        {
            AdminQR adminQR = db.AdminQRs.Find(id);
            if (adminQR == null)
            {
                return NotFound();
            }

            db.AdminQRs.Remove(adminQR);
            db.SaveChanges();

            return Ok(adminQR);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdminQRExists(long id)
        {
            return db.AdminQRs.Count(e => e.Id == id) > 0;
        }
    }
}