using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using SignalR.Game.Enum;
using SignalR.Game.Models;

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
            DataBase.Units.RemoveAll(x => x.Id == id);
            var user = DataBase.Users.FirstOrDefault(x => x.ConnectionId == connectionId);
            if(user != null)
                user.Score++;
        }

        public static void UpdateUnits()
        {
            foreach (var unit in DataBase.Units)
            {
                unit.MovePath = UpdateMovePath(unit.MovePath[0]);
                unit.MoveAnimation = GenerateMoveAnimation();
                unit.TrollAnimation = GenerateTrollAnimation();
            }

            var countKillUnits = DataBase.Units.Count - GameSettings.UnitsCount;
            for (int i = 0; i < countKillUnits; i++)
                DataBase.Units.Add(GenerateUnit());
        }

        private static Unit GenerateUnit()
        {
            var unit = new Unit
            {
                Id = DataBase.CurrentId++,
                GamePerson = GenerateGamePerson(),
                MovePath = GenerateMovePath(),
                MoveAnimation = GenerateMoveAnimation(),
                TrollAnimation = GenerateTrollAnimation()
            };
            return unit;
        }

        private static GamePerson GenerateGamePerson()
        {
            var max = _rnd.Next(0, System.Enum.GetNames(typeof(GamePerson)).Length - 1);
            return (GamePerson)_rnd.Next(0, max);
        }

        private static List<Unit.Point> GenerateMovePath()
        {
            int movePointCount = _rnd.Next(4, 8);
            var movePoints = new List<Unit.Point>();
            int lastX = 0;
            int lastY = 0;
            for (int i = 0; i < movePointCount; i++)
            {
                int x = _rnd.Next(0, GameSettings.AreaWidth);
                int y = _rnd.Next(0, GameSettings.AreaHeight);
                while (Math.Abs(x - lastX) < _unitsOffset || Math.Abs(y - lastY) < _unitsOffset)
                {
                    x = _rnd.Next(0, GameSettings.AreaWidth);
                    y = _rnd.Next(0, GameSettings.AreaHeight);
                }

                movePoints.Add(new Unit.Point(x,y));
            }
            
            return movePoints;
        }

        private static List<Unit.Point> UpdateMovePath(Unit.Point lastPoint)
        {
            int movePointCount = _rnd.Next(4, 8);
            var movePoints = new List<Unit.Point>();
            int lastX = 0;
            int lastY = 0;
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
            if ((n / 100) <= GameSettings.ShowMoveAnimationProbability)
                return (MoveAnimation)_rnd.Next(0, System.Enum.GetNames(typeof(MoveAnimation)).Length - 1);
            else
                return MoveAnimation.None;
        }

        private static TrollAnimation GenerateTrollAnimation()
        {
            var n = _rnd.Next(0, 100);
            if ((n / 100) <= GameSettings.ShowTrollAnimationProbability)
                return (TrollAnimation)_rnd.Next(0, System.Enum.GetNames(typeof(TrollAnimation)).Length - 1);
            else
                return TrollAnimation.None;
        }
    }
}