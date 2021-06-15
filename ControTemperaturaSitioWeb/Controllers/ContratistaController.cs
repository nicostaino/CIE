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
using ControTemperaturaSitioWeb.Models;

namespace ControTemperaturaSitioWeb.Controllers
{
    [CustomAuthorize(Roles = "Contratista")]
    public class ContratistaController : Controller
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();

        // GET: Contratista
        public ActionResult Index()
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();
   
            User = x.CurrentUser;
                      
            return View(db.Contractors.Where(o=>o.LandId==User.LandId).ToList());
        }

        // GET: Contratista/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // GET: Contratista/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contratista/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,IsEnabled")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
             

                CustomPrincipal User;
                CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

                User = x.CurrentUser;
                contractor.LandId = User.LandId;
                db.Contractors.Add(contractor);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id=contractor.Id});
            }

            return View(contractor);
        }

        // GET: Contratista/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            //   ViewBag.Roles = db.Database.SqlQuery<RoleUsers>("select *, CASE     WHEN Role_Id is not null THEN 'checked'     ELSE '' END AS estado from Roles r left join(select * from    RoleUsers where     User_Id=" + id + " ) ru on r.Id=ru.Role_Id").ToList();
            ViewBag.Oficios = db.Database.SqlQuery<OficioContrato>("select *, CASE WHEN TypeEmployee_Id is not null THEN 'checked'     ELSE '' END AS estado from TypeEmployees r left join(select* from    ContractTypeEmployees where   Contract_Id = "+id+") ru on r.Id = ru.TypeEmployee_Id where LandId ="+contractor.LandId).ToList();



            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // POST: Contratista/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,IsEnabled")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {

                CustomPrincipal User;
                CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

                User = x.CurrentUser;
                contractor.LandId = User.LandId; 
                db.Entry(contractor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contractor);
        }

        // GET: Contratista/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // POST: Contratista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Contractor contractor = db.Contractors.Find(id);
            db.Contractors.Remove(contractor);
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
