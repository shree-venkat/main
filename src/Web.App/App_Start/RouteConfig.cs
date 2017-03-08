using System.Web.Mvc;
using System.Web.Routing;

namespace Web.App
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Version",
                url: "ver",
                defaults: new { controller = "Home", action = "Version" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "catch-all",
                "{*url}",
                new { controller = "Error", action = "InvalidUrl" }
            );
        }
    }
}
