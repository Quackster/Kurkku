using System;
using System.Linq;
using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class PurchaseItemMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var cataloguePage = CatalogueManager.Instance.GetPage(request.ReadInt(), player.Details.Rank, player.IsSubscribed);

            if (cataloguePage == null)
                return;

            var catalogueItem = cataloguePage.Items.FirstOrDefault(x => x.Data.Id == request.ReadInt());

            if (catalogueItem == null)
                return;

            Console.WriteLine(catalogueItem.Definition.Data.Name);
        }
    }
}
