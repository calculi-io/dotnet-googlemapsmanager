using System.Web;
using System.Web.Optimization;

namespace GoogleMapsManager
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/kendojs").Include(
                      "~/Scripts/kendo/2015.2.624/jquery.min.js",
                      "~/Scripts/kendo/2015.2.624/jszip.min.js",
                      "~/Scripts/kendo/2015.2.624/kendo.all.min.js",
                      "~/Scripts/kendo/2015.2.624/kendo.aspnetmvc.min.js",
                      "~/Scripts/kendo.modernizr.custom.js",
                      "~/Scripts/bootstrap-formhelpers.min.js"));

            bundles.Add(new StyleBundle("~/Content/kendocss").Include(
            "~/Content/kendo/2015.2.624/kendo.common-bootstrap.min.css",
            "~/Content/kendo/2015.2.624/kendo.mobile.all.min.css",
            "~/Content/kendo/2015.2.624/kendo.dataviz.min.css",
            "~/Content/kendo/2015.2.624/kendo.bootstrap.min.css",
            "~/Content/kendo/2015.2.624/kendo.dataviz.bootstrap.min.css"));
            }
    }
}
