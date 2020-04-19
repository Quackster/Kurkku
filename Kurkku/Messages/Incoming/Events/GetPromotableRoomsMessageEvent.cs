using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;

namespace Kurkku.Messages.Incoming
{
    class GetPromotableRoomsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var roomList = RoomManager.Instance.ReplaceQueryRooms(RoomDao.GetUserRooms(player.Details.Id));
            player.Send(new PromotableRoomsMessageComposer(roomList));
        }
    }
}
