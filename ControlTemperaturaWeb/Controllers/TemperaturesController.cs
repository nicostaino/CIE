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

namespace ControlTemperaturaWeb.Models
{
    public class TemperaturesController : ApiController
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();

        // GET: api/Temperatures
        public IQueryable<Temperature> GetTemperatures()
        {
            return db.Temperatures;
        }

        // GET: api/Temperatures/5
        [ResponseType(typeof(Temperature))]
        public IHttpActionResult GetTemperature(long id)
        {
            Temperature temperature = db.Temperatures.Find(id);
            if (temperature == null)
            {
                return NotFound();
            }

            return Ok(temperature);
        }

        // PUT: api/Temperatures/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTemperature(long id, Temperature temperature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != temperature.Id)
            {
                return BadRequest();
            }

            db.Entry(temperature).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemperatureExists(id))
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

        // POST: api/Temperatures
   
        public IHttpActionResult PostTemperature(IList<TemperatureRecibeApi> temperatureList)
        {
            IList<TemperatureRecibeApi> temperatureListRetorno = new List<TemperatureRecibeApi>();
            foreach (var temperature in temperatureList)
            {
                try
                {
                    Temperature newTemperature = new Temperature();

                    newTemperature.Date = temperature.FECHA;
                    newTemperature.local = temperature.LOCAL == 1;
                    newTemperature.Value = temperature.TEMPERATURA;
                    newTemperature.User = db.Users.FirstOrDefault(x => x.Id == temperature.ID_USUARIO);
                    newTemperature.UserId = newTemperature.User.Id;
                    newTemperature.Land = db.Lands.FirstOrDefault(x => x.Id == temperature.ID_CAMPO);
                    newTemperature.LandId = newTemperature.Land.Id;
                    newTemperature.Name = temperature.NOMBRE;
                    newTemperature.Identification = temperature.DOCUMENTO;
                    if (temperature.LOCAL == 1)
                    {
                        newTemperature.Employee = db.Employees.FirstOrDefault(x => x.Documento == temperature.DOCUMENTO);
                        newTemperature.EmployeeId = newTemperature.Employee.Id; 
                    }
         
                    db.Temperatures.Add(newTemperature);
                    db.SaveChanges();
  
                    temperatureListRetorno.Add(temperature);
                }
                catch (Exception)
                {
 
                }
            }
           

            return CreatedAtRoute("DefaultApi", new { id = 1 }, temperatureListRetorno);
        }

        // DELETE: api/Temperatures/5
        [ResponseType(typeof(Temperature))]
        public IHttpActionResult DeleteTemperature(long id)
        {
            Temperature temperature = db.Temperatures.Find(id);
            if (temperature == null)
            {
                return NotFound();
            }

            db.Temperatures.Remove(temperature);
            db.SaveChanges();

            return Ok(temperature);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TemperatureExists(long id)
        {
            return db.Temperatures.Count(e => e.Id == id) > 0;
        }
    }
}