using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Storage.Database.Data
{
    class NavigatorCategoryMapping : ClassMap<NavigatorCategoryData>
    {
        public NavigatorCategoryMapping()
        {
            Table("room_category");
            Id(x => x.Id, "id");
            Map(x => x.Caption, "caption");
            Map(x => x.IsEnabled, "enabled");
            Map(x => x.MinimumRank, "min_rank");
            Map(x => x.IsTradingAllowed, "trading_allowed");
        }
    }

    public class NavigatorCategoryData
    {
        public virtual int Id { get; set; }
        public virtual string Caption { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual int MinimumRank { get; set; }
        public virtual bool IsTradingAllowed { get; set; }
    }
}
