using ControlTemperaturaSitioWeb.Models.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ControlTemperaturaSitioWeb.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult AccessDenied()
        {
            return View();
        }

        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RedirectUnknownAction()
        {
            return RedirectToAction("InternalServerError", "Error");
        }

        public ActionResult Error(ExceptionViewModel exceptionBaseModel)
        {
            return View(exceptionBaseModel);
        }

        public ActionResult Unauthorized(string currentController = "Home", string currentAction = "Index")
        {
            if (Session["userSession"] == null)
            {
                //string controller = Request.RequestContext.RouteData.Values["controller"].ToString();
                //  string action = Request.RequestContext.RouteData.Values["action"].ToString();

                return new RedirectToRouteResult(new RouteValueDictionary { { "action", "ReloadSession" }, { "controller", "Home" }, { "currentController", currentController }, { "currentAction", currentAction } });
            }
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult InternalServerError()
        {
            return View();
        }

        public ActionResult PostTooLarge()
        {
            if (Request.IsAjaxRequest())
            {
                var error = new ExceptionViewModel
                {
                    Title = " Resources.AppStrings.ResourceManager.GetS",
                Message = " Resources.AppStrings.ResourceManager.GetString"
                };
                return Json(error);
            }
            else
            {
                return View();
            }
        }

    }
}