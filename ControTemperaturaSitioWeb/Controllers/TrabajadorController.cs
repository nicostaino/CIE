using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccessoADatos;
using Clases.Models;
using ControlTemperaturaSitioWeb.CustomAuthentication;
 
using System.Web.Routing;
using System.Globalization;
using ControTemperaturaSitioWeb.Models;

namespace ControTemperaturaSitioWeb.Controllers
{
    public class TrabajadorController : Controller
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();
        CustomPrincipal User;
        // GET: Trabajador
        public ActionResult Index()
        {
            CustomAuthorizeAttribute x=new CustomAuthorizeAttribute();
           
              User = x.CurrentUser;
            List<Employee> employees = db.Employees.Where(u => u.LandId == User.LandId).Include(e => e.TypeEmployee).Include(e => e.Contractor).Include(e => e.Foreman).Include(e => e.Land).OrderBy(l => l.Apellido).ToList();

            List<Employee> employeesAux = new List<Employee>();

            foreach (var item in employees)
            {
                item.Foreman = db.Employees.FirstOrDefault(emp => emp.Id == item.EmployeeId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
             item.Img = "";
                  if (item.Foreman != null) item.Foreman.Img = "";
                if (item.Foreman == null) item.Foreman = new Employee();
            
                item.TypeEmployee = db.TypeEmployee.FirstOrDefault(U => U.Id == item.TypeEmployeeId);
                if (item.TypeEmployee == null) item.TypeEmployee = new TypeEmployee();
                item.Contractor = db.Contractors.FirstOrDefault(U => U.Id == item.ContractorId);
                if (item.Contractor == null) item.Contractor = new Contractor();
                item.Img = db.FeaturedEvents.FirstOrDefault(u => u.Employee.Documento == item.Documento && u.type.Contains("Nuevo"))?.IngressDateTime.ToString();            }
            return View(employees);
        }

        // GET: Trabajador/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ///
                 CustomPrincipal User;
                CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();
                User = x.CurrentUser;
                var LandId = User.LandId;
                DateTime dtDesde = DateTime.Now.AddYears(-1);
                DateTime dtHasta = DateTime.Now;
                ViewBag.Desde = dtDesde.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                ViewBag.Hasta = dtHasta.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                List<EgresosFromView> xa = db.Database.SqlQuery<EgresosFromView>("SELECT  'Egreso' as 'Accion', [egressDateTime] ,[EmployeeId],[retiredbefore],constratista_Id ,[descriptionout],[groupNumber],[TakenFromForeman] ,[TypeEmployeeId] ,[LandId] ,[UserId] ,[ForemanId]  FROM[Temperatura].[dbo].[VW_EGRESOS] where LandId=" + LandId + " and egressDateTime>'" + dtDesde.ToString("yyyy-MM-dd") + "' and egressDateTime<'" + dtHasta.AddDays(1).ToString("yyyy-MM-dd") + "' and EmployeeId=" + id+" union select 'Ingreso' as 'Accion', [IngressDateTime] as  'egressDateTime' ,[EmployeeId],[RetiredBefore],[ContractorId] as constratista_Id, [descriptionout],[groupNumber],0 ,[TypeEmployeeId] ,[LandId] ,[UserIdIngressRegister]  as 'UserId',[ForemanId]   FROM [Temperatura].[dbo].[IngressEgresses] where  LandId=" + LandId + " and IngressDateTime>'" + dtDesde.ToString("yyyy-MM-dd") + "' and IngressDateTime<'" + dtHasta.AddDays(1).ToString("yyyy-MM-dd") + "' and isDeleted=0 and EmployeeId="+id).ToList();

                foreach (var item in xa)
                {
                    item.Employee = db.Employees.FirstOrDefault(emp => emp.Id == item.EmployeeId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
                    if (item.Employee != null) item.Employee.Img = "";
                    item.Foreman = db.Employees.FirstOrDefault(emp => emp.Id == item.ForemanId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
                    if (item.Foreman != null) item.Foreman.Img = "";
                    if (item.Foreman == null) item.Foreman = new Employee();
                    item.User = db.Users.FirstOrDefault(U => U.Id == item.UserId);
                    item.TypeEmployee = db.TypeEmployee.FirstOrDefault(U => U.Id == item.TypeEmployeeId);
                    if (item.TypeEmployee == null) item.TypeEmployee = new TypeEmployee();
                    item.Contractor = db.Contractors.FirstOrDefault(U => U.Id == item.contractorista_Id);
                    if (item.Contractor == null) item.Contractor = new Contractor();
                }

                ViewBag.Ingresos = xa;
    
            //
            return View(employee);
        }

        // GET: Trabajador/Create
        public ActionResult Create()
        {
            CustomAuthorizeAttribute xo = new CustomAuthorizeAttribute();
         
            User = xo.CurrentUser;
            ViewBag.TypeEmployeeId = new SelectList(db.TypeEmployee.Where(o => o.LandId == User.LandId).OrderBy(l => l.Description), "Id", "Description");
            ViewBag.ContractorId = new SelectList(db.Contractors.Where(x => x.IsEnabled && x.LandId==User.LandId).OrderBy(l => l.Description), "Id", "Name") ;
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(x => x.isforeman && x.isActive && x.LandId == User.LandId).OrderBy(l => l.Apellido), "Id", "Nombre");
          //  ViewBag.LandId = new SelectList(db.Lands, "Id", "Name");
            return View();
        }

        // POST: Trabajador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Documento,LandId,Sex,Local,TypeEmployeeId,isforeman,EmployeeId,ContractorId,isActive")] Employee employee)
        {

            ViewBag.TypeEmployeeId = new SelectList(db.TypeEmployee.Where(o => o.LandId == User.LandId).OrderBy(l => l.Description), "Id", "Description", employee.TypeEmployeeId);
            ViewBag.ContractorId = new SelectList(db.Contractors.Where(x => x.IsEnabled).OrderBy(l => l.Description), "Id", "Name", employee.ContractorId);
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(x => x.isforeman && x.isActive && x.LandId == User.LandId).OrderBy(l => l.Apellido), "Id", "Nombre", employee.EmployeeId);

            if (ModelState.IsValid && employee.Documento!=null && !string.IsNullOrEmpty(employee.Apellido))
            {
                CustomAuthorizeAttribute xo = new CustomAuthorizeAttribute();

                User = xo.CurrentUser;
                employee.LandId = User.LandId;
                employee.birthDate = DateTime.Now;
                employee.lastUpdate = DateTime.Now;
                db.Employees.Add(employee);
                ViewBag.CreadoConExito = "Si";
                ViewBag.DatosEmployee = "Trabajador creado: " + employee.Apellido + " con Documento: " + employee.Documento;
                db.SaveChanges();
                //   ViewBag.CreadoConExito = true;
                employee = new Employee();
                return View(employee);
            }
        
            // ViewBag.LandId = new SelectList(db.Lands, "Id", "Name", employee.LandId);
            return View(employee);
        }

