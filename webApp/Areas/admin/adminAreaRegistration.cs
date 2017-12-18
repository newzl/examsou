using System.Web.Mvc;

namespace webApp.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "admin";
            }
        }
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                new { controller = "home", action = "index", id = UrlParameter.Optional },
                new string[] { "webApp.Areas.admin.Controllers" }
            );
        }
    }
}
