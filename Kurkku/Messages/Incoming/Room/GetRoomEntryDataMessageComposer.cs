using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Incoming
{
    class GetRoomEntryDataMessageComposer : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.RoomUser.Room == null)
                return;

            Room room = player.RoomUser.Room;
            RoomModel roomModel = room.Model;

            player.Send(new HeightMapComposer(roomModel.Heightmap));
            player.Send(new FloorHeightMapComposer(roomModel.Heightmap));
        }
    }
}
