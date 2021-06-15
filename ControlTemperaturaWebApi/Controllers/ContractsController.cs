using System;
using System.Collections;
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
    [CustomExceptionFilter]
    public class ContractsController : ApiController
    {
        [ResponseType(typeof(Contract))]
        public IHttpActionResult GetContracts()
        {
            using(var context = new AppIngresoTemperatrasContext())
            {
                context.Configuration.LazyLoadingEnabled = false;

                var contracts = context.Contracts
                                    //.Include("Contractor")
                                    //.Include("Land")
                                    //.Include("TypeEmployees")
                                    .ToList();
                return Ok(contracts);
            }

        }
        // GET: api/Contracts/5
        [ResponseType(typeof(Contract))]
        public IHttpActionResult GetContract(long id)
        {
            using (var context = new AppIngresoTemperatrasContext())
            {
                context.Configuration.LazyLoadingEnabled = false;

                var contract = context.Contracts
                                    .Include("Contractor")
                                    .Include("Land")
                                    .Include("TypeEmployees")
                                    .FirstOrDefault(c => c.Id == id);

                if (contract == null)
                {
                    return NotFound();
                }
                return Ok(contract);
            }
        }

        // PUT: api/Contracts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContract(long id, Contract contract)
        {
            using (var context = new AppIngresoTemperatrasContext())
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != contract.Id)
                {
                    return BadRequest();
                }

                context.Entry(contract).State = EntityState.Modified;

                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Contracts
        [ResponseType(typeof(Contract))]
        public IHttpActionResult PostContract(Contract contract)
        {
            using (var context = new AppIngresoTemperatrasContext())
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                context.Contracts.Add(contract);
                context.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = contract.Id }, contract);
            }
        }

        // DELETE: api/Contracts/5
        [ResponseType(typeof(Contract))]
        public IHttpActionResult DeleteContract(long id)
        {
            using (var context = new AppIngresoTemperatrasContext())
            {
                Contract contract = context.Contracts.Find(id);
                if (contract == null)
                {
                    return NotFound();
                }

                context.Contracts.Remove(contract);
                context.SaveChanges();

                return Ok(contract);
            }
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool ContractExists(long id)
        {
            using(var context = new AppIngresoTemperatrasContext())
            {
            return context.Contracts.Count(e => e.Id == id) > 0;
            }
        }
    }
}