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
    [CustomAuthorize(Roles = "ListaNegra")]
    public class ListaNegraController : Controller
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();

        // GET: ListaNegra
        public ActionResult Index()
        {
            var blackLists = db.BlackLists.Include(b => b.UserRegister);
            return View(blackLists.ToList());
        }

        // GET: ListaNegra/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlackList blackList = db.BlackLists.Find(id);
            if (blackList == null)
            {
                return HttpNotFound();
            }
            return View(blackList);
        }

        // GET: ListaNegra/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: ListaNegra/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Documento,Reason")] BlackList blackList)
        {
            if (ModelState.IsValid)
            {
                blackList.UserId = db.Users.FirstOrDefault(x=>x.Username == User.Identity.Name).Id;
                blackList.AdmissionDate = DateTime.Now;
                db.BlackLists.Add(blackList);
                db.SaveChanges();
                return RedirectToAction("Index"); 
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", blackList.UserId);
            return View(blackList);
        }

        // GET: ListaNegra/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlackList blackList = db.BlackLists.Find(id);
            if (blackList == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", blackList.UserId);
            return View(blackList);
        }

        // POST: ListaNegra/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Documento,AdmissionDate,Reason")] BlackList blackList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blackList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", blackList.UserId);
            return View(blackList);
        }

        // GET: ListaNegra/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlackList blackList = db.BlackLists.Find(id);
            if (blackList == null)
            {
                return HttpNotFound();
            }
            return View(blackList);
        }

        // POST: ListaNegra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BlackList blackList = db.BlackLists.Find(id);
            db.BlackLists.Remove(blackList);
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
