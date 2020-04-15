using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class GoToFlatMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int roomId = request.ReadInt();

            Room room = RoomManager.Instance.GetRoom(roomId);

            if (room == null)
                return;

            room.EntityManager.EnterRoom(player);
        }
    }
}
