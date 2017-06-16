using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Game;
using SignalR.Game.Enum;
using SignalR.Game.Models;
using SignalR.Game.Proxies;

namespace SignalR.Game
{
    public static class GameCore
    {
        private static readonly int _unitsOffset = 200;

        private static readonly Random _rnd;

        static GameCore()
        {
            _rnd = new Random();
        }

        public static void InitUnits()
        {
            for (int i = 0; i < GameSettings.UnitsCount; i++)
                DataBase.Units.Add(GenerateUnit());
        }

        public static void KillUnit(long id, string connectionId)
        {
            if (DataBase.Units.FirstOrDefault(x => x.Id == id) != null)
            {
                DataBase.Units.RemoveAll(x => x.Id == id);
                var user = DataBase.Users.FirstOrDefault(x => x.ConnectionId == connectionId);
                if (user != null)
                    user.Score++;
            }
        }

        public static void UpdateUnits()
        {
            foreach (var unit in DataBase.Units)
            {
                unit.MovePath = UpdateMovePath(unit.MovePath[0]);
                unit.MoveAnimation = GenerateMoveAnimation();
                unit.TrollAnimation = 0;
            }

            var countKillUnits = GameSettings.UnitsCount - DataBase.Units.Count;
            for (int i = 0; i < countKillUnits; i++)
                DataBase.Units.Add(GenerateUnit());
        }

        public static List<ScoreProxy> GetScoreList()
        {
            return DataBase.Users.Select(x => new ScoreProxy(x)).ToList();
        }

        private static Unit GenerateUnit()
        {
            var unit = new Unit
            {
                Id = DataBase.CurrentId++,
                GamePerson = GenerateGamePerson(),
                MovePath = GenerateMovePath(),
                MoveAnimation = GenerateMoveAnimation(),
                TrollAnimation = 0
            };
            return unit;
        }

        private static GamePerson GenerateGamePerson()
        {
            var max = System.Enum.GetNames(typeof(GamePerson)).Length - 1;
            return (GamePerson)_rnd.Next(0, max);
        }

        private static List<Unit.Point> GenerateMovePath()
        {
            int movePointCount = _rnd.Next(4, 8);
            movePointCount = 4;
            var movePoints = new List<Unit.Point>();
            int lastX = 0;
            int lastY = 0;
            for (int i = 0; i < movePointCount; i++)
            {
                int x = _rnd.Next(0, GameSettings.AreaWidth - 100);
                int y = _rnd.Next(0, GameSettings.AreaHeight - 100);
                while (Math.Abs(x - lastX) < _unitsOffset || Math.Abs(y - lastY) < _unitsOffset)
                {
                    x = _rnd.Next(0, GameSettings.AreaWidth - 100);
                    y = _rnd.Next(0, GameSettings.AreaHeight - 100);
                }

                movePoints.Add(new Unit.Point(x,y));
            }
            
            return movePoints;
        }

        private static List<Unit.Point> UpdateMovePath(Unit.Point lastPoint)
        {
            int movePointCount = _rnd.Next(4, 8);
            movePointCount = 4;
            var movePoints = new List<Unit.Point>();
            int lastX = GameSettings.AreaWidth / 2;
            int lastY = GameSettings.AreaHeight / 2;
            for (int i = 0; i < movePointCount; i++)
            {
                int x = _rnd.Next(0, GameSettings.AreaWidth);
                int y = _rnd.Next(0, GameSettings.AreaHeight);
                while (Math.Abs(x - lastX) < _unitsOffset || Math.Abs(y - lastY) < _unitsOffset)
                {
                    x = _rnd.Next(0, GameSettings.AreaWidth);
                    y = _rnd.Next(0, GameSettings.AreaHeight);
                }

                movePoints.Add(new Unit.Point(x, y));
            }

            movePoints[0] = lastPoint;

            return movePoints;
        }

        private static MoveAnimation GenerateMoveAnimation()
        {
            var n = _rnd.Next(0, 100);
            if ( n <= 70)
            {
                return (MoveAnimation)_rnd.Next(0, System.Enum.GetNames(typeof(MoveAnimation)).Length - 1);
            }
            else
                return MoveAnimation.None;
        }

        /*private static TrollAnimation GenerateTrollAnimation()
        {
            var n = _rnd.Next(0, 100);
            if ((n / 100) <= GameSettings.ShowTrollAnimationProbability)
                return (TrollAnimation)_rnd.Next(0, System.Enum.GetNames(typeof(TrollAnimation)).Length - 1);
            else
                return TrollAnimation.None;
        }*/
    }
}