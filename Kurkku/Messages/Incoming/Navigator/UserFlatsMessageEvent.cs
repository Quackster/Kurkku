using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;

namespace Kurkku.Messages.Incoming
{
    public class UserFlatsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var roomList = RoomManager.SortRooms(
                RoomManager.Instance.ReplaceQueryRooms(RoomDao.GetUserRooms(player.Details.Id))
            );

            player.Send(new FlatListComposer(2, roomList, null));
        }
    }
}
