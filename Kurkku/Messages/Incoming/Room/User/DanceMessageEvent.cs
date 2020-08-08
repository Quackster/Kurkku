using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    public class DanceMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.RoomUser.Room == null)
                return;

            if (player.RoomUser.IsSitting)
                return;

            int danceId = request.ReadInt();

            player.RoomUser.DanceId = danceId;
            player.RoomUser.Room.Send(new DanceMessageComposer(player.RoomEntity.InstanceId, danceId));
        }
    }
}