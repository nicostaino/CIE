using AccessoADatos;
using ControlTemperaturaWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clases.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Http;
using System.Net;
using System.Globalization;

namespace ControlTemperaturaWebApi.Controllers
{
    public class EntryAndExitController : Controller
    {
        private AppIngresoTemperatrasContext db = new AppIngresoTemperatrasContext();
        // GET: entryAndExit

        public ActionResult Ingress(FormIngressEgress ingress)
        {
            var culturaArgentina = CultureInfo.GetCultureInfo("es-AR");
            CultureInfo.DefaultThreadCurrentCulture = culturaArgentina;
            CultureInfo.DefaultThreadCurrentUICulture = culturaArgentina;
            try
            {
            
                Employee employee = db.Employees.FirstOrDefault(x => x.Documento == ingress.employee_document.ToString() && x.LandId == ingress.field_id);
                Employee foreman = db.Employees.FirstOrDefault(x => x.Documento == ingress.foreman_document.ToString() && x.LandId == ingress.field_id);
                Contractor Contractor=  db.Contractors.FirstOrDefault(x => x.Id == ingress.contractor_id ); ;
                Contract contract = db.Contracts.Find(ingress.contract_id);
                IngressEgress toInsert = new IngressEgress();
                //creo uno nuevo si no hay ingreso
                toInsert = new IngressEgress();
                toInsert.Land = db.Lands.Find(ingress.field_id);
                // toInsert.LandId = ingress.field;
                toInsert.UserIngressRegister = db.Users.Find(ingress.user_id);
                toInsert.UserIdIngressRegister = ingress.user_id;

                // toInsert.IngressDateTime = DateTime.ParseExact(ingress.time, "dd/MM/yyyy HH:mm tt", null);
                toInsert.IngressDateTime = ConverterDateTime.converteStringTodateTime(ingress.time);// DateTime.ParseExact(Egrees.time, "dd/MM/yyyy HH:mm", null);
                toInsert.groupNumber = ingress.groupNumber;
                
                if (foreman != null)
                {
                    toInsert.Foreman = foreman;
                    toInsert.ForemanId = foreman.Id;
                }


                if (employee != null)
                {

                    toInsert.Employee = employee;
                    toInsert.EmployeeId = employee.Id;
                }

                if (Contractor != null)
                {

                    toInsert.Contractor = Contractor;
                    toInsert.ContractorId = Contractor.Id;
                }
                if(contract != null)
                {
                    toInsert.Contract = contract;
                    toInsert.ContractId = Convert.ToInt32(contract.Id);
                }

                toInsert.TypeEmployee = db.TypeEmployee.Find(ingress.task_id );
              //  toInsert.TypeEmployeeId = ingress.task_id ;
                toInsert.DocumentEmployee = ingress.employee_document;
                toInsert.DocumentForeman = ingress.foreman_document;
                toInsert.isForeman = ingress.isForeman;
                db.IngressEgress.Add(toInsert);
                db.SaveChanges();
                //return View();

            }
            catch (Exception ex)
            {
                ExceptionLoger.Write(ex);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Egrees(FormIngressEgress Egrees)
        {
            var culturaArgentina = CultureInfo.GetCultureInfo("es-AR");
            CultureInfo.DefaultThreadCurrentCulture = culturaArgentina;
            CultureInfo.DefaultThreadCurrentUICulture = culturaArgentina;
            try
            {
                Employee employee = db.Employees.FirstOrDefault(x => x.Documento == Egrees.employee_document.ToString() && x.LandId == Egrees.field_id);
                //      Employee foreman = db.Employees.FirstOrDefault(x => x.Documento.Equals(Egrees.foreman) && x.LandId == Egrees.field);

                IngressEgress toInsert = new IngressEgress();
                //creo uno nuevo si no hay ingreso
                toInsert = new IngressEgress();
                toInsert.DescriptionOut = Egrees.egressReason;
                toInsert.RetiredBefore = Egrees.specialEgress;
                toInsert.Land = db.Lands.Find(Egrees.field_id);
                toInsert.LandId = Egrees.field_id;
                toInsert.UserEgressRegister = db.Users.Find(Egrees.user_id);
                toInsert.UserId = Egrees.user_id;
                toInsert.EgressDateTime = ConverterDateTime.converteStringTodateTime(Egrees.time);// DateTime.ParseExact(Egrees.time, "dd/MM/yyyy HH:mm", null);
                toInsert.isForeman = Egrees.isForeman;
                // var a = new DateTime(2000, 2,2,2,3,4 );
                //toInsert.Foreman = foreman;
                //if (foreman != null)
                //{
                //    toInsert.ForemanId = foreman.Id;
                //}

                if (employee != null)
                {
                    toInsert.EmployeeId = employee.Id;
                }
                toInsert.Employee = employee;
                //  toInsert.EmployeeId = employee != null ? employee.Id : 0;
                //toInsert.TypeEmployee = db.TypeEmployee.Find(Egrees.task);
                //toInsert.TypeEmployeeId = Egrees.task;
                toInsert.DocumentEmployee = Egrees.employee_document;
                // toInsert.DocumentForeman = Egrees.foreman;
                db.IngressEgress.Add(toInsert);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ExceptionLoger.Write(ex);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        // POST: entryAndExit/Create
        // [HttpPost]
        //public ActionResult Ingress(FormIngressEgress ingress)
        //{
        //    try
        //    {

        //        Employee employee = db.Employees.FirstOrDefault(x => x.Documento.Equals(ingress.DocumentEmployee) && x.LandId == ingress.LandId);
        //        Employee foreman = db.Employees.FirstOrDefault(x => x.Documento.Equals(ingress.DocumentForeman) && x.LandId == ingress.LandId);

        //        if (employee != null && foreman != null)
        //        {
        //            //si es captaz
        //            IngressEgress findIfExistIngressEgress = db.IngressEgress.FirstOrDefault(x => x.EgressDateTime.Date == ingress.IngressDateTime.Date && x.LandId == ingress.LandId && long.Parse(x.Foreman.Documento) == ingress.DocumentEmployee && !x.RetiredBefore);
        //            if (findIfExistIngressEgress == null)
        //            {

        //                findIfExistIngressEgress = new IngressEgress();
        //                findIfExistIngressEgress.Land = db.Lands.Find(ingress.LandId);
        //                findIfExistIngressEgress.LandId = ingress.LandId;
        //                findIfExistIngressEgress.UserIngressRegister = db.Users.Find(ingress.UserId);
        //                findIfExistIngressEgress.UserIdIngressRegister = ingress.UserId;
        //                findIfExistIngressEgress.IngressDateTime = ingress.IngressDateTime;
        //                findIfExistIngressEgress.ForemanId = ingress.DocumentForeman;
        //                findIfExistIngressEgress.Foreman = foreman;
        //                findIfExistIngressEgress.Employee = employee;
        //                findIfExistIngressEgress.EmployeeId = employee.Id;
        //            }
        //            else
        //            {
        //                //  findIfExistIngressEgress.Land = db.Lands.Find(ingress.LandId);
        //                findIfExistIngressEgress.LandId = ingress.LandId;
        //                findIfExistIngressEgress.UserIngressRegister = db.Users.Find(ingress.UserId);
        //                findIfExistIngressEgress.UserIdIngressRegister = ingress.UserId;
        //                findIfExistIngressEgress.IngressDateTime = ingress.IngressDateTime;
        //                findIfExistIngressEgress.ForemanId = ingress.DocumentForeman;
        //                findIfExistIngressEgress.Foreman = foreman;
        //                findIfExistIngressEgress.Employee = employee;
        //                findIfExistIngressEgress.EmployeeId = employee.Id;

        //                List<IngressEgress> ListIngressEgress = db.IngressEgress.Where(x => x.IngressDateTime.Date == ingress.IngressDateTime.Date && x.LandId == ingress.LandId && long.Parse(x.Employee.Documento) == ingress.DocumentForeman && !x.RetiredBefore).ToList();
        //                foreach (var item in ListIngressEgress)
        //                {
        //                    item.UserEgressRegister = db.Users.Find(ingress.UserId);
        //                    item.UserId = ingress.UserId;
        //                    item.EgressDateTime = ingress.EgressDateTime;
        //                }
        //            }
        //            //public bool RetiredBefore { get; set; }
        //            // public string DescriptionOut { get; set; }
        //            // public DateTime IngressDateTime { get; set; }
        //            // public DateTime EgressDateTime { get; set; }
        //            // //Se toma el ultimo QR Ordenado por decha Desc
        //            // public long? Document { get; set; }
        //            // public long? ForemanId { get; set; }
        //            // public long LandId { get; set; }
        //            // public long UserId { get; set; }
        //            // public bool IsSyncToVisma { get; set; }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //public ActionResult Egress(FormIngressEgress ingress)
        //{
        //    try
        //    {

        //        Employee employee = db.Employees.FirstOrDefault(x => x.Documento.Equals(ingress.DocumentEmployee) && x.LandId == ingress.LandId);
        //        Employee foreman = db.Employees.FirstOrDefault(x => x.Documento.Equals(ingress.DocumentForeman) && x.LandId == ingress.LandId);

        //        if (employee != null && (foreman != null || ingress.isForeman))
        //        {
        //            IngressEgress findIfExistIngressEgress = db.IngressEgress.FirstOrDefault(x => x.IngressDateTime.Date == ingress.EgressDateTime.Date && x.LandId == ingress.LandId
        //                                                                                          && long.Parse(x.Employee.Documento) == ingress.DocumentEmployee);
        //            if (findIfExistIngressEgress == null)
        //            {
        //                //creo uno nuevo si no hay ingreso
        //                findIfExistIngressEgress = new IngressEgress();
        //                findIfExistIngressEgress.Land = db.Lands.Find(ingress.LandId);
        //                findIfExistIngressEgress.LandId = ingress.LandId;
        //                findIfExistIngressEgress.UserEgressRegister = db.Users.Find(ingress.UserId);
        //                findIfExistIngressEgress.UserId = ingress.UserId;
        //                findIfExistIngressEgress.EgressDateTime = ingress.EgressDateTime;
        //                findIfExistIngressEgress.ForemanId = ingress.DocumentForeman;
        //                findIfExistIngressEgress.Foreman = foreman;
        //                findIfExistIngressEgress.Employee = employee;
        //                findIfExistIngressEgress.EmployeeId = employee.Id;
        //                findIfExistIngressEgress.RetiredBefore = ingress.RetiredBefore;
        //                findIfExistIngressEgress.DescriptionOut = ingress.DescriptionOut;
        //                db.IngressEgress.Add(findIfExistIngressEgress);

        //                try
        //                {
        //                    db.SaveChanges();
        //                }
        //                catch (Exception)
        //                {

        //                    throw;
        //                }
        //            }
        //            else
        //            {
        //                findIfExistIngressEgress.UserEgressRegister = db.Users.Find(ingress.UserId);
        //                findIfExistIngressEgress.UserId = ingress.UserId;
        //                findIfExistIngressEgress.EgressDateTime = ingress.EgressDateTime;
        //                findIfExistIngressEgress.RetiredBefore = ingress.RetiredBefore;
        //                findIfExistIngressEgress.DescriptionOut = ingress.DescriptionOut;
        //                db.Entry(findIfExistIngressEgress).State = EntityState.Modified;
        //                try
        //                {

        //                    db.SaveChanges();
        //                }
        //                catch (DbUpdateConcurrencyException)
        //                {

        //                }
        //                if (!ingress.RetiredBefore)
        //                {
        //                    List<IngressEgress> ListIngressEgress = db.IngressEgress.Where(x => x.IngressDateTime.Date == ingress.EgressDateTime.Date && x.LandId == ingress.LandId && long.Parse(x.Foreman.Documento) == ingress.DocumentEmployee && !x.RetiredBefore).ToList();
        //                    foreach (var item in ListIngressEgress)
        //                    {
        //                        item.UserEgressRegister = db.Users.Find(ingress.UserId);
        //                        item.UserId = ingress.UserId;
        //                        item.EgressDateTime = ingress.EgressDateTime;
        //                    }

        //                }
        //                // findIfExistIngressEgress.Land = db.Lands.Find(ingress.LandId);
        //                //                        findIfExistIngressEgress.LandId = ingress.LandId;
        //                // findIfExistIngressEgress.UserEgressRegister = db.Users.Find(ingress.UserId);
        //                //findIfExistIngressEgress.UserId = ingress.UserId;
        //                //findIfExistIngressEgress.EgressDateTime = ingress.EgressDateTime;
        //                // findIfExistIngressEgress.ForemanId = ingress.ForemanId;
        //                // findIfExistIngressEgress.Foreman = foreman;
        //                // findIfExistIngressEgress.Employee = employee;
        //                // findIfExistIngressEgress.EmployeeId = employee.Id;


        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


    }
}
