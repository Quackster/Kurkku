using System.Linq;
using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Util;
using Kurkku.Util.Extensions;

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

            if (catalogueItem.Definition != null && catalogueItem.Definition.HasBehaviour(ItemBehaviour.EFFECT))
                return; // Effects disabled for now

            if (catalogueItem.Definition != null && (
                catalogueItem.Definition.InteractorType == InteractorType.TELEPORTER || 
                catalogueItem.Definition.InteractorType == InteractorType.ROOMDIMMER))
                return; // Teleporters, trophies & moodlights disabled for now until coded

            string extraData = request.ReadString().FilterInput(false);
            int amount = request.ReadInt();

            // Credits to Alejandro from Morningstar xoxo
            int totalDiscountedItems = 0;
            var discount = CatalogueManager.Instance.GetBestDiscount(cataloguePage.Data.Id);

            if (catalogueItem.AllowBulkPurchase && discount != null)
            {
                decimal basicDiscount = amount / discount.DiscountBatchSize;
                decimal bonusDiscount = 0;

                if (basicDiscount >= discount.MinimumDiscountForBonus)
                {
                    if (amount % discount.DiscountBatchSize == discount.DiscountBatchSize - 1)
                        bonusDiscount = 1;

                    bonusDiscount += basicDiscount - discount.MinimumDiscountForBonus;
                }


                totalDiscountedItems = ((int)basicDiscount * (int)discount.DiscountAmountPerBatch) + (int)bonusDiscount;
            }

            // Can't buy an amount less than 0
            if (amount <= 0)
                return;

            // Calculate new price for both credits and seasonal furniture
            int priceCoins = catalogueItem.Data.PriceCoins * (amount - totalDiscountedItems);
            int priceSeasonal = catalogueItem.Data.PriceSeasonal * (amount - totalDiscountedItems);

            // Continue standard purchase
            if (priceCoins > player.Details.Credits)
            {
                player.Send(new NoCreditsComposer(true, false));
                return;
            }

            if (priceSeasonal > player.Currency.GetBalance(catalogueItem.Data.SeasonalType))
            {
                player.Send(new NoCreditsComposer(false, true, catalogueItem.Data.SeasonalType));
                return;
            }

            // Update credits of user
            if (priceCoins > 0)
            {
                player.Currency.ModifyCredits(-priceCoins);
                player.Currency.UpdateCredits();
            }

            // Update seasonal currency
            if (priceSeasonal > 0)
            {
                player.Currency.AddBalance(catalogueItem.Data.SeasonalType, -priceSeasonal);
                player.Currency.UpdateCurrency(catalogueItem.Data.SeasonalType, false);
                player.Currency.SaveCurrencies();
            }

            CatalogueManager.Instance.Purchase(player.Details.Id, catalogueItem.Data.Id, amount, extraData, DateUtil.GetUnixTimestamp());
        }
    }
}
