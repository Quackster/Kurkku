using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class QuitMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.RoomUser.Room == null)
                return;

            player.RoomUser.AuthenticateRoomId = -1;
            player.RoomUser.AuthenticateTeleporterId = -1;

            player.RoomUser.Room.EntityManager.LeaveRoom(player, true);
        }
    }
}
