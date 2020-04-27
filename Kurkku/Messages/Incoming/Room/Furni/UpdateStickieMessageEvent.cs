using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Util.Extensions;

namespace Kurkku.Messages.Incoming
{
    class UpdateStickieMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int itemId = request.ReadInt();

            if (player.RoomUser.Room == null)
                return;

            Room room = player.RoomUser.Room;

            if (room == null) // TODO: Fix for staff
                return;

            Item item = room.ItemManager.GetItem(itemId);

            if (item == null) // TODO: Staff check
                return;

            StickieExtraData stickieData = (StickieExtraData)item.Interactor.GetJsonObject();

            String colour = request.ReadString();
            String text = request.ReadString().FilterInput(false);

            if (colour != stickieData.Colour || !stickieData.Message.StartsWith(text))
                if (!room.HasRights(player.Details.Id))
                    return; // TODO: Staff check

            StickieExtraData updatedStickieData = new StickieExtraData
            {
                Colour = colour,
                Message = text
            };

            item.Interactor.SetJsonObject(updatedStickieData);
            item.Update();
            item.Save();
        }
    }
}
