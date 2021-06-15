using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccessoADatos;
using Clases;
using ControlTemperaturaSitioWeb.CustomAuthentication;
using ControTemperaturaSitioWeb.Models;
using log4net;

namespace ControTemperaturaSitioWeb.Controllers
{
    [CustomAuthorize(Roles = "Usuarios")]
    public class UsuariosController : Controller
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();
        


        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            //ViewBag.SelectionIds = new MultiSelectView(db.Roles, "Value", "Name", selectedValues);

            User user = new User();

            return View(user);
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();
            User = x.CurrentUser;
            var LandId = User.LandId;

            if (ModelState.IsValid)
            {
                Land Tierra = db.Lands.Find(LandId);

                user.Land.Add(Tierra);
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            } 
            ViewBag.Roles=  db.Database.SqlQuery<RoleUsers>("select *, CASE     WHEN Role_Id is not null THEN 'checked'     ELSE '' END AS estado from Roles r left join(select * from    RoleUsers where     User_Id=" + id+" ) ru on r.Id=ru.Role_Id").ToList();

            return View(user);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Mail,Password,Username,FirstName,LastName,IsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        public JsonResult addRol(int Rol, int User, bool agregar)
        {
            if (agregar)
            {
                db.Users.Find(User).Roles.Add(db.Roles.Find(Rol));
            }
            else
            {
                db.Users.Find(User).Roles.Remove(db.Roles.Find(Rol));

            }
            db.SaveChanges();
            return Json(new { Data = true }, JsonRequestBehavior.AllowGet);

         ;
        }
        // GET: Usuarios/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

        [AllowAnonymous]
        [HttpGet]
        public ActionResult IsUserNameAvailable(int? id, string username)
        {
            return Json(!db.Users.Any(c => c.Username.Equals(username) && id == null), JsonRequestBehavior.AllowGet);

        }
    }
}
