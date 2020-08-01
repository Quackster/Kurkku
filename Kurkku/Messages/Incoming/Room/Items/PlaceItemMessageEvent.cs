using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using Kurkku.Util.Extensions;
using Newtonsoft.Json;
using System.Linq;

namespace Kurkku.Messages.Incoming
{
    class PlaceItemMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.RoomUser.Room == null)
                return;

            Room room = player.RoomUser.Room;
            var placementData = request.ReadString().Split(' ');

            if (!placementData[0].IsNumeric())
                return;

            int itemId = int.Parse(placementData[0]);
            Item item = player.Inventory.GetItem(itemId);

            if (item == null)
                return;

            if (room == null || !room.HasRights(player.Details.Id) || (room.ItemManager.HasItem(x => x.Definition.HasBehaviour(ItemBehaviour.NOTE_TAG)) && item.Definition.InteractorType == InteractorType.STICKY_POLE)) 
            {
                player.Send(new ItemPlaceErrorComposer(ItemPlaceError.NoRights));
                return;
            }


            if (item.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
            {
                // Do nothing if dimmer exists.. replicating Habbo's behaviour here, I literally bought another room dimmer on official Habbo just to test what happens!
                if (item.Definition.InteractorType == InteractorType.ROOMDIMMER &&
                    room.ItemManager.HasItem(x => x.Definition.InteractorType == InteractorType.ROOMDIMMER))
                    return;

                var wallPosition = $"{placementData[1]} {placementData[2]} {placementData[3]}";
                room.FurnitureManager.AddItem(item, wallPosition: wallPosition, player: player);
            }
            else
            {
                int x = (int)double.Parse(placementData[1]);
                int y = (int)double.Parse(placementData[2]);
                int rotation = (int)double.Parse(placementData[3]);

                var position = new Position();
                position.X = x;
                position.Y = y;
                position.Rotation = rotation;

                if (!item.IsValidMove(item, room, x, y, rotation))
                {
                    player.Send(new ItemPlaceErrorComposer(ItemPlaceError.NoPlacementAllowed));
                    return;
                }

                room.FurnitureManager.AddItem(item, position, player: player);
            }

            player.Inventory.RemoveItem(item);
            player.Send(new FurniListRemoveComposer(item.Id));
        }
    }
}
