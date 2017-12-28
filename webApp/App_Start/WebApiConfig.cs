using System.Web.Http;

namespace webApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();//启用特性路由
            //默认路由
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //匹配到action
            //config.Routes.MapHttpRoute(
            //    name: "ActionApi",
            //    routeTemplate: "actionapi/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
