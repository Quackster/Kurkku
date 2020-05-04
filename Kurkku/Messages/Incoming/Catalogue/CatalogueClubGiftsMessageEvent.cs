using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;

namespace Kurkku.Messages.Incoming.Catalogue
{
    class CatalogueClubGiftsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Subscription.Refresh();
            player.Subscription.CountMemberDays();

            player.Send(new CatalogueClubGiftsMessageComposer(player.IsSubscribed ? player.Subscription : null, SubscriptionManager.Instance.Gifts));
        }
    }
}
