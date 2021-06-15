using AccessoADatos;
using ControlTemperaturaWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlTemperaturaWebApi.Controllers
{
    public class HomeController : Controller
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult UpdateEmployeesData(string lastUpdate, long landId, string Token)
        {
            if (Token.Equals("62468a24-e1dc-407c-975b-7b00e8775b8b"))
            {
                DateTime dtLastUpdate = ConverterDateTime.converteStringTodate(lastUpdate);
                dtLastUpdate.AddDays(-1);
                var listReturn = db.Employees.Where(p => p.lastUpdate > dtLastUpdate && p.LandId == landId).Select(p => new { p.Id, p.groupNumber, p.birthDate, p.ContractorId, p.Documento, p.EmployeeId, p.isActive, p.isforeman, p.LandId, p.Local, p.Nombre, p.Apellido, p.QR, p.Sex, p.TypeEmployeeId, p.ContractId }).ToList();
                return Json(listReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(new List<EmployeeObjAPi>(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult usuarioCampo()
        {
            var x = db.Database.SqlQuery<LandUser>("select * FROM LandUsers");

            return Json(x, JsonRequestBehavior.AllowGet);
        }
    }
}
