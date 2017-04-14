using System.Web.Optimization;

namespace Sitecore.OperationsSuite
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/Js/Script").Include(
                    "~/js/jquery-1.9.1.js",
                    "~/js/jquery-migrate-1.1.1.min.js",
                    "~/js/jquery-ui-1.9.2.min.js",
                    "~/js/modernizr.min.js",
                    "~/js/bootstrap.min.js",
                    "~/js/tinymce/jquery.tinymce.js",
                    "~/js/jquery.jgrowl.js",
                    "~/js/jquery.cookie.js",
                    "~/js/jquery.uniform.min.js",
                    "~/js/flot/jquery.flot.min.js",
                    "~/js/flot/jquery.flot.resize.min.js",
                    "~/js/responsive - tables.js",
                    "~/js/jqColorPicker.js",
                    "~/js/ui.spinner.min",
                     "~/js/wysiwyg.js",
                     "~/js/jquery-ui-sliderAccess.js",
                     "~/js/jquery-ui-timepicker-addon.js",
                     "~/js/custom.js"
             ));

            bundles.Add(new StyleBundle("~/css/Styles").Include(
                   "~/css/style.default.css",
                   "~/css/app.css",
                   "~/css/standard_theme.css",
                   "~/css/font-awesome.css",
                   "~/css/jquery.ui.css",
                   "~/css/jquery-ui-timepicker-addon.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}