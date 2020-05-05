using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using Kurkku.Util.Extensions;

namespace Kurkku.Messages.Incoming
{
    class PlaceItemMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.RoomUser.Room == null)
                return;

            Room room = player.RoomUser.Room;

            if (room == null || !room.HasRights(player.Details.Id))
                return;

            var placementData = request.ReadString().Split(' ');

            if (!placementData[0].IsNumeric())
                return;

            int itemId = int.Parse(placementData[0]);

            Item item = player.Inventory.GetItem(itemId);

            if (item == null)
                return;

            if (item.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
            {
                var wallPosition = $"{placementData[1]} {placementData[2]} {placementData[3]}";
                room.Mapping.AddItem(item, wallPosition: wallPosition, player: player);
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
                    return;

                room.Mapping.AddItem(item, position, player: player);
            }

            player.Inventory.RemoveItem(item);
            player.Send(new FurniListRemoveComposer(item.Id));
        }
    }
}
