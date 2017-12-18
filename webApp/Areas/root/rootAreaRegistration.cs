using System.Web.Mvc;

namespace webApp.Areas.root
{
    public class rootAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "root";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "root_default",
                "root/{controller}/{action}/{id}",
                new { controller = "home", action = "index", id = UrlParameter.Optional },
                new string[] { "webApp.Areas.root.Controllers" }
            );
        }
    }
}
