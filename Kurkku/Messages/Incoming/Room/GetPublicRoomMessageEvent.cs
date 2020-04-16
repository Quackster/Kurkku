using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class GetPublicRoomMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var room = RoomManager.Instance.GetRoom(request.ReadInt());

            if (room == null)
                return;


            player.Send(new PublicRoomCastsComposer(room.Data.Id, room.Data.CCTs));
        }
    }
}
