using log4net;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ControlTemperaturaWebApi
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnException(HttpActionExecutedContext filterContext)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            //String message = String.Empty;
            //var exceptionType = filterContext.Exception.GetType();
            //if (exceptionType == typeof(UnauthorizedAccessException))
            //{
            //    message = "Access to the Web API is not authorized.";
            //    status = HttpStatusCode.Unauthorized;
            //}
            //else if (exceptionType == typeof(DivideByZeroException))
            //{
            //    message = "Internal Server Error.";
            //    status = HttpStatusCode.InternalServerError;
            //}
            //else
            //{
            //    message = "Not found.";
            //    status = HttpStatusCode.NotFound;
            //}
            filterContext.Response = new HttpResponseMessage()
            {
                Content = new StringContent(filterContext.Exception.Message, System.Text.Encoding.UTF8, "text/plain"),
                StatusCode = status
            };

            //var controller = filterContext.ActionContext.ControllerContext.Controller.ToString();
            //var action = filterContext.ActionContext.ActionDescriptor.ActionName;
            
            log.Error(filterContext.Exception.Message, filterContext.Exception);

            base.OnException(filterContext);
        }
    }
}