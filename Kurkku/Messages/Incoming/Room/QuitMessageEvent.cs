using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class QuitMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.RoomUser.Room != null)
                player.RoomUser.Room.EntityManager.LeaveRoom(player, true);
        }
    }
}
