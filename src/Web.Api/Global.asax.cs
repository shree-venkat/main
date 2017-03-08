using System.Web.Http;
using System.Web.Mvc;

namespace Web.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            GlobalConfiguration.Configuration.EnsureInitialized();
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();

            try
            {
                //logger.Error("Application_Error", ex);
            }
            catch (System.Exception)
            {
            }

            Server.ClearError();
            // Hack for GoDaddy Hosting
            //Response.TrySkipIisCustomErrors = true;
        }
    }
}
