using System.Web.Optimization;

namespace SisvonMVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/methods_pt.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                      "~/Scripts/jquery-ui-1.11.4.js"));

            bundles.Add(new ScriptBundle("~/bundles/scriptsADM").Include(
                      "~/Scripts/jquery-ui-1.11.4.js",
                      "~/Scripts/jQuery.FileUpload/jquery.fileupload.js",
                      "~/Scripts/bootbox.min.js",
                      "~/Scripts/scriptsAdm.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/font-awesome.min.css",
                      "~/Content/bootstrap.css",
                      "~/Content/lightslider/css/lightslider.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/cssAdm").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Adm/MenuAdm.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/Adm/Adm.css",
                      "~/Content/themes/base/theme.css",
                      "~/Content/themes/base/autocomplete.css",
                      "~/Content/jQuery.FileUpload/css/jquery.fileupload.css"
                      ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
