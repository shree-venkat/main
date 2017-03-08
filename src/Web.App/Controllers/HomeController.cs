using System;
using System.Configuration;
using System.Web.Mvc;

namespace Web.App.Controllers
{
    public class HomeController : Controller
    {
        private static readonly string[] ConfigsToLoad = { "apiBaseUrl" };

        public ActionResult Index()
        {
            var appSettings = string.Empty;
            foreach (var configKey in ConfigsToLoad)
            {
                appSettings += string.Format(".constant('{0}', '{1}')", configKey, ConfigurationManager.AppSettings[configKey]);
            }

            ViewBag.AppSettings = appSettings;
            return View();
        }

        public ActionResult Version()
        {
            var ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var builtOn = new DateTime(2000, 1, 1);
            builtOn = builtOn.AddDays(ver.Build);
            var buildNumber = (DateTime.Now.Year - 2000) + builtOn.DayOfYear.ToString("#000");
            builtOn = builtOn.AddSeconds(ver.Revision * 2);

            return Content(string.Format("<div style=\"font-family: Verdana; font-size: 13px\">DLL version: {0}<br /> Build number: {1} (built on {2})</div>", ver, buildNumber, builtOn), "text/html");
        }
    }
}