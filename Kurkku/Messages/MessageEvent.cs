using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages
{
    public interface MessageEvent
    {
        void Handle(Player player, Request request);
    }
}