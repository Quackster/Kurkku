using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class GetFurnitureAliasesMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new FurnitureAliasesComposer());
        }
    }
}
