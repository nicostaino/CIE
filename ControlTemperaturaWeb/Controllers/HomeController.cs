using AccessoADatos;
using Clases;
using Clases.Models;
using ControlTemperaturaWeb.CustomAuthentication;
using Servicios.Services.User;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
namespace ControlTemperaturaWeb.Controllers
{
    public class HomeController : Controller
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();
        private IUserService _userService;
        public HomeController(IUserService userService) {
            _userService = userService;

        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var x = _userService.GetAll();
            return View();
        }
        [CustomAuthorize(Roles = "ControlTemperaturas")]
        public ActionResult ControlTemperaturaIframe()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        [CustomAuthorize(Roles = "ControlTemperaturas")]
        public ActionResult ControlTemperatura()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public long GetuserForLogin(string correo, string password)
        {

            var usuario = db.Users.FirstOrDefault(x => x.Password == password && x.Mail == correo);
            return usuario==null?0:usuario.Id;
        }
        public JsonResult  obtenerCamposDelUsuario(long user)
        {

            var usuario = db.Users.Find(user);
            List<Land> campos = new List<Land>();
            campos = usuario.Land.ToList<Land>();
             var data    = campos.Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name, 
                }).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult EmpleadosPorCampo(long IdCampo)
        {

            var Campo = db.Lands.Find(IdCampo);
            List<Employee> employees = new List<Employee>();
            employees = Campo.Employee.ToList<Employee>();
            var data = employees.Select(p => new
            {
                Id = p.Id,
                Name = p.Nombre + " - "+ p.Documento,
            }).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult usuarioCampo()
        {

           var x = db.Database.SqlQuery<LandUser>("select * FROM UserLands");
            
            return Json(x, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Temperaturas(long IdCampo, long? IdTrabajador, string desde, string hasta)
        {

            var temperatura = db.Temperatures.Where(x => x.LandId == IdCampo);
            if(IdTrabajador!=null)
                temperatura = temperatura.Where(x => x.EmployeeId == IdTrabajador);
            if (desde != "")
            {
                DateTime dateDesde = DateTime.ParseExact(desde, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                temperatura = temperatura.Where(x => x.Date >= dateDesde) ;

            }
            if (hasta != "")
            {
                DateTime dateHasta = DateTime.ParseExact(hasta, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                temperatura = temperatura.Where(x => x.Date <= dateHasta);
            }
            var data = temperatura.Select(p => new
            {
                Id = p.Id,
                NombreTrabajador = p.Name,
                DNI = p.Identification,
                temperatura = p.Value,
                fecha = p.Date,
                Campo = p.Land.Name,
                Local = p.local,
                UsuarioTomador=p.User.Name
                
            }).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}
