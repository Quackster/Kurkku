using FluentNHibernate.Mapping;
using System;

namespace Kurkku.Storage.Database.Data
{
    public class SubscriptionGiftDataMapping : ClassMap<SubscriptionGiftData>
    {
        public SubscriptionGiftDataMapping()
        {
            Table("subscription_gifts");
            Id(x => x.SaleCode, "sale_code");
            Map(x => x.DurationRequirement, "duration_requirement");
        }
    }

    public class SubscriptionGiftData
    {
        public virtual int SaleCode { get; set; }
        public virtual DateTime DurationRequirement { get; set; }
    }
}
