using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class GetStickieMessageEvent : IMessageEvent
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

            if (item == null) // TODO: Staff check
                return;

            StickieExtraData stickieData = (StickieExtraData)item.Interactor.GetJsonObject();

            player.Send(new StickieComposer(item.Id, stickieData));
        }
    }
}
