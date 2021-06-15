using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AccessoADatos;
using Clases;
using Clases.Models;
using ControlTemperaturaSitioWeb.Controllers;
using ControlTemperaturaSitioWeb.CustomAuthentication;
using ControTemperaturaSitioWeb.Models;

namespace ControTemperaturaSitioWeb.Controllers
{
    public class IngresoEgresoController : Controller
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();
        CustomPrincipal User;

        // GET: IngresoEgreso
        public ActionResult Index()
        {
            using (var context = new AppIngresoTemperatrasContext())
            {
                CustomPrincipal User;
                CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

                //context.Configuration.LazyLoadingEnabled = false;

                User = x.CurrentUser;
                var LandId = User.LandId;

                var result = context.IngressEgress
                                    .Include(i => i.Employee)
                                    .Include(i => i.Land)
                                    .Include(i => i.UserEgressRegister)
                                    .Where(c => c.LandId == LandId && c.isDeleted != true && c.EmployeeId != null);

                //var ingressEgress = db.IngressEgress.Include(i => i.Land).Include(i => i.UserEgressRegister).Where(l => l.LandId == LandId && l.isDeleted == false);
                ViewBag.EnableABMIngresoEgreso = User.Roles.Contains("ABMIngresoEgreso");
                //return View(ingressEgress.ToList());
                return View(result.ToList());
            }
        }
        // GET: IngresoEgreso
        public ActionResult Ingresos()
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();
            User = x.CurrentUser;

            var LandId = User.LandId;
            DateTime dtDesde = DateTime.Now.AddMonths(-1);
            DateTime dtHasta = DateTime.Now;
            ViewBag.Desde = dtDesde.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.Hasta = dtHasta.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            List<IngressEgress> ingressEgress = db.IngressEgress
                                                    .Include(i => i.Land)
                                                    .Include(i => i.UserEgressRegister)
                                                    .Include(i => i.Employee)
                                                    .Include(i => i.Foreman)
                                                    .Where(l => l.LandId == LandId && l.EgressDateTime == null
                                                                && l.IngressDateTime > dtDesde && l.IngressDateTime < dtHasta
                                                                && l.isDeleted == false && l.EmployeeId != null)
                                                    .ToList();
            foreach (var item in ingressEgress)
            {
                item.Employee = db.Employees.FirstOrDefault(emp => emp.Id == item.EmployeeId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
                if (item.Employee != null) item.Employee.Img = "";
                item.Foreman = db.Employees.FirstOrDefault(emp => emp.Id == item.ForemanId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
                if (item.Foreman != null) item.Foreman.Img = "";

                item.TypeEmployee = db.TypeEmployee.FirstOrDefault(U => U.Id == item.TypeEmployeeId);

            }
            return View(ingressEgress);
        }
        public ActionResult Ingresos2(string desde, string hasta)
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

            User = x.CurrentUser;
            var LandId = User.LandId;
            DateTime dtDesde = ConverterDateTime.converteStringTodate(desde);
            DateTime dtHasta = ConverterDateTime.converteStringTodate(hasta);
            ViewBag.Desde = dtDesde.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.Hasta = dtHasta.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            var ingressEgress = db.IngressEgress
                                    .Include(i => i.Land)
                                    .Include(i => i.UserEgressRegister)
                                    .Where(l => l.LandId == LandId
                                            && l.EgressDateTime == null && l.IngressDateTime > dtDesde && l.IngressDateTime < dtHasta
                                            && l.isDeleted == false
                                            && l.EmployeeId != null);
            return View("Ingresos", ingressEgress.ToList());
        }
        // GET: IngresoEgreso/Details/5
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
        public ActionResult Egresso()
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();
            User = x.CurrentUser;
            var LandId = User.LandId;
            DateTime dtDesde = DateTime.Now.AddMonths(-1);
            DateTime dtHasta = DateTime.Now;
            ViewBag.Desde = dtDesde.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.Hasta = dtHasta.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<EgresosFromView> xa = db.Database.SqlQuery<EgresosFromView>("SELECT[egressDateTime],[EmployeeId],[retiredbefore], constratista_Id,[descriptionout],[groupNumber],[TakenFromForeman],[TypeEmployeeId],[LandId],[UserId],[ForemanId]  FROM[Temperatura].[dbo].[VW_EGRESOS] where LandId = " + LandId + " and egressDateTime > '" + dtDesde.ToString("yyyy-MM-dd") + "'  and egressDateTime < '" + dtHasta.AddDays(1).ToString("yyyy-MM-dd") + "' ").ToList();

