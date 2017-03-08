using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.App.Controllers;

namespace Web.App
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();

            try
            {
                //logger.Error("Application_Error", ex);
            }
            catch (Exception)
            {
            }

            Server.ClearError();
            Response.Clear();
            Response.ContentType = "text/html";
            Response.StatusCode = (ex is HttpException) ? (ex as HttpException).GetHttpCode() : 500;

            // Hack for GoDaddy Hosting
            Response.TrySkipIisCustomErrors = true;

            // if the request is AJAX return JSON else view.
            if (IsAjax())
            {
                var errorMsg = (ex != null ? ex.Message : string.Empty);
                errorMsg += (ex != null && ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                errorMsg += (ex != null && ex.InnerException != null && ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : string.Empty);
                Context.Session["JSON_ERROR_MESSAGE"] = errorMsg;

                Context.Response.Redirect("~/Error/AjaxResponse", false);
                return;
            }

            var model = new HandleErrorInfo(ex, "UNKNOWN", "UNKNOWN");

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("errorInfo", model);

            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }

        private bool IsAjax()
        {
            return Context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
