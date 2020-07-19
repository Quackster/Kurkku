using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class PlaceStickieMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int itemId = request.ReadInt();

            if (player.RoomUser.Room == null)
                return;

            Room room = player.RoomUser.Room;

            if (room == null || !room.HasRights(player.Details.Id))
                return;

            Item item = player.Inventory.GetItem(itemId);

            if (item == null)
                return;

            string wallPosition = request.ReadString();
            room.FurnitureManager.AddItem(item, wallPosition: wallPosition);

            player.Inventory.RemoveItem(item);
            player.Send(new FurniListRemoveComposer(item.Id));
        }
    }
}
