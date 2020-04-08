using Kurkku.Game;
using Kurkku.Messages.Outoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using Kurkku.Util.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Messages.Incoming
{
    class BuddyRequestMessengerEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int userId = UserDao.GetIdByName(request.ReadString());

            if (userId < 1)
                return;

            bool acceptsFriendRequests = MessengerDao.AcceptsFriendRequests(userId);

            if (!acceptsFriendRequests)
            {

            }

            //var userSettings = UserSettingsDao.GetById(UserDao.GetByName(request.ReadString()));
        }
    }
}
