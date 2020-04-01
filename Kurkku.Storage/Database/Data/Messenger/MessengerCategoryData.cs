
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Storage.Database.Data.Messenger
{
    public class MessengerCategoryMapping : ClassMap<MessengerCategoryData>
    {
        public MessengerCategoryMapping()
        {
            Table("messenger_category");
            Id(x => x.Id, "id");
            Map(x => x.UserId, "user_id");
            Map(x => x.Label, "label");
        }
    }

    public class MessengerCategoryData
    {
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual string Label { get; set; }
    }
}
