using System.Collections.Generic;
using SignalR.Game.Enum;

namespace SignalR.Game
{
    public class GameSettings
    {
        public static int ClienAnimationIntreval = 5000;
        public static int ServerBroadcastInterval = 4850;

        public static int UnitsCount = 8;
        public static int AreaWidth = 1800;
        public static int AreaHeight = 700;
        
        //public static double ShowTrollAnimationProbability = 0;
        //public static Dictionary<MoveAnimation, double> AnimatiomProbabilityMap;

        static GameSettings()
        {
            /*AnimatiomProbabilityMap = new Dictionary<MoveAnimation, double>()
            {
                {MoveAnimation.None, 0.5},
                {MoveAnimation.Spin, 0.1},
                {MoveAnimation.Sway, 60},
                {MoveAnimation.ToBig, 0.1},
                {MoveAnimation.ToBigWithTransform, 0.05},
                {MoveAnimation.Rotate, 0.1}
            };*/

        }
    }
}