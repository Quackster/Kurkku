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
            int amountToCharge = catalogueItem.Data.PriceCoins * amount;

            // Credits to Alejandro from Morningstar xoxo
            var discount = CatalogueManager.Instance.GetBestDiscount(cataloguePage.Data.Id);

            if (catalogueItem.AllowBulkPurchase && discount != null)
            {
                decimal basicDiscount = amount / discount.DiscountBatchSize;
                decimal bonusDiscount = 0;

                if (basicDiscount >= discount.MinimumDiscountForBonus)
                {
                    if (amount % discount.DiscountBatchSize == discount.DiscountBatchSize - 1)
                    {
                        bonusDiscount = 1;
                    }

                    bonusDiscount += basicDiscount - discount.MinimumDiscountForBonus;
                }


                int totalDiscountedItems = ((int)basicDiscount * (int)discount.DiscountAmountPerBatch) + (int)bonusDiscount;
                amountToCharge = Math.Max(0, catalogueItem.Data.PriceCoins * (amount - totalDiscountedItems));
            }

            // Check if user has correct currency amount
        }
    }
}
