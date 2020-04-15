using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using System.Collections.Generic;

namespace Kurkku.Messages.Incoming
{
    class GetRoomEntryDataMessageComposer : IMessageEvent
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
            player.Send(new RoomEntryInfoComposer(room.Data, room.IsOwner(player.Details.Id)));

            room.Send(new UsersComposer(List.Create<IEntity>(player)));
            room.Send(new UsersStatusComposer(List.Create<IEntity>(player)));

            player.Send(new UsersComposer(room.EntityManager.GetEntities<IEntity>()));
            player.Send(new UsersStatusComposer(room.EntityManager.GetEntities<IEntity>()));

            player.Send(new RoomInfoComposer(room.Data, true, false)); // false, true));

            /*
                    player.send(new USER_OBJECTS(room.getEntities()));
        room.send(new USER_OBJECTS(player), List.of(player));
        player.send(new USER_STATUSES(room.getEntities()));
        */


        }
    }
}
