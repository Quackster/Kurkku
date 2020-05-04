using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming.Catalogue
{
    class CatalogueClubGiftsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new CatalogueClubGiftsMessageComposer(player.IsSubscribed ? player.Subscription : null, SubscriptionManager.Instance.Gifts));
        }
    }
}
