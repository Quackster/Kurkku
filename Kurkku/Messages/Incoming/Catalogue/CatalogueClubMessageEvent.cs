using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming.Catalogue
{
    class CatalogueClubMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new CatalogueClubMessageComposer(SubscriptionManager.Instance.Subscriptions));
        }
    }
}
