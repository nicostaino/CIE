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
    public class ContratoController : Controller
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();
        CustomPrincipal User;

        // GET: Contrato
        public ActionResult Index()
        {
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();
            CustomPrincipal CurrentUser = x.CurrentUser;
            User = x.CurrentUser;

            var contracts = db.Contracts.Include(c => c.Contractor).Include(c => c.Land).Where(o => o.LandId == User.LandId).ToList();
 
            return View(contracts);
        }

        // GET: Contrato/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // GET: Contrato/Create
        public ActionResult Create()
        {
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();
            CustomPrincipal CurrentUser = x.CurrentUser;
            User = x.CurrentUser;

            ViewBag.ContractorId = new SelectList(db.Contractors.Where(c => c.LandId == User.LandId && c.IsEnabled), "Id", "Name");
            //  ViewBag.LandId = new SelectList(db.Lands, "Id", "Name");
            return View();
        }

        // POST: Contrato/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ContractorId,DateStart,DateEnd")] Contract contract)
        {
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();
            CustomPrincipal CurrentUser = x.CurrentUser;
            User = x.CurrentUser;

            if (ModelState.IsValid)
            {
                
                contract.LandId = User.LandId; 

                db.Contracts.Add(contract);
                db.SaveChanges();
            
                return RedirectToAction("Edit", new { id = contract.Id });
            }

            var contractors = db.Contractors.Where(c => c.LandId == User.LandId && c.IsEnabled);

            ViewBag.ContractorId = new SelectList(contractors, "Id", "Name", contract.ContractorId);
            //ViewBag.LandId = new SelectList(db.Lands, "Id", "Name", contract.LandId);
            return View(contract);
        }

        // GET: Contrato/Edit/5
        public ActionResult Edit(long? id)
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();
            User = x.CurrentUser;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }

            var contractors = db.Contractors.Where(c => c.LandId == User.LandId && c.IsEnabled);

            ViewBag.ContractorId = new SelectList(contractors, "Id", "Name", contract.ContractorId);
            //   ViewBag.Roles = db.Database.SqlQuery<RoleUsers>("select *, CASE     WHEN Role_Id is not null THEN 'checked'     ELSE '' END AS estado from Roles r left join(select * from    RoleUsers where     User_Id=" + id + " ) ru on r.Id=ru.Role_Id").ToList();
            ViewBag.Oficios = db.Database.SqlQuery<OficioContrato>("select *, CASE WHEN TypeEmployee_Id is not null THEN 'checked'     ELSE '' END AS estado from TypeEmployees r left join(select* from [ContractTypeEmployees] where   Contract_Id = " + id + ") ru on r.Id = ru.TypeEmployee_Id where LandId =" + contract.LandId).ToList();

            return View(contract);
        }

        // POST: Contrato/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ContractorId,DateStart,DateEnd")] Contract contract)
        {
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();
            CustomPrincipal CurrentUser = x.CurrentUser;
            User = x.CurrentUser;

            if (ModelState.IsValid)
            {
                contract.LandId = User.LandId;
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractorId = new SelectList(db.Contractors.Where(c => c.LandId == User.LandId && c.IsEnabled), "Id", "Name", contract.ContractorId);
            //ViewBag.LandId = new SelectList(db.Lands, "Id", "Name", contract.LandId);
            return View(contract);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult addOficio(int Oficio, int IdContrato, bool agregar)
        {
            if (agregar)
            {
                db.Contracts.Find(IdContrato).TypeEmployees.Add(db.TypeEmployee.Find(Oficio));
            }
            else
            {
                db.Contracts.Find(IdContrato).TypeEmployees.Remove(db.TypeEmployee.Find(Oficio));

            }
            db.SaveChanges();
            return Json(new { Data = true }, JsonRequestBehavior.AllowGet);

            ;
        }
    }
}
