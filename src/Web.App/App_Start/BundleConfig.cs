using System.Web.Optimization;

namespace Web.App
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BUNDLES: BundleConfig.cs bundle (which is dynamic content) is not used due to load on the web server when compressing dynamic content.
            //Instead, a JS task runner (Grunt) is used to generate build-time static content bundles to take advantage of static file compression &caching by IIS.

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/app/app.module.js",
                        "~/app/app.config.js",
                        "~/app/app.routes.js").IncludeDirectory(
                        "~/app/common", "*.module.js", true).IncludeDirectory(
                        "~/app/common", "*.js", true).IncludeDirectory(
                        "~/app/shared", "*.module.js", true).IncludeDirectory(
                        "~/app/shared", "*.js", true).IncludeDirectory(
                        "~/app/layout", "*.module.js", true).IncludeDirectory(
                        "~/app/layout", "*.js", true).IncludeDirectory(
                        "~/app/resources", "*.module.js", true).IncludeDirectory(
                        "~/app/resources", "*.js", true).IncludeDirectory(

                        "~/app/functional", "*.module.js", true).IncludeDirectory(
                        "~/app/functional", "*.routes.js", true).IncludeDirectory(
                        "~/app/functional", "*.js", true));
        }
    }
}
