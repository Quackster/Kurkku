using FluentNHibernate.Mapping;

namespace Kurkku.Storage.Database.Data
{
    public class MessengerCategoryMapping : ClassMap<MessengerCategoryData>
    {
        public MessengerCategoryMapping()
        {
            Table("messenger_category");
            Map(x => x.UserId, "user_id");
            Map(x => x.Label, "label");

            CompositeId()
                .KeyProperty(x => x.UserId, "user_id")
                .KeyProperty(x => x.Label, "label");
        }
    }

    public class MessengerCategoryData
    {
        public virtual int UserId { get; set; }
        public virtual string Label { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var t = obj as MessengerCategoryData;

            if (t == null)
                return false;

            if (UserId == t.UserId && Label == t.Label)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
