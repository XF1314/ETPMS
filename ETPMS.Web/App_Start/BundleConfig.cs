using System.Web.Optimization;

namespace ETPMS.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;//生产环境应该设置为True
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                        "~/Scripts/kendo/kendo.all.min.js",
                        "~/Scripts/kendo/messages/kendo.messages.zh-CN.min.js",
                        "~/Scripts/kendo/cultures/kendo.culture.zh-CN.min.js",
                        "~/Scripts/kendo/kendo.dialog.js",
                        "~/Scripts/kendo/kendo.extend.pf.js"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/three").Include(
                        "~/Scripts/three/three.min.js",
                        "~/Scripts/three/TrackballControls.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/kendo/kendo.common.min.css",
                        "~/Content/kendo/kendo.rtl.min.css",
                        "~/Content/kendo/kendo.Silver.min.css",
                        "~/Content/kendo/kendo.Bootstrap.min.css",
                        "~/Content/kendo/kendo.dataviz.min.css",
                        "~/Content/kendo/kendo.dataviz.default.min.css",
                        "~/Content/kendo/kendo.dialog.css",
                        "~/Content/site.css"));
        }
    }
}
