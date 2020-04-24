using System;
using System.Linq;
using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Data;

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

                // Override the amount with how much we saved >:)
                amount = (amount - totalDiscountedItems);
            }

            // Calculate new price for both credits and seasonal furniture
            int priceCoins = catalogueItem.Data.PriceCoins * amount;
            int priceSeasonal = catalogueItem.Data.PriceSeasonal * amount;

            // Continue standard purchase
            if (priceCoins > player.Details.Credits)
            {
                player.Send(new NoCreditsComposer(true, false));
                return;
            }

            if (priceSeasonal > player.CurrencyDetails.GetBalance(catalogueItem.Data.SeasonalType))
            {
                player.Send(new NoCreditsComposer(false, true, catalogueItem.Data.SeasonalType));
                return;
            }

            // Update credits of user
            if (priceCoins > 0)
            {
                player.CurrencyDetails.ModifyCredits(-priceCoins);
            }


        }
    }
}
