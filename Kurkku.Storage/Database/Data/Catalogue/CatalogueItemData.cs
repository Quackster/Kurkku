using FluentNHibernate.Mapping;
using System;

namespace Kurkku.Storage.Database.Data
{
    class CatalogueItemMapping : ClassMap<CatalogueItemData>
    {
        public CatalogueItemMapping()
        {
            Table("catalogue_items");
            Id(x => x.Id, "id");
            Map(x => x.SaleCode, "sale_code");
            Map(x => x.PageId, "page_id");
            Map(x => x.OrderId, "order_id");
            Map(x => x.PriceCoins, "price_coins");
            Map(x => x.PriceSeasonal, "price_seasonal");
            Map(x => x.SeasonalType, "seasonal_type");
            Map(x => x.IsHidden, "hidden");
            Map(x => x.Amount, "amount");
            Map(x => x.DefinitionId, "definition_id");
            Map(x => x.SpecialSpriteId, "item_specialspriteid");
            Map(x => x.IsPackage, "is_package");
            Map(x => x.AllowBulkPurchase, "allow_bulk_purchase");
        }
    }

    [Serializable]
    public class CatalogueItemData
    {
        public virtual int Id { get; set; }
        public virtual string SaleCode { get; set; }
        public virtual string PageId { get; set; }
        public virtual int OrderId { get; set; }
        public virtual int PriceCoins { get; set; }
        public virtual int PriceSeasonal { get; set; }
        public virtual SeasonalCurrencyType SeasonalType { get; set; }
        public virtual bool IsHidden { get; set; }
        public virtual int Amount { get; set; }
        public virtual int DefinitionId { get; set; }
        public virtual string SpecialSpriteId { get; set; }
        public virtual bool IsPackage { get; set; }
        public virtual bool AllowBulkPurchase { get; set; }
    }

    public enum SeasonalCurrencyType
    {
        PUMPKINS = 101,
        PEANUTS = 102,
        STARS = 103,
        CLOUDS = 104,
        DIAMONDS = 105,
        LOYALTY_POINTS = 3,
        DUCKETS = 0
    }
}
