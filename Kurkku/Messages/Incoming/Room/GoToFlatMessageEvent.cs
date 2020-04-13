using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Incoming
{
    class GoToFlatMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int roomId = request.ReadInt();

            Room room = RoomManager.Instance.GetRoom(roomId);

            if (room == null)
                return;

            room.EntityManager.EnterRoom(player);
        }
    }
}
