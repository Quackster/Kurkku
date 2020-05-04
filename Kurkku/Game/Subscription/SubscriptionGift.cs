using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Game
{
    public class SubscriptionGift
    {
        #region Properties

        public SubscriptionGiftData Data { get; set; } 
        public CatalogueItem CatalogueItem { get; set; }

        #endregion

        #region Constructors

        public SubscriptionGift(SubscriptionGiftData x, CatalogueItem catalogueItem)
        {
            this.Data = x;
            this.CatalogueItem = catalogueItem;

            if (catalogueItem == null)
            {
                Console.WriteLine("Error: " + Data.SaleCode + " is invalid");
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Amount of club seconds required before being able to collect gift.
        /// </summary>
        public int GetSecondsRequired()
        {
            var nextGiftDate = DateTime.Now;

            switch (ValueManager.Instance.GetString("club.gift.interval.type"))
            {
                case "MONTH":
                    nextGiftDate = nextGiftDate.AddMonths(Data.DurationRequirement * ValueManager.Instance.GetInt("club.gift.interval"));
                    break;
                case "DAY":
                    nextGiftDate = nextGiftDate.AddDays(Data.DurationRequirement * ValueManager.Instance.GetInt("club.gift.interval"));
                    break;
            }

            return (int)(nextGiftDate - DateTime.Now).TotalSeconds;
        }

        public bool IsGiftRedeemable(long subscriptionAge)
        {
            return GetDaysUntilGift(subscriptionAge) <= 0;
        }

        public int GetDaysUntilGift(long subscriptionAge)
        {
            int secondsForGift = GetSecondsRequired();
            int daysUntilGift = (int)(subscriptionAge > secondsForGift ? -1 : TimeSpan.FromSeconds(secondsForGift - subscriptionAge).TotalDays);
            return daysUntilGift;
        }

        #endregion
    }
}
