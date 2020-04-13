using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Messages.Incoming
{
    public class PopularFlatsMessengerEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var roomList = RoomManager.Instance.Rooms.Where(x => x.Value.Data.UsersNow > 0).Select(x => x.Value).ToList();
            player.Send(new FlatListComposer(2, roomList));
        }
    }
}
