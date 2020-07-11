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

            var oldPosition = item.Position.Copy();

            bool isRotation = false;

            if (item.Position != new Position(x, y) && item.Position.Rotation != rotation)
            {
                isRotation = true;
            }

            if (isRotation)
            {
                if (item.RollingData != null)
                {
                    return; // Don't allow rotating when rolling.
                }
            }

            if ((oldPosition.X == x &&
                oldPosition.Y == y &&
                oldPosition.Rotation == rotation) || !item.IsValidMove(item, room, x, y, rotation))
            {
                if (new Position(x, y) != item.Position)
                    room.Send(new UpdateFloorItemComposer(item));

                return;
            }

            room.Mapping.MoveItem(item, new Position
            {
                X = x,
                Y = y,
                Rotation = rotation
            });
        }
    }
}
