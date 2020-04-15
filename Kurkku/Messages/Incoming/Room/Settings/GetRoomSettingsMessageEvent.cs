using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Messages.outoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    public class GetRoomSettingsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            Room room = RoomManager.Instance.GetRoom(request.ReadInt());

            if (room == null || !room.IsOwner(player.Details.Id))
                return;

            player.Send(new RoomSettingsDataComposer(room.Data));
        }
    }
}
