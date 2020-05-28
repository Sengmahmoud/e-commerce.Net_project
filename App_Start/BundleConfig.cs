using System.Web;
using System.Web.Optimization;

namespace e_commerce
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/slick.min.js",
                        "~/Scripts/nouislider.min.js",
                        "~/Scripts/jquery.zoom.min.js",
                        "~/Scripts/main.js",
                         "~/Scripts/backend.js",
                         "~/Scripts/toastr.js"


                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/js/bootstrap.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      
                      "~/Content/slick-theme.css",
                      "~/Content/nouislider.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/backend.css",
                      "~/Content/style.css",
                       "~/Content/backend.style.css",
                      "~/Content/slick.css",
                       "~/Content/slick.style.css",
                       "~/Content/toastr.css"


                      ));

            //bundles.Add(new StyleBundle("~/Content/admin/css/css").Include(
            //        "~/Content/admin /css/bootstrap.css",
            //          "~/Content/admin/css/font-awesome.min.css",
            //          "~/Content/admin/css/backend.css"



            //         ));
        }
    }
}






