using Game.Proxies;
using Microsoft.AspNet.SignalR;

namespace SignalR.Game
{
    public class GameHub : Hub
    {
        /*public void Hello()
        {
            Clients.All.hello();
        }*/

        private static bool _isStartGameBroadcast = false;

        public void Auth(string name)
        {
            if(name == null)
                return;
            
            UserService.RegisterUser(name, Context.ConnectionId);
            Clients.Caller.initSettings(new GameSettingProxy());
        }

        public void KillUnit(long id)
        {
            GameCore.KillUnit(id, Context.ConnectionId);
            Clients.All.broadcastKillUnit(id);
            Clients.All.broadcastUpdateScore(GameCore.GetScoreList());
        }

        public void StartGame()
        {
            if (!_isStartGameBroadcast)
            {
                GameCore.InitUnits();
                _isStartGameBroadcast = true;
                while (true)
                {
                    GameCore.UpdateUnits();
                    Clients.All.broadcastUnits(DataBase.Units);
                    System.Threading.Thread.Sleep(GameSettings.ServerBroadcastInterval);
                }
            }
        }
    }
}