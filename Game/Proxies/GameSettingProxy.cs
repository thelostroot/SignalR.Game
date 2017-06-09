using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Game;

namespace Game.Proxies
{
    public class GameSettingProxy
    {
        public int ClienAnimationIntreval { get; set; }
        public int MaxUnitsCount { get; set; }
        public int AreaWidth { get; set; }
        public int AreaHeight { get; set; }

        public GameSettingProxy()
        {
            ClienAnimationIntreval = GameSettings.ClienAnimationIntreval;
            MaxUnitsCount = GameSettings.UnitsCount;
            AreaWidth = GameSettings.AreaWidth;
            AreaHeight = GameSettings.AreaHeight;
        }
    }
}