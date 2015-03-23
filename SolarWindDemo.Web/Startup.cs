using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SolarWindDemo.Web.Startup))]

namespace SolarWindDemo.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            Microsoft.AspNet.SignalR.StockTicker.Startup.ConfigureSignalR(app);
        }
    }
}