        // GET: Trabajador/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            CustomAuthorizeAttribute xo = new CustomAuthorizeAttribute();

            User = xo.CurrentUser;
            ViewBag.TypeEmployeeId = new SelectList(db.TypeEmployee.Where(x=>x.LandId==User.LandId).OrderBy(l => l.Description), "Id", "Description", employee.TypeEmployeeId);
            ViewBag.ContractorId = new SelectList(db.Contractors.Where(x => x.IsEnabled && x.LandId == User.LandId).OrderBy(l => l.Description), "Id", "Name", employee.ContractorId);
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(x => x.isforeman && x.isActive ).OrderBy(l => l.Apellido), "Id", "Nombre", employee.EmployeeId);
            ViewBag.EmployeeIdKey = employee.EmployeeId;
            ViewBag.ContractorIdKey = employee.ContractorId;
           // ViewBag.LandId = new SelectList(db.Lands, "Id", "Name", employee.LandId);
            return View(employee);
        }

        // POST: Trabajador/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {

                var emps = db.Employees.Find(employee.Id);
                emps.isActive = employee.isActive;
                emps.isforeman = employee.isforeman;
                emps.TypeEmployee = employee.TypeEmployee;
                emps.TypeEmployeeId = employee.TypeEmployeeId;
                if(emps.QR != employee.QR)
                {
                    QR newQR = new QR
                    {
                        Codigo = employee.QR,
                        RegistrationDate = DateTime.Now,
                    };
                    emps.QRHistory.Add(newQR);
                    emps.QR = employee.QR;
                }
                emps.Nombre = employee.Nombre;
                emps.Apellido = employee.Apellido;
                emps.Documento = employee.Documento;
                emps.Contractor = employee.Contractor;
                emps.Sex = employee.Sex;
                emps.Local = employee.Local;
                emps.ContractorId = employee.ContractorId;
                emps.EmployeeId=employee.EmployeeId;
                emps.Foreman = employee.Foreman;
                emps.groupNumber = employee.groupNumber;
                emps.lastUpdate = DateTime.Now;
                db.Entry(emps).State = EntityState.Modified; 
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            CustomAuthorizeAttribute xo = new CustomAuthorizeAttribute();

            User = xo.CurrentUser;
            ViewBag.TypeEmployeeId = new SelectList(db.TypeEmployee.Where(o => o.LandId == User.LandId).OrderBy(l => l.Description), "Id", "Description", employee.TypeEmployeeId);
            ViewBag.ContractorId = new SelectList(db.Contractors.Where(x=>x.IsEnabled).OrderBy(l => l.Description), "Id", "Name", employee.ContractorId);
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(x => x.isforeman && x.isActive && x.LandId == User.LandId).OrderBy(l => l.Apellido), "Id", "Nombre", employee.EmployeeId);
           // ViewBag.LandId = new SelectList(db.Lands, "Id", "Name", employee.LandId);
            return View(employee);
        }

        public JsonResult deshabilitar(List<string> listaEmpleados)
        {
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

            User = x.CurrentUser;
            // u => u.LandId == User.LandId
            foreach (var documento in listaEmpleados)
            {
                db.Employees.First(u => u.LandId == User.LandId && documento == u.Documento).isActive = false;

                db.Employees.First(u => u.LandId == User.LandId && documento == u.Documento).lastUpdate = DateTime.Now;
            }
            db.SaveChanges();

            return Json(new { Data = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult habilitar(List<string> listaEmpleados)
        {
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

            User = x.CurrentUser;
            // u => u.LandId == User.LandId
            foreach (var documento in listaEmpleados)
            {
                db.Employees.First(u => u.LandId == User.LandId && documento == u.Documento).isActive = true;
                db.Employees.First(u => u.LandId == User.LandId && documento == u.Documento).lastUpdate = DateTime.Now; 
            }
            db.SaveChanges();
            return Json(new { Data = true }, JsonRequestBehavior.AllowGet);
        }
        
        // GET: Trabajador/Delete/5
        [HttpPost]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Trabajador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
