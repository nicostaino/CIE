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
using System.Web.Mvc;
using AccessoADatos;
using Clases.Models;
using ControlTemperaturaWebApi.Models;
using Servicios.Services.EmployeeSer;
using Servicios.Services.User;
namespace ControlTemperaturaWebApi.Controllers
{
    public class EmployeesController : ApiController
    {
        // private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();
        private IEmployeeService _employeeService;
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();
        private static readonly HttpClient client = new HttpClient();
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //// GET: api/Employees
        //public JsonResult GetEmployees()
        //{
        //    // public virtual List<QR> QRHistory { get; set; }
        //    List<EmployeeObjAPi> listaRetorno = new List<EmployeeObjAPi>();


        //    //   var xo = db.Database.SqlQuery<AllEmployee>("select [Id], QR ,[Nombre] ,[Documento] ,[LandId] ,[Sex] ,[Local] ,[isforeman] ,[ContractorId] ,[EmployeeId] ,[TypeEmployeeId] ,[isActive] ,[birthDate]   ,[groupNumber]  FROM Employees");
        //    var empleados = db.Employees.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory });
        //    foreach (var empleado in empleados)
        //    {
        //        var aInsertar = new EmployeeObjAPi();
        //        aInsertar.id = empleado.Id;
        //        aInsertar.document = empleado.Documento;
        //        aInsertar.field = empleado.LandId;
        //        aInsertar.name = empleado.Nombre;
        //        aInsertar.qr = empleado.QR;
        //        aInsertar.contractor = empleado.ContractorId;
        //     //   aInsertar.foreman = empleado.EmployeeId;
        //        aInsertar.task = empleado.TypeEmployeeId;
        //        // aInsertar.base64 = empleado.Img;
        //        aInsertar.isForeman = empleado.isforeman;
        //        listaRetorno.Add(aInsertar);

        //    }
        //    return Json(empleados.ToList(),;
        //}

