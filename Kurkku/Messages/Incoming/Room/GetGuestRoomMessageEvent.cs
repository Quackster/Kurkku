using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class GetGuestRoomMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var room = RoomManager.Instance.GetRoom(request.ReadInt());

            if (room == null)
                return;

            bool isLoading = request.ReadIntAsBool();
            bool checkEntry = request.ReadIntAsBool();

            player.Send(new RoomInfoComposer(room.Data, isLoading, checkEntry));
        }
    }
}
