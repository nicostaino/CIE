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
    public class AdminQRsController : Controller
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();

        // GET: AdminQRs
        public ActionResult Index()
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

            User = x.CurrentUser;
            var LandId = User.LandId;
            return View(db.AdminQRs.Where(Z => LandId == Z.LandId).ToList());
        }

        // GET: AdminQRs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminQR adminQR = db.AdminQRs.Find(id);
            if (adminQR == null)
            {
                return HttpNotFound();
            }
            return View(adminQR);
        }

        // GET: AdminQRs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminQRs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(  AdminQR adminQR)
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

            User = x.CurrentUser;
            var LandId = User.LandId;
            adminQR.LandId = LandId;
            db.AdminQRs.Add(adminQR);
            db.SaveChanges();
            return View("Index",  db.AdminQRs.Where(Z=>LandId==Z.LandId).ToList());
        }

        // GET: AdminQRs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminQR adminQR = db.AdminQRs.Find(id);
            if (adminQR == null)
            {
                return HttpNotFound();
            }
            return View(adminQR);
        }

        // POST: AdminQRs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,QR,Active")] AdminQR adminQR)
        {
            if (ModelState.IsValid)
            {
                CustomPrincipal User;
                CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

                User = x.CurrentUser;
                var LandId = User.LandId;
                //adminQR.Land = db.Lands.Find(LandId);
                adminQR.LandId = LandId;
                db.Entry(adminQR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminQR);
        }

        // GET: AdminQRs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminQR adminQR = db.AdminQRs.Find(id);
            if (adminQR == null)
            {
                return HttpNotFound();
            }
            return View(adminQR);
        }

        // POST: AdminQRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AdminQR adminQR = db.AdminQRs.Find(id);
            db.AdminQRs.Remove(adminQR);
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
