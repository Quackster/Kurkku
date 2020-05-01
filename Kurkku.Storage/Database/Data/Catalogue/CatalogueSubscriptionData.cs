using FluentNHibernate.Mapping;

namespace Kurkku.Storage.Database.Data
{
    class CatalogueSubscriptionMapping : ClassMap<CatalogueSubscriptionData>
    {
        public CatalogueSubscriptionMapping()
        {
            Table("catalogue_subscriptions");
            Id(x => x.Id, "id");
            Map(x => x.PageId, "page_id");
            Map(x => x.PriceCoins, "price_coins");
            Map(x => x.PriceSeasonal, "price_seasonal");
            Map(x => x.SeasonalType, "seasonal_type");
            Map(x => x.Months, "months");
        }
    }

    public class CatalogueSubscriptionData
    {
        public virtual int Id { get; set; }
        public virtual int PageId { get; set; }
        public virtual int PriceCoins { get; set; }
        public virtual int PriceSeasonal { get; set; }
        public virtual SeasonalCurrencyType SeasonalType { get; set; }
        public virtual int Months { get; set; }
    }
}
