using AccessoADatos;
using Clases;
using Clases.Models;
using ControlTemperaturaSitioWeb.CustomAuthentication;
using Servicios.Services;
using Servicios.Services.EmployeeSer;
using Servicios.Services.TemperatureSer;
using Servicios.Services.User;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlTemperaturaSitioWeb.Controllers
{

    public class HomeController : Controller
    {

        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();
        private IUserService _userService;
        private ITemperatureService _temperaturaService;
         
        private ILandService _landService;
        public HomeController(IUserService userService, ITemperatureService temperaturaService , IEmployeeService employeeService, ILandService landService)
        {
            _userService = userService;
            _temperaturaService = temperaturaService; 
            _landService = landService;

        }
      
        public ActionResult Index()
        {

            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated) {  

                return RedirectToAction("Login", "Account");
            }
            ViewBag.Title = "Home Page";
            var x = _temperaturaService.GetAll();
            var xa = _landService.GetAll();
           

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

            var usuario = _userService.GetAll().FirstOrDefault(x => x.Password == password && x.Mail == correo);
            return usuario == null ? 0 : usuario.Id;
        }
        public JsonResult obtenerCamposDelUsuario(long user)
        {

            var usuario = _userService.GetById(user);
            List<Land> campos = new List<Land>();
            campos = usuario.Land.ToList<Land>();
            var data = campos.Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
            }).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult EmpleadosPorCampo(long IdCampo)
        {

            var Campo = _landService.GetById(IdCampo);
            List<Employee> employees = new List<Employee>();

           var listaEmp= db.Employees.Where(z => z.LandId == IdCampo).Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre,p.Apellido, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory }).ToList();
           
            var data = listaEmp.Select(p => new
            {
                Id = p.Id,
                Name = p.Apellido +", "+p.Nombre + " - " + p.Documento,
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

            var temperatura = _temperaturaService.GetAll().Where(x => x.LandId == IdCampo);
            if (IdTrabajador != null)
                temperatura = temperatura.Where(x => x.EmployeeId == IdTrabajador);
            if (desde != "")
            {
                DateTime dateDesde = DateTime.ParseExact(desde, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                temperatura = temperatura.Where(x => x.Date >= dateDesde);

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
                UsuarioTomador = p.User.Name

            }).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}