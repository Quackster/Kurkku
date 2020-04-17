using Kurkku.Game;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    public class GetCataloguePageMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var cataloguePage = CatalogueManager.Instance.GetPage(request.ReadInt());

            if (cataloguePage == null || !cataloguePage.IsNavigatable)
                return;

            player.Send(new CataloguePageComposer(cataloguePage));
        }
    }
}
