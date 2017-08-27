using System.Web;
using System.Web.Optimization;

namespace AphidBytes.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //~ new bundles 
            bundles.Add(new ScriptBundle("~/bundles/js/home").Include(
                "~/scripts/jquery.mini.js"));
            bundles.Add(new StyleBundle("~/bundles/css/home").Include(
                "~/scss/css-build/home.css"));
            bundles.Add(new StyleBundle("~/bundles/css/register").Include(
                "~/scss/css-build/register.css"));
            bundles.Add(new StyleBundle("~/bundles/css/support").Include(
                "~/scss/css-build/support.css"));
            bundles.Add(new StyleBundle("~/bundles/css/login").Include(
              "~/scss/css-build/login.css"));
            bundles.Add(new StyleBundle("~/bundles/css/learn").Include(
              "~/scss/css-build/learn.css"));
            bundles.Add(new StyleBundle("~/bundles/css/clones").Include(
              "~/scss/css-build/clones.css"));

            bundles.Add(new StyleBundle("~/bundles/css/layout-plain").Include(
                "~/scss/css-build/layout-plain.css"));
            bundles.Add(new StyleBundle("~/bundles/css/layout-matrix").Include(
                "~/scss/css-build/layout-matrix.css"));
            bundles.Add(new StyleBundle("~/bundles/css/layout-backdoor").Include(
                "~/scss/css-build/layout-backdoor.css"));


            //BundleTable.EnableOptimizations = true;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/jquery.mini.js",
                "~/Scripts/script.js",       
                "~/Scripts/jquery-{version}.js",
                       "~/Scripts/CustomValidations.js",
                       "~/Scripts/jquery.mCustomScrollbar.js",
                       "~/Scripts/jquery.horizontal.scroll.js",
                        "~/Scripts/jquery.cycle.all.js",
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui")
                .Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/core")
                  .Include("~/js/core.js"));

            bundles.Add(new StyleBundle("~/bundles/PregressCss").Include("~/Content/ui.all.css"));
            bundles.Add(new ScriptBundle("~/bundles/ProgressBar").Include("~/Scripts/ui.progressbar.js"));
            bundles.Add(new StyleBundle("~/bundles/3DPopUp")
            .Include("~/Content/component.css", 
            "~/Content/demo.css",
            "~/Content/normalize.css")
                );
            bundles.Add(new ScriptBundle("~/js/3dJS")
                .Include("~/js/jquery.min", "~/js/modernizr.custom")
                );
            bundles.Add(new StyleBundle("~/bundles/ImagePopUp")
                .Include("~/Content/styleImage.css")
                );

            bundles.Add(new ScriptBundle("~/bundles/ImagePopUpScript").Include("~/Scripts/scriptImage.js", "~/Scripts/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.simplemodal.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryFileUpload").Include(
                        "~/Scripts/jquery.fileupload.js",
                        "~/Scripts/jquery.ui.widget.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/stylecss.css", "~/Content/responsivedevice@media.css", "~/Content/style.css", "~/Content/styleresponsive.css", "~/Content/jquery.mCustomScrollbar.css", "~/Content/popup-css.css", "~/Content/backdoorstyle.css", "~/Content/jquery.horizontal.scroll.css", "~/Content/premium_cssstyle.css", "~/Content/popup.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css", 
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"
                        ));

            BundleTable.EnableOptimizations = false;
          
  
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js").Include("~/Scripts/CustomValidations.js"));
        }
    }
}