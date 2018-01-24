using System.Web;
using System.Web.Optimization;

namespace webApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //捆绑正在学习js
            //bundles.Add(new ScriptBundle("~/js/course").Include("~/scripts/course/learn.js"));

            //捆绑css
            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
        }
    }
}