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

public class TypeEmployeesController : ApiController
    {
    private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();

    // GET: api/TypeEmployees
    public IHttpActionResult GetTypeEmployees()
        {
            return Ok(db.TypeEmployee.Select(p => new { p.Id, p.Description , p.LandId }).ToList()); 
             
        }

        // GET: api/TypeEmployees/5
        [ResponseType(typeof(TypeEmployee))]
        public IHttpActionResult GetTypeEmployee(long id)
        {
            TypeEmployee typeEmployee = db.TypeEmployee.Find(id);
            if (typeEmployee == null)
            {
                return NotFound();
            }

            return Ok(typeEmployee);
        }
     
    }
