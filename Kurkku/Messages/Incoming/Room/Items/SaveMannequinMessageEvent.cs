using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Network.Streams;
using Kurkku.Util.Extensions;

namespace Kurkku.Messages.Outgoing
{
    class SaveMannequinMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.RoomUser.Room == null)
                return;

            Room room = player.RoomUser.Room;
            Item item = room.ItemManager.GetItem(request.ReadInt());

            if (item == null || item.Definition.InteractorType != InteractorType.MANNEQUIN)
                return;

            if (string.IsNullOrEmpty(player.Details.Figure))
                return;

            if (!room.IsOwner(player.Details.Id))
                return;

            string mannequinName = request.ReadString().FilterInput(true);

            if (mannequinName.Length > 30)
                mannequinName = mannequinName.Substring(0, 30);

            item.Interactor.SetJsonObject(new MannequinExtraData()
            {
                Figure = player.Details.Figure,
                Gender = player.Details.Sex,
                OutfitName = mannequinName
            });

            item.Update();
            item.Save();
        }
    }
}
