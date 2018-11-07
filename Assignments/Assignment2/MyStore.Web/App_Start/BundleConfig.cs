using System.Web;
using System.Web.Optimization;

namespace MyStore.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/popper.js",
                      "~/Scripts/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                   "~/plugins/greensock/TweenMax.min.js",
                    "~/plugins/greensock/TimelineMax.min.js",
                    "~/plugins/scrollmagic/ScrollMagic.min.js",
                    "~/plugins/greensock/animation.gsap.min.js",
                    "~/plugins/greensock/ScrollToPlugin.min.js",
                    "~/plugins/OwlCarousel2-2.2.1/owl.carousel.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/plugins/fontawesome-free-5.0.1/css/fontawesome-all.css",
                      "~/plugins/OwlCarousel2-2.2.1/owl.carousel.css",
                      "~/plugins/OwlCarousel2-2.2.1/owl.theme.default.css",
                      "~/plugins/OwlCarousel2-2.2.1/animate.css"
                   ));
        }
    }
}
