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
    }
}
