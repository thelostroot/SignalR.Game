using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Game.Models;

namespace SignalR.Game.Proxies
{
    public class ScoreProxy
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public ScoreProxy(User user)
        {
            Name = user.Name;
            Score = user.Score;
        }
    }
}