namespace SignalR.Game
{
    public class GameSettings
    {
        public static int ClienAnimationIntreval = 5000;
        public static int ServerBroadcastInterval = 5000;

        public static int UnitsCount = 3;
        public static int AreaWidth = 700;
        public static int AreaHeight = 700;

        public static double ShowMoveAnimationProbability = 0.5;
        public static double ShowTrollAnimationProbability = 0.2;
        //public static Dictionary<MoveAnimation, double> AnimatiomProbabilityMap;

        static GameSettings()
        {
            /*AnimatiomProbabilityMap = new Dictionary<MoveAnimation, double>()
            {
                {MoveAnimation.None, 0},
                {MoveAnimation.Rotate360, 1}
            };*/

        }
    }
}