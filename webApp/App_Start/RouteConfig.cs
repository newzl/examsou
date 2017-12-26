using System.Web.Mvc;
using System.Web.Routing;

namespace webApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();//启用特性路由

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "learnRoute",
                url: "practise/{controller}/{lid}/{sid}/{action}/{stype}",
                defaults: new { controller = "learn", action = "index", stype = UrlParameter.Optional },
                constraints: new { lid = @"^[0-9]*$", sid = @"^[0-9]*$" },
                namespaces: new string[] { "webApp.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "default", action = "index", id = UrlParameter.Optional },
                namespaces: new string[] { "webApp.Controllers" }
            );

        }
    }
}