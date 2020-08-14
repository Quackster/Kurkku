using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using System.Collections.Generic;

namespace Kurkku.Messages.Incoming
{
    class GetRoomEntryDataMessageEvent : IMessageEvent
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

            player.Send(new FloorItemsComposer(room.ItemManager.Items));
            player.Send(new WallItemsComposer(room.ItemManager.Items));

            foreach (var entity in room.Entities.Values)
            {
                if (entity.RoomEntity.IsDancing)
                    player.Send(new DanceMessageComposer(entity.RoomEntity.InstanceId, entity.RoomEntity.DanceId));
            }

            if (player.RoomEntity.CurrentEffect > 0)
                player.RoomEntity.UseEffect(player.RoomEntity.CurrentEffect);
        }
    }
}