            foreach (var item in xa)
            {
                item.Employee = db.Employees.FirstOrDefault(emp => emp.Id == item.EmployeeId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
                if (item.Employee != null) item.Employee.Img = "";
                item.Foreman = db.Employees.FirstOrDefault(emp => emp.Id == item.ForemanId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
                if (item.Foreman != null) item.Foreman.Img = "";
                item.User = db.Users.FirstOrDefault(U => U.Id == item.UserId);
                item.TypeEmployee = db.TypeEmployee.FirstOrDefault(U => U.Id == item.TypeEmployeeId);
            }

            ViewBag.Ingresos = xa;
            //   return Json(xa, JsonRequestBehavior.AllowGet);

            // var p=User.LandId;

            return View();

        }
        public FileStreamResult DownloadCorvusFile(string from, string to)
        {
            string fileName = "IngresosCorvuss" + DateTime.Now.Date.ToShortDateString() + ".txt";

            StringBuilder sb = new StringBuilder();
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

            User = x.CurrentUser;
            var LandId = User.LandId;
            var ingress = db.IngressEgress.Include(i => i.Land).Include(i => i.Employee).Include(i => i.UserEgressRegister).Where(l => l.LandId == LandId && l.EgressDateTime == null && l.isDeleted == false).OrderBy(u => u.IngressDateTime);
            List<EgresosFromView> egress = db.Database.SqlQuery<EgresosFromView>("SELECT  [egressDateTime] ,[EmployeeId],[retiredbefore],constratista_Id ,[descriptionout],[groupNumber],[TakenFromForeman] ,[TypeEmployeeId] ,[LandId] ,[UserId] ,[ForemanId]  FROM[Temperatura].[dbo].[VW_EGRESOS] where LandId=" + LandId + " and egressDateTime> '" + Convert.ToDateTime(from).ToString("yyyy-MM-dd") + "'  and egressDateTime< '" + Convert.ToDateTime(to).AddDays(1).ToString("yyyy-MM-dd") + "'").ToList();
            foreach (var item in ingress)
            {
                sb.AppendLine(item.IngressDateTime.Value.ToString("dd/MM/yyyy") + "," + item.IngressDateTime.Value.ToString("hh:mm:ss") + ",1,0,27," + item.Employee.QR + ",P,,0,99999999;0");
            }
            foreach (var e in egress)
            {
                sb.AppendLine(e.egressDateTime.Value.ToString("dd/MM/yyyy") + "," + e.egressDateTime.Value.ToString("hh:mm:ss") + ",1,0,27," + db.Employees.Where(emp => emp.Id == e.EmployeeId).FirstOrDefault().QR + ",P,,0,99999999;0");
            }

            var byteArray = Encoding.ASCII.GetBytes(sb.ToString());
            var stream = new MemoryStream(byteArray);

            return File(stream, "text/plain", fileName);

        }

        public FileStreamResult DownloadVismaFile(string from, string to)
        {

            //parche para solucionar problema con las fechas desde el server a la bd.
            string[] splitFrom = from.Split('/');
            string[] splitTo = to.Split('/');

            DateTime dateFrom = new DateTime(Int32.Parse(splitFrom[2]), Int32.Parse(splitFrom[1]), Int32.Parse(splitFrom[0]));
            DateTime dateTo = new DateTime(Int32.Parse(splitTo[2]), Int32.Parse(splitTo[1]), Int32.Parse(splitTo[0]));

            string fileName = "Visma" + DateTime.Now.Date.ToShortDateString() + ".txt";
            StringBuilder sb = new StringBuilder();
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

            User = x.CurrentUser;
            var LandId = User.LandId;
            var ingress = db.IngressEgress.Include(i => i.Land).Include(i => i.Employee).Include(i => i.UserEgressRegister).Where(l => l.LandId == LandId && l.IngressDateTime >= dateFrom && l.IngressDateTime <= dateTo && l.isDeleted == false).OrderBy(u => u.IngressDateTime);

            //List<EgresosFromView> egress = db.Database.SqlQuery<EgresosFromView>("SELECT  [egressDateTime] ,[EmployeeId],[retiredbefore],constratista_Id ,[descriptionout],[groupNumber],[TakenFromForeman] ,[TypeEmployeeId] ,[LandId] ,[UserId] ,[ForemanId]  FROM[Temperatura].[dbo].[VW_EGRESOS] where LandId=" + LandId + " and egressDateTime> '" + Convert.ToDateTime(from).ToString("yyyy-MM-dd") + "'  and egressDateTime< '" + Convert.ToDateTime(to).AddDays(1).ToString("yyyy-MM-dd") + "'").ToList();

            List<EgresosFromView> egress = db.Database.SqlQuery<EgresosFromView>("SELECT  [egressDateTime] ,[EmployeeId],[retiredbefore],constratista_Id ,[descriptionout],[groupNumber],[TakenFromForeman] ,[TypeEmployeeId] ,[LandId] ,[UserId] ,[ForemanId]  FROM [Temperatura].[dbo].[VW_EGRESOS] where LandId = " + LandId + " and egressDateTime > '" + dateFrom.ToString("yyyy-MM-dd") + "' and egressDateTime < '" + dateTo.AddDays(1).ToString("yyyy-MM-dd") + "'").ToList();

            //List<EgresosFromView> egress = db.Database.SqlQuery<EgresosFromView>("SELECT * FROM VW_EGRESOS where LandId = @land and egressDateTime >= @from and egressDateTime <= @to "
            //    , new SqlParameter("@from", dateFrom.ToString("yyyy-MM-dd"))
            //    , new SqlParameter("@to", dateTo.ToString("yyyy-MM-dd"))
            //    , new SqlParameter("@land", LandId)).ToList();

            //List<EgresosFromView> egress = new List<EgresosFromView>();

            string clock = ConfigurationManager.AppSettings[LandSwitch(Convert.ToInt32(LandId))];

            foreach (var item in ingress)
            {
                sb.AppendLine(item.DocumentEmployee + " " + item.IngressDateTime.Value.ToString("dd/MM/yyyy") + " " + item.IngressDateTime.Value.ToString("hh:mm") + " " + clock + " 20");
            }
            //si esto anda lento hay que agregar el documento del empleado al objeto inicial (egresosfromview) y traerlo una sola vez en la query.
            foreach (var e in egress)
            {
                if (e.EmployeeId != null)
                {
                    var employee = db.Employees.Where(emp => emp.Id == e.EmployeeId).FirstOrDefault();
                    sb.AppendLine(employee.Documento + " " + e.egressDateTime.Value.ToString("dd/MM/yyyy") + " " + e.egressDateTime.Value.ToString("hh:mm") + " " + clock + " 21");
                }
            }

            var byteArray = Encoding.ASCII.GetBytes(sb.ToString());
            var stream = new MemoryStream(byteArray);

            return File(stream, "text/plain", fileName);

        }
        public ActionResult Egresso2(string desde, string hasta)
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

            User = x.CurrentUser;
            var LandId = User.LandId;
            DateTime dtDesde = DateTime.Parse(desde);
            DateTime dtHasta = ConverterDateTime.converteStringTodate(hasta);


            ViewBag.Desde = dtDesde.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.Hasta = dtHasta.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<EgresosFromView> xa = db.Database.SqlQuery<EgresosFromView>("SELECT  [egressDateTime] ,[EmployeeId],[retiredbefore],constratista_Id ,[descriptionout],[groupNumber],[TakenFromForeman] ,[TypeEmployeeId] ,[LandId] ,[UserId] ,[ForemanId]  FROM[Temperatura].[dbo].[VW_EGRESOS] where LandId=" + LandId + " and egressDateTime> '" + dtDesde.ToString("yyyy-MM-dd") + "'  and egressDateTime< '" + dtHasta.AddDays(1).ToString("yyyy-MM-dd") + "'").ToList();

            foreach (var item in xa)
            {
                item.Employee = db.Employees.FirstOrDefault(emp => emp.Id == item.EmployeeId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
                if (item.Employee != null) item.Employee.Img = "";
                item.Foreman = db.Employees.FirstOrDefault(emp => emp.Id == item.ForemanId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
                if (item.Foreman != null) item.Foreman.Img = "";
                //item.User = db.Users.FirstOrDefault(U => U.Id == item.UserId);
                item.TypeEmployee = db.TypeEmployee.FirstOrDefault(U => U.Id == item.TypeEmployeeId);
            }

            ViewBag.Ingresos = xa;
            //   return Json(xa, JsonRequestBehavior.AllowGet);

            // var p=User.LandId;

            return View("Egresso");

        }

        public ActionResult EgressoIngresoUnificado()
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();
            User = x.CurrentUser;
            var LandId = User.LandId;
            //string[] desdeA = desde.Split('/');
            //string[] hastaA = hasta.Split('/');

            DateTime dtDesde = DateTime.Now;// (Int32.Parse(desdeA[2]), Int32.Parse(desdeA[1]), Int32.Parse(desdeA[0]));
            DateTime dtHasta = DateTime.Now.AddDays(1);// new DateTime(Int32.Parse(hastaA[2]), Int32.Parse(hastaA[1]), Int32.Parse(hastaA[0])); ;


            ViewBag.Desde = dtDesde.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.Hasta = dtHasta.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //List<EgresosFromView> xa = db.Database.SqlQuery<EgresosFromView>("SELECT  'Egreso' as 'Accion', [egressDateTime] ,[EmployeeId],[retiredbefore],constratista_Id ,[descriptionout],[groupNumber],[TakenFromForeman] ,[TypeEmployeeId] ,[LandId] ,[UserId] ,[ForemanId]  FROM[Temperatura].[dbo].[VW_EGRESOS] where  LandId=" + LandId + " and egressDateTime>'" + dtDesde.ToString("yyyy-MM-dd") + "'   and egressDateTime<'" + dtHasta.AddDays(1).ToString("yyyy-MM-dd") + "' union select 'Ingreso' as 'Accion', [IngressDateTime] as  'egressDateTime' ,[EmployeeId],[RetiredBefore],[ContractorId] as constratista_Id, [descriptionout],[groupNumber],0 ,[TypeEmployeeId] ,[LandId] ,[UserIdIngressRegister]  as 'UserId',[ForemanId]   FROM [Temperatura].[dbo].[IngressEgresses] where LandId=" + LandId + " and IngressDateTime>'" + dtDesde.ToString("yyyy-MM-dd") + "' and IngressDateTime<'" + dtHasta.AddDays(1).ToString("yyyy-MM-dd") + "' and isDeleted=0 ").ToList();

            //foreach (var item in xa)
            //{
            //    item.Employee = db.Employees.FirstOrDefault(emp => emp.Id == item.EmployeeId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
            //    if (item.Employee != null) item.Employee.Img = "";
            //    item.Foreman = db.Employees.FirstOrDefault(emp => emp.Id == item.ForemanId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
            //    if (item.Foreman != null) item.Foreman.Img = "";
            //    if (item.Foreman == null) item.Foreman = new Employee();
            //    item.User = db.Users.FirstOrDefault(U => U.Id == item.UserId);
            //    item.TypeEmployee = db.TypeEmployee.FirstOrDefault(U => U.Id == item.TypeEmployeeId);
            //    if (item.TypeEmployee == null) item.TypeEmployee = new TypeEmployee();
            //    item.Contractor = db.Contractors.FirstOrDefault(U => U.Id == item.contractorista_Id);
            //    if (item.Contractor == null) item.Contractor = new Contractor();
            //}

            ViewBag.Ingresos = null;
            return View("EgressoIngresoUnificado");
        }

        public ActionResult GetDataList(string desde, string hasta)
        {
            using (var context = new AppIngresoTemperatrasContext())
            {
                string draw = Request.Form["draw"].ToString();
                string order = Request.Form["order[0][column]"].ToString();
                string orderDir = Request.Form["order[0][dir]"];
                int startRec = int.Parse(Request.Form["start"].ToString());
                int pageSize = int.Parse(Request.Form["length"].ToString());
                string search = Request.Form["search[value]"].ToString();

                CustomPrincipal User;
                CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();
                User = x.CurrentUser;

                var LandId = User.LandId;
                string[] desdeA = desde.Split('/');
                string[] hastaA = hasta.Split('/');

                DateTime dtDesde = (!string.IsNullOrEmpty(desde)) ? DateTime.ParseExact(desde, "dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.Now; // (Int32.Parse(desdeA[2]), Int32.Parse(desdeA[1]), Int32.Parse(desdeA[0]));
                DateTime dtHasta = (!string.IsNullOrEmpty(hasta)) ? DateTime.ParseExact(hasta, "dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.Now.AddDays(1);// new DateTime(Int32.Parse(hastaA[2]), Int32.Parse(hastaA[1]), Int32.Parse(hastaA[0])); ;

                ViewBag.Desde = dtDesde.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                ViewBag.Hasta = dtHasta.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                //string sql = string.Format("select * from VW_UNIFICADOS where egressDateTime between '{0}' and '{1}' and landId = '{2}'", dtDesde.ToString("yyyyMMdd"), dtHasta.ToString("yyyyMMdd"), CurrentUser.LandId);
                string sql = string.Format("select * from VW_UNIFICADOS where egressDateTime between '{0}' and '{1}' and landId = '{2}'", dtDesde, dtHasta, User.LandId);

                List<EgresosView> employees = context.Database.SqlQuery<EgresosView>(sql).ToList();

                long TotalRecordsCount = employees.Count();

                #region filters  

                if (!string.IsNullOrEmpty(search))
                {
                    employees = employees.Where(c => (!string.IsNullOrEmpty(c.EmployeeName) && c.EmployeeName.ToLower().Contains(search.ToLower())) 
                                                    || (!string.IsNullOrEmpty(c.EmployeeLastName) && c.EmployeeLastName.ToLower().Contains(search.ToLower()))
                                                    || (!string.IsNullOrEmpty(c.Documento) && c.Documento.ToLower().Contains(search.ToLower())) 
                                                    || (!string.IsNullOrEmpty(c.Documento) && c.Documento.ToLower().Contains(search.ToLower()))
                                                ).ToList();
                }

                #endregion


                //count of record after filter   
                long FilteredRecordCount = employees.Count();


                /*Here we are allowing only one sorting at time. orderDir will hold asc or desc for sorting the column. */
                #region Sorting  
                // Sorting     
                switch (order)
                {
                    case "1":
                        employees = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            employees.OrderByDescending(p => p.EmployeeLastName).ToList() :
                            employees.OrderBy(p => p.EmployeeLastName).ToList();
                        break;
                    case "2":
                        employees = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            employees.OrderByDescending(p => p.Documento).ToList() :
                            employees.OrderBy(p => p.Documento).ToList();
                        break;
                    case "3":
                        employees = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            employees.OrderByDescending(p => p.Accion).ToList() :
                            employees.OrderBy(p => p.Accion).ToList();
                        break;
                    case "5":
                        employees = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            employees.OrderByDescending(p => p.Accion).ToList() :
                            employees.OrderBy(p => p.Accion).ToList();
                        break;
                    case "6":
                        employees = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            employees.OrderByDescending(p => p.descriptionout).ToList() :
                            employees.OrderBy(p => p.descriptionout).ToList();
                        break;
                    case "7":
                        employees = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                           employees.OrderByDescending(p => p.TypeEmployeeName).ToList() :
                           employees.OrderBy(p => p.TypeEmployeeName).ToList();
                        break;
                    case "8":
                        employees = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                           employees.OrderByDescending(p => p.ForemanName).ToList() :
                           employees.OrderBy(p => p.ForemanName).ToList();
                        break;
                    case "9":
                        employees = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                           employees.OrderByDescending(p => p.ContractorName).ToList() :
                           employees.OrderBy(p => p.ContractorName).ToList();
                        break;
                    default:
                        employees = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            employees.OrderByDescending(p => p.EmployeeName).ToList() :
                            employees.OrderBy(p => p.EmployeeName).ToList();
                        break;
                }

                #endregion
                /* Apply pagination to employee iqueryable, startRec will hold the record number from which we need to display and pageSize will hold the number of records to display. Then assign the values to EmployeeDetails model.  */

                var listemployee = employees.Skip(startRec).Take(pageSize).ToList();

                if (listemployee == null)
                    listemployee = new List<EgresosView>();


                return this.Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = TotalRecordsCount,
                    recordsFiltered = FilteredRecordCount,
                    data = listemployee
                }, JsonRequestBehavior.AllowGet);
                
            }
        }

        public ActionResult GetDataByCriteria(int employeeId, string desde, string hasta)
        {
            using (var context = new AppIngresoTemperatrasContext())
            {
                //string draw = Request.Form["draw"].ToString();
                //string order = Request.Form["order[0][column]"].ToString();
                //string orderDir = Request.Form["order[0][dir]"];
                //int startRec = int.Parse(Request.Form["start"].ToString());
                //int pageSize = int.Parse(Request.Form["length"].ToString());
                //string search = Request.Form["search[value]"].ToString();

                CustomPrincipal User;
                CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();
                User = x.CurrentUser;
                var LandId = User.LandId;
                string[] desdeA = desde.Split('/');
                string[] hastaA = hasta.Split('/');

                DateTime dtDesde = (!string.IsNullOrEmpty(desde)) ? DateTime.ParseExact(desde, "dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.Now; // (Int32.Parse(desdeA[2]), Int32.Parse(desdeA[1]), Int32.Parse(desdeA[0]));
                DateTime dtHasta = (!string.IsNullOrEmpty(hasta)) ? DateTime.ParseExact(hasta, "dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.Now.AddDays(1);// new DateTime(Int32.Parse(hastaA[2]), Int32.Parse(hastaA[1]), Int32.Parse(hastaA[0])); ;

                string sql = string.Format("select * from [dbo].[VW_REPORTE_X_EMPLEADO] where EmployeeId = {2} and WorkDate between '{0}' and '{1}' order by WorkDate desc", dtDesde.ToString("yyyyMMdd"), dtHasta.ToString("yyyyMMdd"), employeeId);

                List<EgresosTrabajadorView> registers = context.Database.SqlQuery<EgresosTrabajadorView>(sql).ToList();

                long TotalRecordsCount = registers.Count();

                #region filters  

                //if (!string.IsNullOrEmpty(search))
                //{
                //    registers = registers.Where(c => (!string.IsNullOrEmpty(c.EmployeeName) && c.EmployeeName.ToLower().Contains(search.ToLower()))
                //                                    || (!string.IsNullOrEmpty(c.EmployeeLastName) && c.EmployeeLastName.ToLower().Contains(search.ToLower()))
                //                                    || (!string.IsNullOrEmpty(c.Documento) && c.Documento.ToLower().Contains(search.ToLower()))
                //                                    || (!string.IsNullOrEmpty(c.Documento) && c.Documento.ToLower().Contains(search.ToLower()))
                //                                ).ToList();
                //}

                #endregion


                //count of record after filter   
                long FilteredRecordCount = registers.Count();

                var list = registers
                                    .ToList();

                if (list == null)
                    list = new List<EgresosTrabajadorView>();


                return this.Json(new
                {
                    // draw = Convert.ToInt32(draw),
                    recordsTotal = TotalRecordsCount,
                    recordsFiltered = FilteredRecordCount,
                    data = list
                }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult EgressoIngresoUnificado2(string desde, string hasta)
        {
            try
            {
                CustomPrincipal User;
                CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();
                User = x.CurrentUser;
                var LandId = User.LandId;

                string[] desdeA = desde.Split('/');
                string[] hastaA = hasta.Split('/');

                DateTime dtDesde = new DateTime(Int32.Parse(desdeA[2]), Int32.Parse(desdeA[1]), Int32.Parse(desdeA[0]));
                DateTime dtHasta = new DateTime(Int32.Parse(hastaA[2]), Int32.Parse(hastaA[1]), Int32.Parse(hastaA[0]));

                ViewBag.Desde = dtDesde.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                ViewBag.Hasta = dtHasta.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                //List<EgresosFromView> xa = db.Database.SqlQuery<EgresosFromView>("SELECT  'Egreso' as 'Accion', [egressDateTime] ,[EmployeeId],[retiredbefore],constratista_Id ,[descriptionout],[groupNumber],[TakenFromForeman] ,[TypeEmployeeId] ,[LandId] ,[UserId] ,[ForemanId]  FROM[Temperatura].[dbo].[VW_EGRESOS] where  LandId=" + LandId + " and egressDateTime>'" + dtDesde.ToString("yyyy-MM-dd") + "'   and egressDateTime<'" + dtHasta.AddDays(1).ToString("yyyy-MM-dd") + "' union select 'Ingreso' as 'Accion', [IngressDateTime] as  'egressDateTime' ,[EmployeeId],[RetiredBefore],[ContractorId] as constratista_Id, [descriptionout],[groupNumber],0 ,[TypeEmployeeId] ,[LandId] ,[UserIdIngressRegister]  as 'UserId',[ForemanId]   FROM [Temperatura].[dbo].[IngressEgresses] where LandId=" + LandId + " and IngressDateTime>'" + dtDesde.ToString("yyyy-MM-dd") + "' and IngressDateTime<'" + dtHasta.AddDays(1).ToString("yyyy-MM-dd") + "' and isDeleted=0 ").ToList();

                ////se hace como hotfix para remover los registros erroneos que genera la query.
                //xa.RemoveAll(a => a.EmployeeId == null);

                //foreach (var item in xa)
                //{
                //    item.Employee = db.Employees.FirstOrDefault(emp => emp.Id == item.EmployeeId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
                //    if (item.Employee != null) item.Employee.Img = "";
                //    item.Foreman = db.Employees.FirstOrDefault(emp => emp.Id == item.ForemanId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
                //    if (item.Foreman != null) item.Foreman.Img = "";
                //    if (item.Foreman == null) item.Foreman = new Employee();
                //    item.User = db.Users.FirstOrDefault(U => U.Id == item.UserId);
                //    item.TypeEmployee = db.TypeEmployee.FirstOrDefault(U => U.Id == item.TypeEmployeeId);
                //    if (item.TypeEmployee == null) item.TypeEmployee = new TypeEmployee();
                //    item.Contractor = db.Contractors.FirstOrDefault(U => U.Id == item.contractorista_Id);
                //    if (item.Contractor == null) item.Contractor = new Contractor();
                //}
                ViewBag.Ingresos = null;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return View("EgressoIngresoUnificado");
        }


        public FileStreamResult GetFile(string desde, string hasta)
        {
            string name = "Ingresos" + DateTime.Now.Date.ToShortDateString() + ".txt";
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

            User = x.CurrentUser;
            var LandId = User.LandId;
            var ingressEgress = db.IngressEgress.Include(i => i.Land).Include(i => i.Employee).Include(i => i.UserEgressRegister).Where(l => l.LandId == LandId && l.EgressDateTime == null && l.isDeleted == false);

            FileInfo info = new FileInfo(name);
            info.Delete();
            if (!info.Exists)
            {
                using (StreamWriter writer = info.CreateText())
                {
                    foreach (var item in ingressEgress)
                    {

                        writer.WriteLine("99.9," + item.Employee.QR + ",0,99991,0," + item.IngressDateTime.Value.ToString("hh:mm:ss") + "," + item.IngressDateTime.Value.ToString("dd/MM/yyyy"));
                    }


                }
            }

            return File(info.OpenRead(), "text/plain", name);

        }


        public FileStreamResult GetFileCorvus(string desde, string hasta)
        {
            //            21/07/2011;10:10:09;1;1104;35373901;35373901;P;;131824.00;99999999;0
            //<fecha del ingreso>;<hora del ingreso>;<in 1/Out 2>;<id vehículo>;<CEMPLEADO data entry>;<CEMPLEADO que ingrea>;<Cod lugar de entrada>;<empty>;<Kms>;99999999;0
            string name = "IngresosCorvuss" + DateTime.Now.Date.ToShortDateString() + ".txt";
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

            User = x.CurrentUser;
            var LandId = User.LandId;
            var ingress = db.IngressEgress.Include(i => i.Land).Include(i => i.Employee).Include(i => i.UserEgressRegister).Where(l => l.LandId == LandId && l.EgressDateTime == null && l.isDeleted == false).OrderBy(u => u.IngressDateTime);

            FileInfo info = new FileInfo(name);
            info.Delete();
            if (!info.Exists)
            {
                using (StreamWriter writer = info.CreateText())
                {
                    foreach (var item in ingress)
                    {
                        writer.WriteLine(item.IngressDateTime.Value.ToString("dd/MM/yyyy") + "," + item.IngressDateTime.Value.ToString("hh:mm:ss") + ",1,0,27," + item.Employee.QR + ",P,,0,99999999;0");
                    }
                }
            }
            return File(info.OpenRead(), "text/plain", name);
        }

        public FileStreamResult GetFileCorvusEgreso(string desde, string hasta)
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

            User = x.CurrentUser;
            var LandId = User.LandId;
            DateTime dtDesde = DateTime.Parse(desde);
            DateTime dtHasta = ConverterDateTime.converteStringTodate(hasta);


            ViewBag.Desde = dtDesde.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.Hasta = dtHasta.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<EgresosFromView> xa = db.Database.SqlQuery<EgresosFromView>("SELECT  [egressDateTime] ,[EmployeeId],[retiredbefore],constratista_Id ,[descriptionout],[groupNumber],[TakenFromForeman] ,[TypeEmployeeId] ,[LandId] ,[UserId] ,[ForemanId]  FROM[Temperatura].[dbo].[VW_EGRESOS] where LandId=" + LandId + " and egressDateTime> '" + dtDesde.ToString("yyyy-MM-dd") + "'  and egressDateTime< '" + dtHasta.AddDays(1).ToString("yyyy-MM-dd") + "'").ToList();

            foreach (var item in xa)
            {
                item.Employee = db.Employees.FirstOrDefault(emp => emp.Id == item.EmployeeId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
                if (item.Employee != null) item.Employee.Img = "";
                item.Foreman = db.Employees.FirstOrDefault(emp => emp.Id == item.ForemanId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
                if (item.Foreman != null) item.Foreman.Img = "";
                //item.User = db.Users.FirstOrDefault(U => U.Id == item.UserId);
                item.TypeEmployee = db.TypeEmployee.FirstOrDefault(U => U.Id == item.TypeEmployeeId);
            }
            string name = "EgressoCorvuss" + DateTime.Now.Date.ToShortDateString() + ".txt";

            User = x.CurrentUser;

            FileInfo info = new FileInfo(name);
            info.Delete();
            if (!info.Exists)
            {
                using (StreamWriter writer = info.CreateText())
                {
                    foreach (var item in xa)
                    {
                        //            21/07/2011;10:10:09;1;1104;35373901;35373901;P;;131824.00;99999999;0
                        //<fecha del ingreso>;<hora del ingreso>;<in 1/Out 2>;<id vehículo>;<CEMPLEADO data entry>;<CEMPLEADO que ingrea>;<Cod lugar de entrada>;<empty>;<Kms>;99999999;0

                        writer.WriteLine(item.egressDateTime.Value.ToString("dd/MM/yyyy") + "," + item.egressDateTime.Value.ToString("hh:mm:ss") + ",2,0,27," + item.Employee.QR + ",P,,0,99999999;0");
                    }
                }
            }
            return File(info.OpenRead(), "text/plain", name);
        }

        //chorbus chimpai visma pa todos
        public FileStreamResult GetFileEgresos(string desde, string hasta)
        {
            string name = "Egresos" + DateTime.Now.Date.ToShortDateString() + ".txt";
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

            User = x.CurrentUser;
            var LandId = User.LandId;
            DateTime dtDesde = DateTime.Parse(desde);
            DateTime dtHasta = ConverterDateTime.converteStringTodate(hasta);


            ViewBag.Desde = dtDesde.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.Hasta = dtHasta.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<EgresosFromView> xa = db.Database.SqlQuery<EgresosFromView>("SELECT [egressDateTime] ,[EmployeeId],[retiredbefore],constratista_Id ,[descriptionout],[groupNumber],[TakenFromForeman] ,[TypeEmployeeId] ,[LandId] ,[UserId] ,[ForemanId]  FROM[Temperatura].[dbo].[VW_EGRESOS] where LandId=" + LandId + " and egressDateTime>'" + dtDesde.ToString("yyyy-MM-dd") + "' and egressDateTime<'" + dtHasta.AddDays(1).ToString("yyyy-MM-dd") + "'").ToList();

            foreach (var item in xa)
            {
                item.Employee = db.Employees.FirstOrDefault(emp => emp.Id == item.EmployeeId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
                if (item.Employee != null) item.Employee.Img = "";
                item.Foreman = db.Employees.FirstOrDefault(emp => emp.Id == item.ForemanId);//.Select(p => new { p.Id, p.groupNumber, p.birthDate, p.Contractor, p.ContractorId, p.Documento, p.EmployeeId, p.Foreman, p.isActive, p.isforeman, p.Land, p.LandId, p.Local, p.Nombre, p.QR, p.Sex, p.TypeEmployee, p.TypeEmployeeId, p.QRHistory })
                if (item.Foreman != null) item.Foreman.Img = "";
                item.User = db.Users.FirstOrDefault(U => U.Id == item.UserId);
                item.TypeEmployee = db.TypeEmployee.FirstOrDefault(U => U.Id == item.TypeEmployeeId);
            }
            FileInfo info = new FileInfo(name);
            info.Delete();
            if (!info.Exists)
            {
                using (StreamWriter writer = info.CreateText())
                {
                    foreach (var item in xa)
                    {

                        writer.WriteLine("99.9," + item.Employee.QR + ",0,99994,0," + item.egressDateTime.Value.ToString("hh:mm:ss") + "," + item.egressDateTime.Value.ToString("dd/MM/yyyy"));
                    }


                }
            }

            return File(info.OpenRead(), "text/plain", name);

        }
        // GET: IngressEgressesTest/Create

        [CustomAuthorize(Roles = "ABMIngresoEgreso")]
        public ActionResult Create()
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

            User = x.CurrentUser;
            var LandId = User.LandId;

            Land Tierra = db.Lands.FirstOrDefault(o => o.Id == LandId);

            List<Employee> listEmployee = db.Employees.Where(l => l.Land.Id == LandId).ToList();


            ViewBag.ContractorId = new SelectList(db.Contractors.Where(qe => qe.LandId == User.LandId).OrderBy(l => l.Description), "Id", "Name");
            ViewBag.EmployeeId = new SelectList(listEmployee.OrderBy(l => l.Apellido), "Id", "Apellido");
            ViewBag.ForemanId = new SelectList(db.Employees.Where(e => e.isforeman && e.LandId == LandId).ToList().OrderBy(l => l.Apellido), "Id", "Nombre");
            ViewBag.TypeEmployeeId = new SelectList(db.TypeEmployee.Where(l => l.LandId == LandId).ToList().OrderBy(l => l.Description), "Id", "Description");
            ViewBag.UserId = new SelectList(Tierra.User.ToList(), "Id", "Name");
            ViewBag.UserIdIngressRegister = new SelectList(Tierra.User.ToList(), "Id", "Name");

            return View(new IngressEgress());
        }

        // POST: IngressEgressesTest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "ABMIngresoEgreso")]
        public ActionResult Create([Bind(Include = "Id,RetiredBefore,DescriptionOut,IngressDateTime,EgressDateTime,DocumentEmployee,EmployeeId,DocumentForeman,ForemanId,LandId,UserId,UserIdIngressRegister,IsSyncToVisma,ContractorId,TypeEmployeeId,groupNumber,isForeman")] IngressEgress ingressEgress)
        {
            var culturaArgentina = CultureInfo.GetCultureInfo("es-AR");
            CultureInfo.DefaultThreadCurrentCulture = culturaArgentina;
            CultureInfo.DefaultThreadCurrentUICulture = culturaArgentina;

            CustomPrincipal User;
            CustomAuthorizeAttribute xo = new CustomAuthorizeAttribute();

            User = xo.CurrentUser;
            var LandId = User.LandId;
            bool CorrectForeman = true;
            ingressEgress.LandId = LandId;
            bool ingresoFecha = ingressEgress.IngressDateTime != null || ingressEgress.EgressDateTime != null;
            if (ingressEgress.IngressDateTime == null)
            {
                ingressEgress.UserId = xo.CurrentUser.Id;
                var foreman = db.Employees.First(QR => QR.Id == ingressEgress.EmployeeId);
                CorrectForeman = foreman.isforeman || ingressEgress.RetiredBefore;
            }
            else
            {
                ingressEgress.UserIdIngressRegister = xo.CurrentUser.Id;
            }
            if (ModelState.IsValid && CorrectForeman && ingresoFecha)
            {
                db.IngressEgress.Add(ingressEgress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (!CorrectForeman)
            {
                ViewBag.ErrorMsg = "El empleado debe ser captaz para un egreso normal ";
            }
            if (!ingresoFecha)
            {
                ViewBag.ErrorMsg = "Debe ingresar la fecha";
            }
            ViewBag.Accion = ingressEgress.EgressDateTime != null ? "Egreso" : "Ingreso";
            ViewBag.ContractorId = new SelectList(db.Contractors.Where(qe => qe.LandId == User.LandId).OrderBy(l => l.Description), "Id", "Name", ingressEgress.ContractorId);
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(x => x.isActive && x.LandId == User.LandId).OrderBy(l => l.Apellido), "Id", "Nombre", ingressEgress.EmployeeId);
            ViewBag.ForemanId = new SelectList(db.Employees.Where(x => x.isforeman && x.isActive && x.LandId == User.LandId).OrderBy(l => l.Apellido), "Id", "Nombre", ingressEgress.ForemanId);
            ViewBag.LandId = new SelectList(db.Lands, "Id", "Name", ingressEgress.LandId);
            ViewBag.TypeEmployeeId = new SelectList(db.TypeEmployee.Where(l => l.LandId == LandId).OrderBy(l => l.Description), "Id", "Description", ingressEgress.TypeEmployeeId);
            ViewBag.UserId = new SelectList(db.Users.OrderBy(l => l.Name), "Id", "Name", ingressEgress.UserId);
            ViewBag.UserIdIngressRegister = new SelectList(db.Users.OrderBy(l => l.Name), "Id", "Name", ingressEgress.UserIdIngressRegister);
            return View(ingressEgress);
        }

        // GET: IngressEgressesTest/Edit/5

        [CustomAuthorize(Roles = "ABMIngresoEgreso")]
        public ActionResult Edit(long? id)
        {
            CustomPrincipal User;
            CustomAuthorizeAttribute xa = new CustomAuthorizeAttribute();
            User = xa.CurrentUser;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngressEgress ingressEgress = db.IngressEgress.Find(id);
            if (ingressEgress == null)
            {
                return HttpNotFound();
            }
            ViewBag.Accion = ingressEgress.EgressDateTime != null ? "Egreso" : "Ingreso";
            ViewBag.ContractorId = new SelectList(db.Contractors.Where(qe => qe.LandId == User.LandId).OrderBy(l => l.Description), "Id", "Name", ingressEgress.ContractorId);
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(x => x.isActive && x.LandId == User.LandId).OrderBy(l => l.Apellido), "Id", "Nombre", ingressEgress.EmployeeId);
            ViewBag.ForemanId = new SelectList(db.Employees.Where(x => x.isforeman && x.isActive && x.LandId == User.LandId).OrderBy(l => l.Apellido), "Id", "Nombre", ingressEgress.ForemanId);
            ViewBag.LandId = new SelectList(db.Lands, "Id", "Name", ingressEgress.LandId);
            ViewBag.TypeEmployeeId = new SelectList(db.TypeEmployee.Where(o => o.LandId == User.LandId).OrderBy(l => l.Description), "Id", "Description", ingressEgress.TypeEmployeeId);
            ViewBag.UserId = new SelectList(db.Users.OrderBy(l => l.Name), "Id", "Name", ingressEgress.UserId);
            ViewBag.UserIdIngressRegister = new SelectList(db.Users.OrderBy(l => l.Name), "Id", "Name", ingressEgress.UserIdIngressRegister);
            ViewBag.IngressDate = ingressEgress.IngressDateTime == null ? "" : ingressEgress.IngressDateTime.Value.ToString("dd/MM/yyyy hh:mm");
            ViewBag.EgressDate = ingressEgress.EgressDateTime == null ? "" : ingressEgress.EgressDateTime.Value.ToString("dd/MM/yyyy hh:mm");

            return View(ingressEgress);
        }

        // POST: IngressEgressesTest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "ABMIngresoEgreso")]
        public ActionResult Edit(IngressEgress ingressEgress)
        {
            var culturaArgentina = CultureInfo.GetCultureInfo("es-AR");
            CultureInfo.DefaultThreadCurrentCulture = culturaArgentina;
            CultureInfo.DefaultThreadCurrentUICulture = culturaArgentina;

            if (ModelState.IsValid)
            {
                CustomPrincipal User;
                CustomAuthorizeAttribute x = new CustomAuthorizeAttribute();

                User = x.CurrentUser;
                var LandId = User.LandId;
                // toInsert.IngressDateTime = ConverterDateTime.converteStringTodateTime(ingress.time);// DateTime.ParseExact(Egrees.time, "dd/MM/yyyy HH:mm", null);
                //toInsert.EgressDateTime = ConverterDateTime.converteStringTodateTime(Egrees.time);// DateTime.ParseExact(Egrees.time, "dd/MM/yyyy HH:mm", null);
                //IngressEgress aux = db.IngressEgress.Find(ingressEgress.Id);
                //if (ingressEgress.IngressDateTime != null)
                //{
                //    ingressEgress.UserId = aux.UserIdIngressRegister != null ? aux.UserIdIngressRegister : aux.UserId;
                //}
                //else
                //{
                //    ingressEgress.UserIdIngressRegister = aux.UserIdIngressRegister != null ? aux.UserIdIngressRegister : aux.UserId;
                //}
                //ingressEgress.UserIdIngressRegister = aux.UserIdIngressRegister!=null ? aux.UserIdIngressRegister:aux.UserId;
                //ingressEgress.UserId = db.IngressEgress.Find(ingressEgress.Id).UserId;
                //ingressEgress 
                ingressEgress.LandId = LandId;
                ingressEgress.UserEdit = db.Users.Find(User.Id);
                ingressEgress.EditDateTime = DateTime.Now;
                ingressEgress.UserEditID = User.Id;
                db.Entry(ingressEgress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractorId = new SelectList(db.Contractors.Where(qe => qe.LandId == User.LandId).OrderBy(l => l.Name), "Id", "Name", ingressEgress.ContractorId);
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(x => x.isActive && x.LandId == User.LandId).OrderBy(l => l.Apellido), "Id", "Nombre", ingressEgress.EmployeeId);
            ViewBag.ForemanId = new SelectList(db.Employees.Where(x => x.isforeman && x.isActive && x.LandId == User.LandId).OrderBy(l => l.Apellido), "Id", "Nombre", ingressEgress.ForemanId);
            ViewBag.LandId = new SelectList(db.Lands, "Id", "Name", ingressEgress.LandId);
            ViewBag.TypeEmployeeId = new SelectList(db.TypeEmployee.Where(o => o.LandId == User.LandId).OrderBy(l => l.Description), "Id", "Description", ingressEgress.TypeEmployeeId);
            ViewBag.UserId = new SelectList(db.Users.OrderBy(l => l.Name), "Id", "Name", ingressEgress.UserId);
            ViewBag.UserIdIngressRegister = new SelectList(db.Users.OrderBy(l => l.Name), "Id", "Name", ingressEgress.UserIdIngressRegister);
            ViewBag.IngressDate = ingressEgress.IngressDateTime == null ? "" : ingressEgress.IngressDateTime.Value.ToString("dd/MM/yyyy hh:mm");
            ViewBag.EgressDate = ingressEgress.EgressDateTime == null ? "" : ingressEgress.EgressDateTime.Value.ToString("dd/MM/yyyy hh:mm");

            return View(ingressEgress);
        }

        public ActionResult ReporteTrabajador()
        {
            return View();
        }

        public ActionResult GetEmployeesByCriteria(string criteria)
        {
            using(var context = new AppIngresoTemperatrasContext())
            {
                var employees = context.Employees.Where(c => c.isActive && (c.Nombre.Contains(criteria) || c.Apellido.Contains(criteria)) || c.Documento.Contains(criteria))
                            .Select(c => new { c.Id, c.Nombre, c.Apellido, c.Documento })
                            .ToList();

                List<Select2Data> employeelist = new List<Select2Data>();
                foreach(var employee in employees)
                {
                    employeelist.Add(new Select2Data
                    {
                        id = (int)employee.Id,
                        name = employee.Nombre + " " + employee.Apellido + " (" + employee.Documento + ")"
                    });
                }

                return Json(employeelist.ToArray(), JsonRequestBehavior.AllowGet);

            }
        }

        // GET: IngressEgressesTest/Delete/5

        [CustomAuthorize(Roles = "ABMIngresoEgreso")]
        public JsonResult Delete(long? id)
        {
            IngressEgress ingressEgress = db.IngressEgress.Find(id);
            ingressEgress.isDeleted = true;
            db.Entry(ingressEgress).State = EntityState.Modified;
            db.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);


        }
        //esto se puede pasar a la base de datos, se hace de esta forma por sugerencia de gera.
        private string LandSwitch(int id)
        {
            switch (id)
            {
                case 1:
                    return "concordiaClock";
                case 2:
                    return "sarmientoClock";
                case 3:
                    return "metanClock";
                case 5:
                    return "gamorelClock";
                case 6:
                    return "chimpayClock";
                default:
                    return "";
            }
        }

    }
}
