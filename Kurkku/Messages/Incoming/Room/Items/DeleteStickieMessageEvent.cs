using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using Kurkku.Util.Extensions;

namespace Kurkku.Messages.Incoming
{
    class DeleteStickieMessageEvent : IMessageEvent
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

            if (item == null && item.Data.OwnerId != player.Details.Id && !room.IsOwner(player.Details.Id)) // TODO: Staff check
                return;

            room.Mapping.RemoveItem(item);

            ItemDao.DeleteItem(item.Data);
        }
    }
}
