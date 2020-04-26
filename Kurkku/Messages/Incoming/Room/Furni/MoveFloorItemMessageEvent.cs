using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class MoveFloorItemMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int itemId = request.ReadInt();

            if (player.RoomUser.Room == null)
                return;

            Room room = player.RoomUser.Room;

            if (room == null)
                return;

            Item item = room.ItemManager.GetItem(itemId);

            if (item == null || item.Data.OwnerId != player.Details.Id) // TODO: Staff check
                return;


            int x = request.ReadInt();
            int y = request.ReadInt();
            int rotation = request.ReadInt();

            if (!item.IsValidMove(item, room, x, y, rotation))
                return;

            room.Mapping.MoveItem(item, new Position
            {
                X = x,
                Y = y,
                Rotation = rotation
            }); ;
        }
    }
}
