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
    class ApplyDecorationMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.RoomUser.Room == null)
                return;

            Room room = player.RoomUser.Room;

            if (room == null || !room.IsOwner(player.Details.Id))
                return;

            Item item = player.Inventory.GetItem(request.ReadInt());

            if (item == null || item.Definition.InteractorType != InteractorType.DECORATION)
                return;

            switch (item.Definition.Data.Sprite)
            {
                case "floor":
                    room.Data.Floor = item.Interactor.GetExtraData().ToString();
                    room.Send(new RoomPropertyComposer("floor", room.Data.Floor));
                    break;
                case "wallpaper":
                    room.Data.Wallpaper = item.Interactor.GetExtraData().ToString();
                    room.Send(new RoomPropertyComposer("wallpaper", room.Data.Wallpaper));
                    break;
                case "landscape":
                    room.Data.Landscape = item.Interactor.GetExtraData().ToString();
                    room.Send(new RoomPropertyComposer("landscape", room.Data.Landscape));
                    break;
            }

            player.Inventory.RemoveItem(item);
            player.Send(new FurniListRemoveComposer(item.Id));

            RoomDao.SaveRoom(room.Data);
            ItemDao.DeleteItem(item.Data);
        }
    }
}
