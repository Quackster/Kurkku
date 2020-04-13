using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;

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
            player.Send(new RoomVisualizationSettingsComposer(room.Data.FloorThickness, room.Data.WallThickness, room.Data.IsHidingWall));

            room.Send(new UsersComposer(room.EntityManager.GetEntities<IEntity>()));
        }
    }
}
