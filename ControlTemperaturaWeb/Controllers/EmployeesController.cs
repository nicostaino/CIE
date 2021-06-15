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
using Servicios.Services.User; 

namespace ControlTemperaturaWeb.Controllers
{
    public class EmployeesController : ApiController
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();
        private IUserService _userService;
        public EmployeesController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Employees
        public List<Employee> GetEmployees()
        {
            var a = _userService.GetAll();
            List<Employee> listaRetorno = new List<Employee>();
            foreach (var empleado in db.Employees)
            {
                var aInsertar = new Employee();
                aInsertar.Id = empleado.Id;
                aInsertar.Documento = empleado.Documento;
                aInsertar.LandId = empleado.LandId;
                aInsertar.Nombre = empleado.Nombre;
                listaRetorno.Add(aInsertar);
            }
            return listaRetorno;
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(long id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(long id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(long id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(long id)
        {
            return db.Employees.Count(e => e.Id == id) > 0;
        }
    }
}