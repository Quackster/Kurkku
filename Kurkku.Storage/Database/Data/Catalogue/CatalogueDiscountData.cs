using FluentNHibernate.Mapping;
using System;

namespace Kurkku.Storage.Database.Data
{
    class CatalogueDiscountMapping : ClassMap<CatalogueDiscountData>
    {
        public CatalogueDiscountMapping()
        {
            Table("catalogue_discounts");
            Id(x => x.PageId, "page_id");
            Map(x => x.PurchaseLimit, "purchase_limit");
            Map(x => x.ItemCountRequired, "item_count_required");
            Map(x => x.ItemCountFree, "item_count_free");
            Map(x => x.ExpireDate, "expire_at");
        }
    }

    public class CatalogueDiscountData
    {
        public virtual int PageId { get; set; }
        public virtual int PurchaseLimit { get; set; }
        public virtual decimal ItemCountRequired { get; set; }
        public virtual decimal ItemCountFree { get; set; }
        public virtual DateTime ExpireDate { get; set; }
    }
}
