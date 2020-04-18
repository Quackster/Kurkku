using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Outgoing
{
    public class GetCataloguePageMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var cataloguePage = CatalogueManager.Instance.GetPage(request.ReadInt());

            if (cataloguePage == null || !cataloguePage.Data.IsNavigatable)
                return;

            player.Send(new CataloguePageComposer(cataloguePage));
        }
    }
}
