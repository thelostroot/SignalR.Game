using System.Collections.Generic;
using SignalR.Game.Enum;

namespace SignalR.Game.Models
{
    public class Unit
    {
        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public long Id { get; set; }
        public GamePerson GamePerson { get; set; }
        public List<Point> MovePath { get; set; }
        public MoveAnimation MoveAnimation { get; set; }
        public TrollAnimation TrollAnimation { get; set; }
    }
}