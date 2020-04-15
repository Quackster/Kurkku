using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages
{
    public interface IMessageEvent
    {
        void Handle(Player player, Request request);
    }
}