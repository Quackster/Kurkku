using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Messages.Incoming
{
    public class PopularFlatsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var roomList = RoomManager.Instance.ReplaceQueryRooms(
                RoomDao.GetPopularFlats()
            );

            player.Send(new FlatListComposer(2, roomList, NavigatorManager.Instance.GetPopularPromotion()));
        }
    }
}
