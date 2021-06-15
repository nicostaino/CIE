using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccessoADatos;
using Clases.Models;
using ControlTemperaturaSitioWeb.CustomAuthentication;
using ControTemperaturaSitioWeb.Models;

namespace ControTemperaturaSitioWeb.Controllers
{
    public class NovedadesController : Controller
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();

        // GET: Novedades
        public ActionResult Index()
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

            User = x.CurrentUser;
            var LandId = User.LandId;
            DateTime dtDesde = DateTime.Now.AddMonths(-1);
            DateTime dtHasta = DateTime.Now;
            ViewBag.Desde = dtDesde.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.Hasta = dtHasta.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.Oficios = "Checked";
            ViewBag.Contratista = "Checked";
            ViewBag.Capataz = "Checked";
            //.Include(f => f.CurrentTypeEmployee).
            var apa = db.FeaturedEvents
                                .Include(f => f.CurrentContractor)
                                .Include(f => f.LastContractor)
                                .Include(f => f.LastForeman)
                                .Include(f => f.LastTypeEmployee)
                                .Include(f => f.UserRegister)
                                .Where(l => l.Employee.LandId == LandId 
                                            && l.IngressDateTime != null && l.IngressDateTime > dtDesde && l.IngressDateTime < dtHasta
                                            && l.Employee != null)
                                .ToList();
            //var featuredEvents = db.FeaturedEvents.Include(f => f.CurrentContractor).Include(f => f.CurrentTypeEmployee).Include(f => f.Employee).Include(f => f.LastContractor).Include(f => f.LastForeman).Include(f => f.LastTypeEmployee).Include(f => f.UserRegister).Where(l => l.Employee.LandId == LandId && l.IngressDateTime != null && l.IngressDateTime > dtDesde && l.IngressDateTime < dtHasta).ToList();
            foreach (var item in apa)
            {
             }
            return View(apa.ToList());
        }

        // GET: Novedades/Details/5 Oficios  

        public ActionResult Index2(string desde, string hasta, bool Oficios, bool Contratista, bool Capataz)
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();
            User = x.CurrentUser;
            var LandId = User.LandId;
            DateTime dtDesde = ConverterDateTime.converteStringTodate(desde);
            DateTime dtHasta = ConverterDateTime.converteStringTodate(hasta);
            ViewBag.Oficios = Oficios ? "Checked" : "";
            ViewBag.Contratista = Contratista ? "Checked" : "";
            ViewBag.Capataz = Capataz ? "Checked" : "";
            ViewBag.Desde = dtDesde.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.Hasta = dtHasta.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); 
            var featuredEvents = db.FeaturedEvents.Include(f => f.CurrentContractor).Include(f => f.LastContractor).Include(f => f.LastForeman).Include(f => f.LastTypeEmployee).Include(f => f.UserRegister).Where(l => l.Employee.LandId == LandId && l.IngressDateTime != null && l.IngressDateTime > dtDesde && l.IngressDateTime < dtHasta); 

            //.Include(f => f.CurrentForeman)
            if (!Capataz)
                featuredEvents = featuredEvents.Where(z => z.CurrentContractorId != z.LastContractorId);
            if (!Contratista)
                featuredEvents = featuredEvents.Where(l => l.LastForemanId != l.LastForemanId);
            if (!Oficios)
                featuredEvents = featuredEvents.Where(l => l.LastTypeEmployeeId != l.CurrentTypeEmployeeId);
            return View("Index", featuredEvents.ToList());
        }

        // GET: Novedades/Create
        //public ActionResult Create()
        //{
        //    ViewBag.CurrentContractorId = new SelectList(db.Contractors, "Id", "Name");
        //    ViewBag.IdCurrentForeman = new SelectList(db.Employees, "Id", "Nombre");
        //    ViewBag.CurrentTypeEmployeeId = new SelectList(db.TypeEmployee, "Id", "Description");
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Nombre");
        //    ViewBag.LastContractorId = new SelectList(db.Contractors, "Id", "Name");
        //    ViewBag.LastForemanId = new SelectList(db.Employees, "Id", "Nombre");
        //    ViewBag.LastTypeEmployeeId = new SelectList(db.TypeEmployee, "Id", "Description");
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
        //    return View();
        //}

        // POST: Novedades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,type,Description,EmployeeId,UserId,LastForemanId,IdCurrentForeman,LastContractorId,CurrentContractorId,LastTypeEmployeeId,CurrentTypeEmployeeId,IngressDateTime")] FeaturedEvent featuredEvent)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.FeaturedEvents.Add(featuredEvent);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CurrentContractorId = new SelectList(db.Contractors, "Id", "Name", featuredEvent.CurrentContractorId);
        //    ViewBag.IdCurrentForeman = new SelectList(db.Employees, "Id", "Nombre", featuredEvent.IdCurrentForeman);
        //    ViewBag.CurrentTypeEmployeeId = new SelectList(db.TypeEmployee, "Id", "Description", featuredEvent.CurrentTypeEmployeeId);
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Nombre", featuredEvent.EmployeeId);
        //    ViewBag.LastContractorId = new SelectList(db.Contractors, "Id", "Name", featuredEvent.LastContractorId);
        //    ViewBag.LastForemanId = new SelectList(db.Employees, "Id", "Nombre", featuredEvent.LastForemanId);
        //    ViewBag.LastTypeEmployeeId = new SelectList(db.TypeEmployee, "Id", "Description", featuredEvent.LastTypeEmployeeId);
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Name", featuredEvent.UserId);
        //    return View(featuredEvent);
        //}

        //// GET: Novedades/Edit/5
        //public ActionResult Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FeaturedEvent featuredEvent = db.FeaturedEvents.Find(id);
        //    if (featuredEvent == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CurrentContractorId = new SelectList(db.Contractors, "Id", "Name", featuredEvent.CurrentContractorId);
        //    ViewBag.IdCurrentForeman = new SelectList(db.Employees, "Id", "Nombre", featuredEvent.IdCurrentForeman);
        //    ViewBag.CurrentTypeEmployeeId = new SelectList(db.TypeEmployee, "Id", "Description", featuredEvent.CurrentTypeEmployeeId);
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Nombre", featuredEvent.EmployeeId);
        //    ViewBag.LastContractorId = new SelectList(db.Contractors, "Id", "Name", featuredEvent.LastContractorId);
        //    ViewBag.LastForemanId = new SelectList(db.Employees, "Id", "Nombre", featuredEvent.LastForemanId);
        //    ViewBag.LastTypeEmployeeId = new SelectList(db.TypeEmployee, "Id", "Description", featuredEvent.LastTypeEmployeeId);
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Name", featuredEvent.UserId);
        //    return View(featuredEvent);
        //}

        //// POST: Novedades/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,type,Description,EmployeeId,UserId,LastForemanId,IdCurrentForeman,LastContractorId,CurrentContractorId,LastTypeEmployeeId,CurrentTypeEmployeeId,IngressDateTime")] FeaturedEvent featuredEvent)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(featuredEvent).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CurrentContractorId = new SelectList(db.Contractors, "Id", "Name", featuredEvent.CurrentContractorId);
        //    ViewBag.IdCurrentForeman = new SelectList(db.Employees, "Id", "Nombre", featuredEvent.IdCurrentForeman);
        //    ViewBag.CurrentTypeEmployeeId = new SelectList(db.TypeEmployee, "Id", "Description", featuredEvent.CurrentTypeEmployeeId);
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Nombre", featuredEvent.EmployeeId);
        //    ViewBag.LastContractorId = new SelectList(db.Contractors, "Id", "Name", featuredEvent.LastContractorId);
        //    ViewBag.LastForemanId = new SelectList(db.Employees, "Id", "Nombre", featuredEvent.LastForemanId);
        //    ViewBag.LastTypeEmployeeId = new SelectList(db.TypeEmployee, "Id", "Description", featuredEvent.LastTypeEmployeeId);
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Name", featuredEvent.UserId);
        //    return View(featuredEvent);
        //}

        //// GET: Novedades/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FeaturedEvent featuredEvent = db.FeaturedEvents.Find(id);
        //    if (featuredEvent == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(featuredEvent);
        //}

        //// POST: Novedades/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    FeaturedEvent featuredEvent = db.FeaturedEvents.Find(id);
        //    db.FeaturedEvents.Remove(featuredEvent);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
