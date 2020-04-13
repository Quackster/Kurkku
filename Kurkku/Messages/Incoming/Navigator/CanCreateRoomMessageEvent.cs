using Kurkku.Game;
using Kurkku.Network.Streams;
using Kurkku.Messages.Outgoing;
using Kurkku.Storage.Database.Access;

namespace Kurkku.Messages.Incoming
{
    public class CanCreateRoomMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int maxRoomsAllowed = ValueManager.Instance.GetInt("max.rooms.allowed");

            if (player.IsSubscribed)
                maxRoomsAllowed = ValueManager.Instance.GetInt("max.rooms.allowed.subscribed");

            player.Send(new CanCreateRoomComposer(
                maxRoomsAllowed >= RoomDao.CountUserRooms(player.Details.Id),
                maxRoomsAllowed)
            );
        }
    }
}
