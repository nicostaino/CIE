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

namespace ControTemperaturaSitioWeb.Controllers
{
    public class IngressEgressesTestController : Controller
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();

        // GET: IngressEgressesTest
        public ActionResult Index()
        {
            var ingressEgress = db.IngressEgress.Include(i => i.Contractor).Include(i => i.Employee).Include(i => i.Foreman).Include(i => i.Land).Include(i => i.TypeEmployee).Include(i => i.UserEgressRegister).Include(i => i.UserIngressRegister);
            return View(ingressEgress.ToList());
        }

        // GET: IngressEgressesTest/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngressEgress ingressEgress = db.IngressEgress.Find(id);
            if (ingressEgress == null)
            {
                return HttpNotFound();
            }
            return View(ingressEgress);
        }

        // GET: IngressEgressesTest/Create
        public ActionResult Create()
        {
            ViewBag.ContractorId = new SelectList(db.Contractors, "Id", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Nombre");
            ViewBag.ForemanId = new SelectList(db.Employees, "Id", "Nombre");
            ViewBag.LandId = new SelectList(db.Lands, "Id", "Name");
            ViewBag.TypeEmployeeId = new SelectList(db.TypeEmployee, "Id", "Description");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            ViewBag.UserIdIngressRegister = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: IngressEgressesTest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RetiredBefore,DescriptionOut,IngressDateTime,EgressDateTime,DocumentEmployee,EmployeeId,DocumentForeman,ForemanId,LandId,UserId,UserIdIngressRegister,IsSyncToVisma,ContractorId,TypeEmployeeId,groupNumber,isForeman")] IngressEgress ingressEgress)
        {
            if (ModelState.IsValid)
            {
                db.IngressEgress.Add(ingressEgress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractorId = new SelectList(db.Contractors, "Id", "Name", ingressEgress.ContractorId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Nombre", ingressEgress.EmployeeId);
            ViewBag.ForemanId = new SelectList(db.Employees, "Id", "Nombre", ingressEgress.ForemanId);
            ViewBag.LandId = new SelectList(db.Lands, "Id", "Name", ingressEgress.LandId);
            ViewBag.TypeEmployeeId = new SelectList(db.TypeEmployee, "Id", "Description", ingressEgress.TypeEmployeeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", ingressEgress.UserId);
            ViewBag.UserIdIngressRegister = new SelectList(db.Users, "Id", "Name", ingressEgress.UserIdIngressRegister);
            return View(ingressEgress);
        }

        // GET: IngressEgressesTest/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngressEgress ingressEgress = db.IngressEgress.Find(id);
            if (ingressEgress == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractorId = new SelectList(db.Contractors, "Id", "Name", ingressEgress.ContractorId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Nombre", ingressEgress.EmployeeId);
            ViewBag.ForemanId = new SelectList(db.Employees, "Id", "Nombre", ingressEgress.ForemanId);
            ViewBag.LandId = new SelectList(db.Lands, "Id", "Name", ingressEgress.LandId);
            ViewBag.TypeEmployeeId = new SelectList(db.TypeEmployee, "Id", "Description", ingressEgress.TypeEmployeeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", ingressEgress.UserId);
            ViewBag.UserIdIngressRegister = new SelectList(db.Users, "Id", "Name", ingressEgress.UserIdIngressRegister);
            return View(ingressEgress);
        }

        // POST: IngressEgressesTest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RetiredBefore,DescriptionOut,IngressDateTime,EgressDateTime,DocumentEmployee,EmployeeId,DocumentForeman,ForemanId,LandId,UserId,UserIdIngressRegister,IsSyncToVisma,ContractorId,TypeEmployeeId,groupNumber,isForeman")] IngressEgress ingressEgress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingressEgress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractorId = new SelectList(db.Contractors, "Id", "Name", ingressEgress.ContractorId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Nombre", ingressEgress.EmployeeId);
            ViewBag.ForemanId = new SelectList(db.Employees, "Id", "Nombre", ingressEgress.ForemanId);
            ViewBag.LandId = new SelectList(db.Lands, "Id", "Name", ingressEgress.LandId);
            ViewBag.TypeEmployeeId = new SelectList(db.TypeEmployee, "Id", "Description", ingressEgress.TypeEmployeeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", ingressEgress.UserId);
            ViewBag.UserIdIngressRegister = new SelectList(db.Users, "Id", "Name", ingressEgress.UserIdIngressRegister);
            return View(ingressEgress);
        }

        // GET: IngressEgressesTest/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngressEgress ingressEgress = db.IngressEgress.Find(id);
            if (ingressEgress == null)
            {
                return HttpNotFound();
            }
            return View(ingressEgress);
        }

        // POST: IngressEgressesTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            IngressEgress ingressEgress = db.IngressEgress.Find(id);
            db.IngressEgress.Remove(ingressEgress);
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
