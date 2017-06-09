using System.Collections.Generic;
using SignalR.Game.Models;

namespace SignalR.Game
{
    public static class DataBase
    {
        public static long CurrentId = 0;
        public static List<User> Users;
        public static List<Unit> Units;

        static DataBase()
        {
            Users = new List<User>();
            Units = new List<Unit>();
        }
    }
}