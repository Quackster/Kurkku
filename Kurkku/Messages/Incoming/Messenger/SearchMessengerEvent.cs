using Kurkku.Game;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Incoming.Messenger
{
    class SearchMessengerEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            string query = request.ReadString();

            MessengerDao.SearchMessenger(query);
            var t = 1;
        }
    }
}
