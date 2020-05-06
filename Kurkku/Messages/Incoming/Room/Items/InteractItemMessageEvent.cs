using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class InteractItemMessageEvent : IMessageEvent
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

            if (item == null)
                return;

            if (item.Definition.HasBehaviour(ItemBehaviour.REQUIRES_RIGHTS_FOR_INTERACTION))
            {
                if (!room.HasRights(player.Details.Id))
                    return;
            }

            item.Interactor.OnInteract(player);
        }
    }
}
