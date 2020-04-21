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

            int itemId = request.ReadInt();
            var catalogueItem = cataloguePage.Items.Where(x => x.Data.Id == itemId).FirstOrDefault();

            if (catalogueItem == null)
                return;

            string extraData = request.ReadString();
            int amount = request.ReadInt();

            var discount = CatalogueManager.Instance.GetBestDiscount(cataloguePage.Data.Id);
            
            Console.WriteLine("extra data: " + extraData);
            Console.WriteLine("amount: " + amount);

            decimal amountToCharge = catalogueItem.Data.PriceCoins * amount;

            if (catalogueItem.AllowBulkPurchase && discount != null)
            {
                decimal percentageSaved = (decimal)(discount.ItemCountFree / discount.ItemCountRequired);
                int amountSaved = (int)Math.Ceiling(percentageSaved * amountToCharge);
                int newAmount = (int)(amountToCharge - amountSaved);
            }
        }
    }
}
