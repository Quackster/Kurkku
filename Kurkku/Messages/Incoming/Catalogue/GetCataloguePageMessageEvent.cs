using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Outgoing
{
    public class GetCataloguePageMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var cataloguePage = CatalogueManager.Instance.GetPage(request.ReadInt(), player.Details.Rank, player.IsSubscribed);

            if (cataloguePage == null)
                return;

            player.Send(new CataloguePageComposer(cataloguePage));

            var discount = CatalogueManager.Instance.GetBestDiscount(cataloguePage.Data.Id);

            if (discount != null)
                player.Send(new CatalogueItemDiscountComposer(discount));
        }
    }
}