        public List<Employee> GetEmployees()
        {

            List<Employee> listaRetorno = new List<Employee>();
            foreach (var empleado in db.Employees.Select(p => new { p.Id, p.Documento,p.Apellido, p.EmployeeId, p.LandId, p.Nombre }))
            {
                var aInsertar = new Employee();
                aInsertar.Id = empleado.Id;
                aInsertar.Documento = empleado.Documento;
                aInsertar.LandId = empleado.LandId;
                aInsertar.Nombre = empleado.Nombre;
                aInsertar.Apellido = empleado.Apellido;

                listaRetorno.Add(aInsertar);
            }
            return listaRetorno;
        }
        public IHttpActionResult GetEmployee(int id, string Token)
        {
            List<EmployeeObjAPi> listaRetorno = new List<EmployeeObjAPi>();
            if (Token.Equals("62468a24-e1dc-407c-975b-7b00e8775b8b"))
            {
                foreach (var empleado in db.Employees.Where(z => z.LandId == id).Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre,p.Apellido, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory, p.hiringDate, p.ContractId }).ToList())
                {
                    var aInsertar = new EmployeeObjAPi();
                    aInsertar.id = empleado.Id;
                    aInsertar.document = empleado.Documento;
                    aInsertar.field = empleado.LandId;
                    aInsertar.name = empleado.Nombre;
                    aInsertar.lastName = empleado.Apellido;
                    aInsertar.qr = empleado.QR;
                    aInsertar.contractor = empleado.ContractorId;
                    aInsertar.foreman = empleado.Foreman == null ? 0 : empleado.Foreman.Id;
                    aInsertar.task = empleado.TypeEmployeeId;
                    aInsertar.base64 = null;
                    aInsertar.isForeman = empleado.isforeman;
                    aInsertar.hiringDate = (DateTime)empleado.hiringDate;
                    aInsertar.contractId = empleado.ContractId;
                    listaRetorno.Add(aInsertar);
                }
            }
            return Ok(listaRetorno);
        }

        [ResponseType(typeof(EmployeeObjAPi))]
        public IHttpActionResult PostEmployees(EmployeeObjAPi emp)
        {
            //parche para formato de fecha incorrecta.
            DateTime dt = DateTime.ParseExact(emp.birthDate, "dd/MM/yyyy", null);
            int año = dt.Year;
            DateTime birthDate;
            var onlyYear = int.Parse(año.ToString().Substring(2));
            if (onlyYear > 0 && onlyYear < 25)
            {
                birthDate = new DateTime(2000 + onlyYear, dt.Month, dt.Day);

            }
            else
            {
                birthDate = new DateTime(1900 + onlyYear, dt.Month, dt.Day);
            }
            Employee E = db.Employees.FirstOrDefault(x => x.Documento == emp.document && x.LandId == emp.field);
            Employee foreman = db.Employees.FirstOrDefault(x => x.Documento.Equals(emp.foreman.ToString()));

            if (E == null)
            {

                FeaturedEvent newFeatured = new FeaturedEvent()
                {
                    IngressDateTime = DateTime.Now,
                    CurrentContractorId = emp.contractor,
                    CurrentContractor = db.Contractors.Find(emp.contractor),
                    // IdCurrentForeman = foreman == null ? 0 : foreman.Id,
                    CurrentForeman = foreman,
                    //  E.TypeEmployeeId = emp.task;
                    CurrentTypeEmployee = db.TypeEmployee.FirstOrDefault(x => x.Id == emp.task),
                    CurrentTypeEmployeeId = emp.task,
                    //  Employee = E,
                    //EmployeeId = E.Id,
                    type = "Nuevo Empleado",
                    UserId = emp.user,
                    UserRegister = db.Users.FirstOrDefault(x => x.Id == emp.user),

                };

                E = new Employee();
                E.ContractorId = emp.contractor;
                E.Contractor = db.Contractors.Find(emp.contractor);
                E.Documento = emp.document;
                E.Foreman = foreman;
                E.Img = emp.base64;
                E.isActive = true;
                E.isforeman = emp.isForeman;
                E.LandId = emp.field;
                E.Land = db.Lands.FirstOrDefault(x => x.Id == emp.field);
                E.Sex = emp.sex;
                E.TypeEmployeeId = emp.task;
                E.TypeEmployee = db.TypeEmployee.FirstOrDefault(x => x.Id == emp.task);
                E.Nombre = emp.name;
                E.Apellido = emp.lastName;
                QR NewQR = new QR()
                {
                    Codigo = emp.qr,
                    RegistrationDate = DateTime.Now
                };
                E.QRHistory = new List<QR>();
                E.QRHistory.Add(NewQR);
                E.Local = false;
                E.groupNumber = emp.groupNumber;
                E.QR = emp.qr;
                E.birthDate = birthDate;
                E.ContractId = emp.contractId;
                E.Contract = db.Contracts.Find(emp.contractId);
                E.lastUpdate = DateTime.Now;
                db.Employees.Add(E);
                db.SaveChanges();
                E = db.Employees.FirstOrDefault(x => x.Documento == emp.document && x.LandId == emp.field);
                newFeatured.Employee = E;
                newFeatured.EmployeeId = E.Id;
                db.FeaturedEvents.Add(newFeatured);

            }
            else
            {
                if (E.TypeEmployeeId != emp.task || E.Foreman?.Id != emp.foreman || E.ContractorId != emp.contractor)
                {


                    FeaturedEvent newFeatured = new FeaturedEvent()
                    {
                        IngressDateTime = DateTime.Now,
                        CurrentContractorId = emp.contractor,
                        CurrentContractor = db.Contractors.Find(emp.contractor),
                        //   IdCurrentForeman = foreman == null ? 0 : foreman.Id,

                        CurrentForeman = foreman,
                        //  E.TypeEmployeeId = emp.task;
                        CurrentTypeEmployee = db.TypeEmployee.FirstOrDefault(x => x.Id == emp.task),
                        CurrentTypeEmployeeId = emp.task,
                        Employee = E,
                        EmployeeId = E.Id,
                        type = "Modificación de Empleado",
                        UserId = emp.user,
                        UserRegister = db.Users.FirstOrDefault(x => x.Id == emp.user),
                        LastContractor = E.Contractor,
                        //LastContractorId = E.Contractor == null ? 0 : E.Contractor.Id,
                        LastForeman = E.Foreman,
                        //  LastForemanId = E.Foreman == null ? 0 : E.Foreman.Id,
                        LastTypeEmployee = E.TypeEmployee,
                        //   LastTypeEmployeeId = E.TypeEmployee == null ? 0 : E.TypeEmployee.Id


                    };
                    db.FeaturedEvents.Add(newFeatured);
                }
                E.ContractorId = emp.contractor;
                E.Contractor = db.Contractors.Find(emp.contractor);
                E.Documento = emp.document;
                E.Foreman = foreman;
                E.Img = emp.base64;
                E.isActive = true;
                E.isforeman = emp.isForeman;
                E.LandId = emp.field;
                E.Land = db.Lands.FirstOrDefault(x => x.Id == emp.field);
                E.Sex = emp.sex;
                E.TypeEmployeeId = emp.task;
                E.TypeEmployee = db.TypeEmployee.FirstOrDefault(x => x.Id == emp.task);
                E.Nombre = emp.name;
                E.Apellido = emp.lastName;
                E.birthDate = DateTime.ParseExact(emp.birthDate, "dd/MM/yyyy", null);
                QR NewQR = new QR()
                {
                    Codigo = emp.qr,
                    RegistrationDate = DateTime.Now
                };
                E.groupNumber = emp.groupNumber;
                //E.QRHistory = new List<QR>();
                E.QR = emp.qr;
                E.QRHistory.Add(NewQR);
                E.Local = false;
                E.lastUpdate = DateTime.Now;
                E.ContractId = emp.contractId;
                E.Contract = db.Contracts.Find(emp.contractId);
                db.Entry(E).State = EntityState.Modified;

            }

            try
            {

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ExceptionLoger.Write(ex);
            }
            return CreatedAtRoute("DefaultApi", new { id = emp.document }, emp);

            //  return StatusCode(HttpStatusCode.NoContent);

        }

 }
}
