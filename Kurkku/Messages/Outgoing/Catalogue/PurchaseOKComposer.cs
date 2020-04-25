using Kurkku.Game;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    public class PurchaseOKComposer : IMessageComposer
    {
        private CatalogueItem offer;

        public PurchaseOKComposer(CatalogueItem offer)
        {
            this.offer = offer;
        }

        public override void Write()
        {
            SerialiseOffer(this, this.offer);
        }

        internal static void SerialiseOffer(IMessageComposer composer, CatalogueItem item)
        {
            composer.Data.Add(item.Data.Id);
            composer.Data.Add(item.Data.SaleCode);
            composer.Data.Add(item.Data.PriceCoins);
            composer.Data.Add(item.Data.PriceSeasonal);
            composer.Data.Add((int)item.Data.SeasonalType);
            composer.Data.Add(false);
            composer.Data.Add(item.Packages.Count);

            foreach (CataloguePackage package in item.Packages)
            {
                composer.Data.Add(package.Definition.Type);
                composer.Data.Add(package.Definition.Data.SpriteId);
                composer.Data.Add(package.Data.SpecialSpriteId); // extra data
                composer.Data.Add(package.Data.Amount);
                composer.Data.Add(0);
            }

            composer.Data.Add(false);
            composer.Data.Add(item.AllowBulkPurchase);
        }
    }
}
