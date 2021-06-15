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

namespace ControTemperaturaSitioWeb.Controllers
{
    public class TipoTrabajadorController : Controller
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();

        // GET: TipoTrabajador
        public ActionResult Index()
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

            User = x.CurrentUser;
            var LandId = User.LandId;
            return View(db.TypeEmployee.Where(x1=>x1.LandId==LandId).ToList().OrderBy(l => l.Description));
        }

        // GET: TipoTrabajador/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeEmployee typeEmployee = db.TypeEmployee.Find(id);
            if (typeEmployee == null)
            {
                return HttpNotFound();
            }
            return View(typeEmployee);
        }

        // GET: TipoTrabajador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoTrabajador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description")] TypeEmployee typeEmployee)
        {
            if (ModelState.IsValid)
            {
                CustomPrincipal User;
                CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

                User = x.CurrentUser;
                var LandId = User.LandId;
                typeEmployee.LandId = LandId;
                db.TypeEmployee.Add(typeEmployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeEmployee);
        }

        // GET: TipoTrabajador/Edit/5
        public ActionResult Edit(long? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeEmployee typeEmployee = db.TypeEmployee.Find(id);
            if (typeEmployee == null)
            {
                return HttpNotFound();
            }
            return View(typeEmployee);
        }

        // POST: TipoTrabajador/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description")] TypeEmployee typeEmployee)
        {
            if (ModelState.IsValid)
            {
                CustomPrincipal User;
                CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

                User = x.CurrentUser;
                var LandId = User.LandId;
                typeEmployee.LandId = LandId;
                db.Entry(typeEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeEmployee);
        }

        // GET: TipoTrabajador/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeEmployee typeEmployee = db.TypeEmployee.Find(id);
            if (typeEmployee == null)
            {
                return HttpNotFound();
            }
            return View(typeEmployee);
        }

        // POST: TipoTrabajador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TypeEmployee typeEmployee = db.TypeEmployee.Find(id);
            db.TypeEmployee.Remove(typeEmployee);
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
