using Kurkku.Game;
using Kurkku.Messages.Outoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    public class GetRoomSettingsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            Room room = RoomManager.Instance.GetRoom(request.ReadInt());

            if (room == null || !room.IsOwner(player.Details.Id))
                return;

            player.Send(new RoomSettingsDataComposer(room));
        }
    }
}
