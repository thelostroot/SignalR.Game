using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Game.Models;

namespace SignalR.Game
{
    public static class UserService
    {
        public static void RegisterUser(string name, string connectionId)
        {
            if(name.Length > 1)
                DataBase.Users.Add(new User()
                {
                    ConnectionId = connectionId,
                    Name = name
                });
        }
    }
}