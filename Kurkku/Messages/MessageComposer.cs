using Kurkku.Game.Player;
using Kurkku.Network.Streams;

namespace Kurkku.Messages
{
    public interface MessageEvent
    {
        void Handle(Player player, Request request);
    }
}