using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Incoming
{
    public class UserFlatsMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var roomList = RoomManager.Instance.ReplaceQueryRooms(RoomDao.GetUserRooms(player.Details.Id));
            player.Send(new FlatListComposer(2, roomList));
        }
    }
}
