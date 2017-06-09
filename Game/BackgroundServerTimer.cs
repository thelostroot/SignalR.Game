using System;
using System.Threading;
using System.Web.Hosting;
using Game;
using Microsoft.AspNet.SignalR;

namespace SignalR.Game
{
    public class BackgroundServerTimer : IRegisteredObject
    {
        private readonly IHubContext _uptimeHub;
        private Timer _timer;

        public BackgroundServerTimer()
        {
            _uptimeHub = GlobalHost.ConnectionManager.GetHubContext<BroadcastHub>();

            GameCore.InitUnits();
            _timer = new Timer(BroadcastUptimeToClients, null, TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(GameSettings.ServerBroadcastInterval));
        }
       
        private void BroadcastUptimeToClients(object state)
        {
            GameCore.UpdateUnits();
            _uptimeHub.Clients.All.internetUpTime(DataBase.Units);
        }

        public void Stop(bool immediate)
        {
            _timer.Dispose();

            HostingEnvironment.UnregisterObject(this);
        }
    }
}