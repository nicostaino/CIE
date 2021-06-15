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
    public class ContractorsController : ApiController
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();

        // GET: api/Contractors
        public List<Contractor> GetContractors()
        {
          //  return db.Contractors;
            List<Contractor> listaRetorno = new List<Contractor>();
            foreach (var usuario in db.Contractors)
            {
                var aInsertar = new Contractor();
                aInsertar.Id = usuario.Id;
                aInsertar.Name = usuario.Name;
                aInsertar.IsEnabled = usuario.IsEnabled;
                aInsertar.LandId = usuario.LandId;
                listaRetorno.Add(aInsertar);
            }
            return listaRetorno;
            //Select(p => new { p.Name, p.LandId, p.IsEnabled, p.Description, p.Id }).ToList<Contractor>();
            ////foreach (var empleado in db.Employees.Select(p => new { p.Id, p.Documento, p.EmployeeId, p.LandId, p.Nombre }))
            ////{
            ////    var aInsertar = new Employee();
            ////    aInsertar.Id = empleado.Id;
            ////    aInsertar.Documento = empleado.Documento;
            ////    aInsertar.LandId = empleado.LandId;
            ////    aInsertar.Nombre = empleado.Nombre;
            ////    listaRetorno.Add(aInsertar);
            ////}
        }

        // GET: api/Contractors/5
      
        public List<Contractor> GetContractor(long? id)
        {

           
            List<Contractor> listaRetorno = new List<Contractor>();
            foreach (var usuario in db.Contractors.Where(o => o.LandId == id))
            {
                var aInsertar = new Contractor();
                aInsertar.Id = usuario.Id;
                aInsertar.Name = usuario.Name;
                aInsertar.IsEnabled = usuario.IsEnabled;
                aInsertar.LandId = usuario.LandId;
                listaRetorno.Add(aInsertar);
            }
            return listaRetorno;
             
        }

        // PUT: api/Contractors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContractor(long id, Contractor contractor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contractor.Id)
            {
                return BadRequest();
            }

            db.Entry(contractor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractorExists(id))
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

        // POST: api/Contractors
        [ResponseType(typeof(Contractor))]
        public IHttpActionResult PostContractor(Contractor contractor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contractors.Add(contractor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contractor.Id }, contractor);
        }

        // DELETE: api/Contractors/5
        [ResponseType(typeof(Contractor))]
        public IHttpActionResult DeleteContractor(long id)
        {
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return NotFound();
            }

            db.Contractors.Remove(contractor);
            db.SaveChanges();

            return Ok(contractor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContractorExists(long id)
        {
            return db.Contractors.Count(e => e.Id == id) > 0;
        }
    }
}