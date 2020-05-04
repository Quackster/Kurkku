using Kurkku.Game;

namespace Kurkku.Messages.Outgoing
{
    public class PurchaseOKComposer : IMessageComposer
    {
        private CatalogueItem item;

        public PurchaseOKComposer(CatalogueItem offer)
        {
            this.item = offer;
        }

        public override void Write()
        {
            SerialiseOffer(this, item);
        }

        internal static void SerialiseOffer(IMessageComposer composer, CatalogueItem item)//, bool spriteAsSaleCode = false)
        {
            composer.Data.Add(item.Data.Id);
            composer.Data.Add(item.Data.SaleCode);//composer.Data.Add(spriteAsSaleCode ? ItemManager.Instance.GetDefinition(item.Packages[0].Data.DefinitionId).Data.Sprite : item.Data.SaleCode);
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
                composer.Data.Add(false);
            }

            composer.Data.Add(0);
            composer.Data.Add(item.AllowBulkPurchase);
        }
    }
}
