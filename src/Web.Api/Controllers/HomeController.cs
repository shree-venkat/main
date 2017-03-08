using System;
using System.Web.Http;

namespace Web.Api.Controllers
{
    public class HomeController : ApiController
    {
        // Route - (Root URL)
        public IHttpActionResult Get()
        {
            return Ok("Hello, World!");
        }

        // Route - "ver"
        [HttpGet]
        public string Version(int id)
        {
            var ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var builtOn = new DateTime(2000, 1, 1);
            builtOn = builtOn.AddDays(ver.Build);
            var buildNumber = (DateTime.Now.Year - 2000) + builtOn.DayOfYear.ToString("#000");
            builtOn = builtOn.AddSeconds(ver.Revision * 2);

            return string.Format("DLL version: {0}; Build number: {1} (built on {2})", ver, buildNumber, builtOn);
        }
    }
}
