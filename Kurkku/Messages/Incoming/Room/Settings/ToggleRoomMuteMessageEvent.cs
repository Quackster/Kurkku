using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class ToggleRoomMuteMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.RoomUser.Room == null)
                return;

            player.RoomUser.Room.Data.IsMuted = !player.RoomUser.Room.Data.IsMuted;
            player.Send(new RoomMuteSettingsComposer(!player.RoomUser.Room.Data.IsMuted));
        }
    }
}
