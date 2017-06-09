using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using SignalR.Game;

[assembly: OwinStartup(typeof(Game.Startup))]

namespace Game
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
            System.Web.Hosting.HostingEnvironment.RegisterObject(new BackgroundServerTimer());
        }
    }
}
