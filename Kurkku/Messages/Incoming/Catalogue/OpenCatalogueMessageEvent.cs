using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class OpenCatalogueMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new CataloguePagesComposer(player.Details.Rank, player.IsSubscribed));
        }
    }
}
