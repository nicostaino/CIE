using AccessoADatos;
using Clases.Models;
using ControlTemperaturaWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlTemperaturaWebApi.Controllers
{
    public class UserLandController : Controller
    {
        // GET: UserLand
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();

        public JsonResult usuarioCampo()
        {

            var x = db.Database.SqlQuery<LandUser>("SELECT *   FROM[Temperatura].[dbo].[UserLands]");

            return Json(x.ToList<LandUser>(), JsonRequestBehavior.AllowGet);

        }
        public JsonResult BlackList()
        {

            var x = db.Database.SqlQuery<BlackList>("SELECT *   FROM[Temperatura].[dbo].[BlackLists]");

            return Json(x.ToList<BlackList>(), JsonRequestBehavior.AllowGet);

        }
        public JsonResult oficioContratoCampo()
        {

            var x = db.Database.SqlQuery<ContractTypeEmployees>("SELECT  [Contract_Id] ,[TypeEmployee_Id]  FROM[Temperatura].[dbo].[ContractTypeEmployees]");

            return Json(x.ToList<ContractTypeEmployees>(), JsonRequestBehavior.AllowGet);

        }
         

    }
}