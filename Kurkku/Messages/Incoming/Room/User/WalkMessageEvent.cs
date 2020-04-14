using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class WalkMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int x = request.ReadInt();
            int y = request.ReadInt();

            player.RoomUser.Move(x, y);
        }
    }
}
