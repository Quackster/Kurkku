using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Incoming.Catalogue
{
    class CatalogueClubMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            List<CatalogueSubscriptionData> subscriptionData = CatalogueDao.GetSubscriptionData();
            player.Send(new CatalogueClubMessageComposer(subscriptionData));
        }
    }
}
