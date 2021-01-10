using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Messages.Incoming
{
    public class PopularFlatsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var roomList = RoomManager.SortRooms(RoomManager.Instance.Rooms.Where(x => 
                x.Value.Data.UsersNow > 0 && 
                x.Value.Data.IsPrivateRoom
            ).Select(x => x.Value).ToList());

            player.Send(new FlatListComposer(2, roomList, NavigatorManager.Instance.GetPopularPromotion()));
        }
    }
}
