using Microsoft.Owin;
using Owin;
//SignalR即时通信
[assembly: OwinStartup(typeof(webApp.App_Start.Startup))]

namespace webApp.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
