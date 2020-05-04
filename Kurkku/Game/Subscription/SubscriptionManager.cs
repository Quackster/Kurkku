using Kurkku.Messages.Outgoing;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class SubscriptionManager : ILoadable
    {
        #region Fields

        public static readonly SubscriptionManager Instance = new SubscriptionManager();

        #endregion

        #region Properties

        public List<CatalogueSubscriptionData> Subscriptions { get; private set; }
        public List<SubscriptionGift> Gifts { get; private set; }

        #endregion

        #region Constructors

        public void Load()
        {
            Subscriptions = CatalogueDao.GetSubscriptionData();
            Gifts = SubscriptionDao.GetSubscriptionGifts()
                .Select(x => new SubscriptionGift(x, CatalogueManager.Instance.GetItem(x.SaleCode)))
                .OrderBy(x => x.Data.DurationRequirement)
                .ToList();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get if the requested item is actually a subscription
        /// </summary>
        public SubscriptionGift GetGift(string spriteName)
        {
            return Gifts.Where(x => x.CatalogueItem.Data.SaleCode == spriteName || (x.CatalogueItem.Definition != null && x.CatalogueItem.Definition.Data.Sprite == spriteName)).FirstOrDefault();
        }

        /// <summary>
        /// Get if the requested item is actually a subscription
        /// </summary>
        public bool IsSubscriptionItem(int pageId, int itemId)
        {
            var subscriptionData = Subscriptions.Where(x => x.Id == itemId && x.PageId == pageId).FirstOrDefault();

            if (subscriptionData == null)
                return false;

            return true;
        }

        /// <summary>
        /// Handler for purchasing club
        /// </summary>
        public void PurchaseClub(Player player, int pageId, int itemId)
        {
            var subscriptionData = Subscriptions.Where(x => x.Id == itemId && x.PageId == pageId).FirstOrDefault();

            if (subscriptionData == null)
                return;

            // Calculate new price for both credits and seasonal furniture
            int priceCoins = subscriptionData.PriceCoins;
            int priceSeasonal = subscriptionData.PriceSeasonal;

            // Continue standard purchase
            if (priceCoins > player.Details.Credits)
            {
                player.Send(new NoCreditsComposer(true, false));
                return;
            }

            if (priceSeasonal > player.Currency.GetBalance(subscriptionData.SeasonalType))
            {
                player.Send(new NoCreditsComposer(false, true, subscriptionData.SeasonalType));
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
                player.Currency.AddBalance(subscriptionData.SeasonalType, -priceSeasonal);
                player.Currency.UpdateCurrency(subscriptionData.SeasonalType, false);
                player.Currency.SaveCurrencies();
            }

            player.Subscription.AddMonths(subscriptionData.Months);
        }

        #endregion
    }
}
