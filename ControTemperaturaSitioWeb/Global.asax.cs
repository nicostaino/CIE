using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ControlTemperaturaSitioWeb.Models;
using System.Web.Security;
using Newtonsoft.Json;
using ControlTemperaturaSitioWeb.CustomAuthentication;
using ControlTemperaturaSitioWeb.Controllers;
using log4net;
using System.Data.Entity;
using AccessoADatos;
using System.Configuration;

namespace ControlTemperaturaSitioWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //registrando las dependencias
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new EntityFrameworkModule());
            //registrando los Filter
            builder.RegisterFilterProvider();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            Database.SetInitializer<AppIngresoTemperatrasContext>(null);
        }
     

    protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
    {
        HttpCookie authCookie = Request.Cookies["Cookie1"];
        if (authCookie != null)
        {
                try
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    var serializeModel = JsonConvert.DeserializeObject<CustomSerializeModel>(authTicket.UserData);

                    CustomPrincipal principal = new CustomPrincipal(authTicket.Name);

                    principal.Id = serializeModel.Id;
                    principal.UserName = serializeModel.UserName;
                    principal.Name = serializeModel.Name;

                    principal.Roles = serializeModel.RoleName.ToArray<string>();
                    principal.LandId = serializeModel.LandId;


                    HttpContext.Current.User = principal;
                }
                catch (Exception)
                {

                   /// throw;
                }
          

        }
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            #region Exception Managment
            var currentController = " ";
            var currentAction = " ";
            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));

            if (currentRouteData != null)
            {
                if (currentRouteData.Values["controller"] != null && !String.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
                    currentController = currentRouteData.Values["controller"].ToString();

                if (currentRouteData.Values["action"] != null && !String.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
                    currentAction = currentRouteData.Values["action"].ToString();
            }

            var exception = Server.GetLastError();

            var controller = DependencyResolver.Current.GetService<ErrorController>();
            var routeData = new RouteData();
            var action = "InternalServerError";

            if (exception is HttpException)
            {
                var httpEx = exception as HttpException;

                log.Error(httpEx.Message, httpEx);

                //archivo excede tamaño permitido
                if (httpEx.WebEventCode == System.Web.Management.WebEventCodes.RuntimeErrorPostTooLarge)
                {
                    action = "PostTooLarge";
                }
                else
                {
                    switch (httpEx.GetHttpCode())
                    {
                        case 404:
                            action = "NotFound";
                            break;

                        case 401:
                            action = "Unauthorized";
                            break;
                    }
                }

            }

            HttpContext.Current.ClearError();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.StatusCode = exception is HttpException ? ((HttpException)exception).GetHttpCode() : 500;
            HttpContext.Current.Response.TrySkipIisCustomErrors = true;

            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = action;

            Response.Clear();
            controller.ViewData.Model = new HandleErrorInfo(exception, currentController, currentAction);
            ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(HttpContext.Current), routeData));
            Response.End();
            #endregion
        }

        public static string GetEnviromentApplication()
        {
            return ConfigurationManager.AppSettings["enviroment"]; 
        }

        public static string GetBackgrounEnviromentApplication()
        {
            return ConfigurationManager.AppSettings["enviromentBackground"];
        }

    }
}
