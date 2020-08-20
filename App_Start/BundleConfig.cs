using System.Web;
using System.Web.Optimization;

namespace Leave_Management
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-3.5.1.slim.js",
                        "~/Scripts/umd/popper.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/angular.js",
                      "~/Scripts/angular-ui/ui-bootstrap-tpls.js"
                      //"~/Scripts/angular-route.js"
                      //"~/Scripts/angular-ui/ui-bootstrap.min.js"
                      //"~/Scripts/Common_Validate.js"
                      //"~/Scripts/Login_Validate.js"
                      ));

            
            bundles.Add(new ScriptBundle("~/bundles/Validate").Include( 
                        "~/Scripts/Validate.js"
                        //"~/Scripts/Common_Validate.js"
                        ));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/ui-bootstrap-csp.css",
                      "~/css/font-awesome.css"));

        }
    }
}
